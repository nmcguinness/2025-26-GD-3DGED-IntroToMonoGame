using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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

        // Two vertices = one line when using PrimitiveType.LineList
        private VertexPositionColor[] _line;

        private BasicEffect _effect; // Fixed-function style shader that understands our W/V/P and vertex colors
        private float _rotationDeg;  // Rotation around Z in degrees (for a bit of motion)
        private float yPos;
        private float xRot=0, yRot=0;
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
            _view = Matrix.CreateLookAt(new Vector3(0, 0, 1), Vector3.Zero, Vector3.UnitY);

            // --- Projection (lens) ---
            // FOV = 90° (Pi/2), aspect ratio = 16:9, near=1, far=1000
            _projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.PiOver2, 16f / 9f, 0.1f, 1000f);

            // --- Geometry: two colored endpoints define one line (LineList uses pairs) ---
            float axisLength = 0.5f;
            _line = new[]
            {
                //x-axis
                new VertexPositionColor(new Vector3(-axisLength, 0, 0), Color.Red),    // start-x
                new VertexPositionColor(new Vector3( axisLength, 0, 0), Color.Red), // end-x

                //y-axis
                new VertexPositionColor(new Vector3(0, axisLength, 0), Color.Green),    // start-y
                new VertexPositionColor(new Vector3( 0, -axisLength, 0), Color.Green), // end-y

                //z-axis
                new VertexPositionColor(new Vector3(0, 0, axisLength), Color.Blue),    // start-z
                new VertexPositionColor(new Vector3(0, 0, -axisLength), Color.Blue), // end-z
            };

            // --- Effect: tells the GPU how to transform and shade our vertices ---
            _effect = new BasicEffect(GraphicsDevice)
            {
                VertexColorEnabled = true, // use per-vertex Color in VertexPositionColor
                // Lighting is off by default; not needed for unlit colored lines.
            };

            _rotationDeg = 0;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            // No textures needed for a LineList demo.
        }

        protected override void UnloadContent()
        {
            System.Diagnostics.Debug.WriteLine("UnloadContent...");
            base.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {

            xRot += (float)(15 * gameTime.ElapsedGameTime.TotalSeconds);
            yRot += (float)(30 * gameTime.ElapsedGameTime.TotalSeconds);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            // Clear the frame buffer first
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // 1) WORLD: rotate the line around Z so students see motion (SRT lives here)
            //  _effect.World = Matrix.CreateRotationZ(MathHelper.ToRadians(_rotationDeg));

            //world = I * S * Ro * T
            _effect.World = Matrix.Identity
                            * Matrix.CreateRotationX(MathHelper.ToRadians(xRot))
                               * Matrix.CreateRotationY(MathHelper.ToRadians(yRot));

            // 2) VIEW: camera transform (where we look from, where we look to, what's "up")
            _effect.View = _view;

            // 3) PROJECTION: perspective lens (FOV, aspect, near/far)
            _effect.Projection = _projection;

            // The BasicEffect can have multiple passes depending on technique.
            // For BasicEffect's default technique, there is typically one pass.
            foreach (var pass in _effect.CurrentTechnique.Passes)
            {
                // Binds shader state & constants (W/V/P, vertex format) for this pass
                pass.Apply();

                // DrawUserPrimitives parameters:
                //   PrimitiveType.LineList → every PAIR of vertices is an independent line
                //   _line                  → our vertex array
                //   vertexOffset=0         → start at the beginning of the array
                //   primitiveCount=1       → number of LINES to draw (not vertices!)
                GraphicsDevice.DrawUserPrimitives(
                    PrimitiveType.LineList, _line, vertexOffset: 0, primitiveCount: 3);
            }

            base.Draw(gameTime);
        }
    }
}
