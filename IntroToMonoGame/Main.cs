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
        #region Camera
        private Matrix _view;        // World space → View space (camera transform)
        private Matrix _projection;  // View space → Clip space (perspective) 
        #endregion

        #region Primitives
        private DemoVPC_LL_Pyramid _pyramidPrimitive;
        private DemoPrimitiveTypeRect _rectPrimitive;
        private DemoVPC_TL_Triangle _litTrianglePrimitive;
        private DemoVPNT_TL_Cube_Lit _litCubePrimitive;
        private DemoVPCNT_TL_Fan_Lit _litFanPrimitive; 
        #endregion

        #region Materials
        private BasicEffect _unlitEffect; // Fixed-function style shader that understands our W/V/P and vertex colors
        private BasicEffect _litEffect;
        private BasicEffect _litVertexColorEffect;
        #endregion

        #region Input
        private KeyboardState _kbState, _oldkbState;
        private MouseState _msState;
        #endregion

        #region World-matrix driven movement
        private float _xRot, _yRot, _zRot;
        private float _xRotSpeed = 16, _yRotSpeed = 90;
        #endregion

        #region Demo switching
        private int _demoIndex = 5;          // default: show fan grid 
        #endregion
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
            var cameraPosition = new Vector3(0, 0, 2);
            // Camera positioned at (0,0,10), looking at the origin, with "up" as +Y
            _view = Matrix.CreateLookAt(cameraPosition, Vector3.Zero, Vector3.UnitY);

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
                = new DemoVPCNT_TL_Fan_Lit(
                    Color.White,
                Content.Load<Texture2D>("mona_lisa"),
                Color.White.ToVector3(),
                32, //0-256
                Color.Yellow.ToVector3(),
                CullMode.None, //see front and back faces
                FillMode.Solid); //wireframe off
            _litFanPrimitive.Initialize(GraphicsDevice);
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

            // rotation demo
            _xRot += dT * _xRotSpeed;
            _yRot += dT * _yRotSpeed;

            // --- edge-trigger input for 1..5 (both top row and numpad) ---
            bool Pressed(Keys k) => _kbState.IsKeyDown(k) && _oldkbState.IsKeyUp(k);

            if (Pressed(Keys.D1)) _demoIndex = 1;   // Pyramid (unlit)
            if (Pressed(Keys.D2)) _demoIndex = 2;   // Rect (unlit)
            if (Pressed(Keys.D3)) _demoIndex = 3;   // Lit Triangle
            if (Pressed(Keys.D4)) _demoIndex = 4;   // Lit Cube
            if (Pressed(Keys.D5)) _demoIndex = 5;   // Fan

            _oldkbState = _kbState;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Gray);

            // --- choose a demo by _demoIndex ---
            switch (_demoIndex)
            {
                case 1: // Pyramid (unlit)
                    _pyramidPrimitive.Draw(
                        gameTime, _unlitEffect,
                        Matrix.Identity
                            * Matrix.CreateRotationX(MathHelper.ToRadians(_xRot))
                            * Matrix.CreateRotationY(MathHelper.ToRadians(_yRot)),
                        _view, _projection, GraphicsDevice);
                    break;

                case 2: // Rect (unlit)
                    _rectPrimitive.Draw(
                        gameTime, _unlitEffect,
                        Matrix.Identity,
                        _view, _projection, GraphicsDevice);
                    break;

                case 3: // Lit Triangle
                    _litTrianglePrimitive.Draw(
                        gameTime, _unlitEffect, // (uses vertex color; ok to keep unlit effect)
                        Matrix.Identity,
                        _view, _projection, GraphicsDevice);
                    break;

                case 4: // Lit Cube (textured, spinning)
                    _litCubePrimitive.Draw(
                        gameTime, _litEffect,
                        Matrix.Identity
                            * Matrix.CreateRotationX(MathHelper.ToRadians(_xRot))
                            * Matrix.CreateRotationY(MathHelper.ToRadians(_yRot))
                            * Matrix.CreateRotationZ(MathHelper.ToRadians(_zRot)),
                        _view, _projection, GraphicsDevice);
                    break;

                case 5: // Fan          
                        _litFanPrimitive.Draw(
                            gameTime, _litVertexColorEffect,
                            Matrix.Identity
                            * Matrix.CreateRotationY(MathHelper.ToRadians(_yRot)),
                            _view, _projection, GraphicsDevice);
                        break;
                    
            }

            base.Draw(gameTime);
        }




        #region Draw Primitive Helpers
        // --- Draw Helpers (effect + world passed in) -------------------------------

        private void DrawPyramid(GameTime gameTime, BasicEffect effect, Matrix world)
        {
            _pyramidPrimitive.Draw(
                gameTime, effect, world,
                _view, _projection, GraphicsDevice);
        }

        private void DrawRect(GameTime gameTime, BasicEffect effect, Matrix world)
        {
            _rectPrimitive.Draw(
                gameTime, effect, world,
                _view, _projection, GraphicsDevice);
        }

        private void DrawLitTriangle(GameTime gameTime, BasicEffect effect, Matrix world)
        {
            _litTrianglePrimitive.Draw(
                gameTime, effect, world,
                _view, _projection, GraphicsDevice);
        }

        private void DrawLitCube(GameTime gameTime, BasicEffect effect, Matrix world)
        {
            _litCubePrimitive.Draw(
                gameTime, effect, world,
                _view, _projection, GraphicsDevice);
        }

        private void DrawFan(GameTime gameTime, BasicEffect effect, Matrix world)
        {
            _litFanPrimitive.Draw(
                gameTime, effect, world,
                _view, _projection, GraphicsDevice);
        }

        // 4×4 grid of fans on the XY plane (z fixed). Each instance = baseWorld * perInstance
        private void DrawFanGrid(
            GameTime gameTime,
            BasicEffect effect,
            Matrix baseWorld,
            int grid = 4,
            float spacing = 2f,
            float zOffset = 0f,
            bool spinInPlane = true)
        {
            float half = (grid - 1) * 0.5f;

            for (int gx = 0; gx < grid; gx++)
                for (int gy = 0; gy < grid; gy++)
                {
                    float x = (gx - half) * spacing;
                    float y = (gy - half) * spacing;

                    // spin around Z to keep blades in XY (use _yRot for speed), then translate
                    Matrix perInstance =
                          (spinInPlane ? Matrix.CreateRotationZ(MathHelper.ToRadians(_yRot)) : Matrix.Identity)
                        * Matrix.CreateTranslation(new Vector3(x, y, zOffset));

                    DrawFan(gameTime, effect, perInstance * baseWorld);
                }
        }
        #endregion

    }
}