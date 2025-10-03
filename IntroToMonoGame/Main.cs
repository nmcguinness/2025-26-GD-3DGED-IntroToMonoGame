using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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
        private Matrix _view;        // World space → View space (camera transform)
        private Matrix _projection;  // View space → Clip space (perspective)
        private BasicEffect _unlitEffect; // Fixed-function style shader that understands our W/V/P and vertex colors
        private BasicEffect _litEffect;
        private DemoVPC_LL_Pyramid _pyramidPrimitive;
        private MouseState _msState;
        private DemoPrimitiveTypeRect _rectPrimitive;
        private DemoVPC_TL_Triangle _litTrianglePrimitive;
        private DemoVPNT_TL_Cube_Lit _litCubePrimitive;
        private KeyboardState _kbState;
        private float _xRot, _yRot, _zRot;
        private float _xRotSpeed = 16, _yRotSpeed = 16;
        private DemoVPCNT_TL_Fan_Lit _litFanPrimitive;
        private BasicEffect _litVertexColorEffect;
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

            #region Camera
            // --- View (camera) ---
            // Camera positioned at (0,0,10), looking at the origin, with "up" as +Y
            _view = Matrix.CreateLookAt(new Vector3(0, 0, 2), Vector3.Zero, Vector3.UnitY);

            // --- Projection (lens) ---
            // FOV = 90° (Pi/2), aspect ratio = 16:9, near=1, far=1000
            _projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.PiOver2, 16f / 9f, 0.1f, 1000f);

            #endregion
 
            #region Unlit Material
            _unlitEffect = new BasicEffect(GraphicsDevice)
            {
                VertexColorEnabled = true, // use per-vertex Color in VertexPositionColor
                // Lighting is off by default; not needed for unlit colored lines.
                //LightingEnabled = true
            };

            #endregion
            
            #region Lit Material
            _litEffect = new BasicEffect(GraphicsDevice)
            {
                TextureEnabled = true,
                LightingEnabled = true,
                PreferPerPixelLighting = true
            };

            _litVertexColorEffect = new BasicEffect(GraphicsDevice)
            {
                TextureEnabled = true,
                LightingEnabled = true,
                PreferPerPixelLighting = true
            };

            _litEffect.EnableDefaultLighting();
            _litVertexColorEffect.EnableDefaultLighting();
            #endregion

            #region Primitive Initialization
            _pyramidPrimitive =
                   new DemoVPC_LL_Pyramid();
            _pyramidPrimitive.Initialize();

            _rectPrimitive =
             new DemoPrimitiveTypeRect();
            _rectPrimitive.Initialize();

            _litTrianglePrimitive
                = new DemoVPC_TL_Triangle();
            _litTrianglePrimitive.Initialize();

            //first lit object!!!
            var litCubeTexture = Content.Load<Texture2D>("mona_lisa");
            _litCubePrimitive
                = new DemoVPNT_TL_Cube_Lit(litCubeTexture);
            _litCubePrimitive.Initialize();

            _litFanPrimitive
                = new DemoVPCNT_TL_Fan_Lit(Color.White,
                Content.Load<Texture2D>("mona_lisa"),
                Color.Red.ToVector3(),
                16, //0-256
                Color.Yellow.ToVector3()
                );
            _litFanPrimitive.Initialize();
            #endregion
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }
        protected override void Update(GameTime gameTime)
        {
            _kbState = Keyboard.GetState();

            float dT = (float)gameTime.ElapsedGameTime.TotalSeconds;

            #region Automatic Rotation
            _xRot += dT * _xRotSpeed;
            _yRot += dT * _yRotSpeed;

            #endregion

            #region User input based rotation
            //if (_kbState.IsKeyDown(Keys.W))
            //    _xRot -= dT * _xRotSpeed;
            //else if (_kbState.IsKeyDown(Keys.S))
            //    _xRot += dT * _xRotSpeed;

            //if (_kbState.IsKeyDown(Keys.A))
            //    _yRot -= dT * _yRotSpeed;
            //else if (_kbState.IsKeyDown(Keys.D))
            //    _yRot += dT * _yRotSpeed;

            //_msState = Mouse.GetState();
            //_zRot = _msState.ScrollWheelValue / 100f; 
            #endregion

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

            //_litCubePrimitive.Draw(gameTime,
            //    _litEffect,
            //    Matrix.Identity
            //    * Matrix.CreateRotationX(MathHelper.ToRadians(_xRot))
            //     * Matrix.CreateRotationY(MathHelper.ToRadians(_yRot))
            //        * Matrix.CreateRotationZ(MathHelper.ToRadians(_zRot)),
            //    _view,
            //    _projection,
            //    GraphicsDevice);

            _litFanPrimitive.Draw(gameTime,
                _litVertexColorEffect,
                Matrix.Identity,
                _view, _projection, GraphicsDevice);

            base.Draw(gameTime);
        }
    }
}