using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
using System;

namespace IntroToMonoGame
{
    /// <summary>
    /// Minimal example: draw a single line in 3D using a LineList and BasicEffect.
    /// Shows the classic W-V-P (World, View, Projection) pipeline.
    /// </summary>
    public class Main : Game
    {
        #region Fields
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // World/View/Projection matrices (aka SRT → camera → lens)
        private Matrix _world;       // Model space → World space (Scale/Rotate/Translate)
        private Matrix _view;        // World space → View space (camera transform)
        private Matrix _projection;  // View space → Clip space (perspective)
        private BasicEffect _unlitEffect; // Fixed-function style shader that understands our W/V/P and vertex colors
        private BasicEffect _litEffect;
        private DemoVPC_LL_Pyramid _pyramidPrimitive;
        private float yRot;
        private float xRot;
        private DemoPrimitiveTypeRect _rectPrimitive;
        private DemoVPC_TL_Triangle _litTrianglePrimitive;
        private DemoVPNT_TL_Cube_Lit _litCubePrimitive;

        #endregion

        public Main()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // --- Window / backbuffer ---
            _graphics.PreferredBackBufferWidth = 1920;   // 1080p window
            _graphics.PreferredBackBufferHeight = 1080;
            _graphics.ApplyChanges();
            // Tip: Alt+Enter toggles fullscreen in MonoGame templates.

            // --- World matrix (SRT): start as identity (no scale/rotate/translate) ---
            _world = Matrix.Identity;

            // --- View (camera) ---
            // Camera positioned at (0,0,10), looking at the origin, with "up" as +Y
            _view = Matrix.CreateLookAt(new Vector3(0, 0, 2), Vector3.Zero, Vector3.UnitY);

            // --- Projection (lens) ---
            // FOV = 90° (Pi/2), aspect ratio = 16:9, near=1, far=1000
            _projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.PiOver2, 16f / 9f, 0.1f, 1000f);

            // --- Effect: tells the GPU how to transform and shade our vertices ---
            _unlitEffect = new BasicEffect(GraphicsDevice)
            {
                VertexColorEnabled = true, // use per-vertex Color in VertexPositionColor
                // Lighting is off by default; not needed for unlit colored lines.
                //LightingEnabled = true
            };

            _litEffect = new BasicEffect(GraphicsDevice)
            {
                TextureEnabled = true,
                LightingEnabled = true,
                PreferPerPixelLighting = true            
            };

            _litEffect.EnableDefaultLighting();
            
            _pyramidPrimitive =
                new DemoVPC_LL_Pyramid();
            _pyramidPrimitive.InitializeVerts();

            _rectPrimitive =
             new DemoPrimitiveTypeRect();
            _rectPrimitive.InitializeVerts();

            _litTrianglePrimitive
                = new DemoVPC_TL_Triangle();
            _litTrianglePrimitive.InitializeVerts();

            //first lit object!!!
            var litCubeTexture = Content.Load<Texture2D>("mona_lisa");
            _litCubePrimitive
                = new DemoVPNT_TL_Cube_Lit(litCubeTexture);
            _litCubePrimitive.InitializeVert();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }
        protected override void Update(GameTime gameTime)
        {
            KeyboardState kbState = Keyboard.GetState();
            if (kbState.IsKeyDown(Keys.A))
                yRot += 1;
            else if (kbState.IsKeyDown(Keys.D))
                yRot -= 1;
            if (kbState.IsKeyDown(Keys.W))
                xRot += 1;
            else if (kbState.IsKeyDown(Keys.S))
                xRot -= 1;

            MouseState msState = Mouse.GetState();
            xRot = msState.ScrollWheelValue / 100f;

            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Gray);

            //_pyramidPrimitive.Draw(gameTime,
            //    _unlitEffect,
            //    Matrix.Identity 
            //    * Matrix.CreateRotationX(MathHelper.ToRadians(xRot))
            //    * Matrix.CreateRotationY(MathHelper.ToRadians(yRot)),
            //    _view,
            //    _projection,
            //    GraphicsDevice);

            //_rectPrimitive.Draw(gameTime,
            //    _unlitEffect,
            //    Matrix.Identity,
            //    _view,
            //    _projection,
            //    GraphicsDevice);

            //_litTrianglePrimitive.Draw(gameTime,
            //   _unlitEffect,
            //   Matrix.Identity,
            //   _view,
            //   _projection,
            //   GraphicsDevice);

            _litCubePrimitive.Draw(gameTime,
                _litEffect,
                Matrix.Identity,
                _view,
                _projection,
                GraphicsDevice);
            
            base.Draw(gameTime);
        }
    }
}