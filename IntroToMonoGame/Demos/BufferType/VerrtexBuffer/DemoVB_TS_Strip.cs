using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IntroToMonoGame.Demos.BufferType.VerrtexBuffer
{
    public class DemoVB_TS_Strip
    {
        private VertexBuffer _vb;
        private int _primitiveCount;

        public void Initialize(GraphicsDevice graphics)
        {
            var verts = new[]
            {
                new VertexPositionColor(new Vector3(-3f,-1f,0), Color.Red),
                new VertexPositionColor(new Vector3(-3f, 1f,0), Color.Yellow),
                new VertexPositionColor(new Vector3(-2f,-1f,0), Color.Green),
                new VertexPositionColor(new Vector3(-2f, 1f,0), Color.Cyan),
                new VertexPositionColor(new Vector3(-1f,-1f,0), Color.Blue),
                new VertexPositionColor(new Vector3(-1f, 1f,0), Color.Magenta),
                new VertexPositionColor(new Vector3( 0f,-1f,0), Color.White),
                new VertexPositionColor(new Vector3( 0f, 1f,0), Color.Orange),
            };
            _primitiveCount = verts.Length - 2;

            _vb = new VertexBuffer(graphics, typeof(VertexPositionColor), verts.Length, BufferUsage.WriteOnly);
            _vb.SetData(verts);
        }

        public void Draw(GameTime gameTime, BasicEffect effect,
            Matrix world, Matrix view, Matrix projection,
            GraphicsDevice graphics)
        {
            effect.World = world;
            effect.View = view;
            effect.Projection = projection;
            effect.VertexColorEnabled = true;

            graphics.SetVertexBuffer(_vb);

            foreach (var pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                graphics.DrawPrimitives(PrimitiveType.TriangleStrip, 
                    0, _primitiveCount);
            }
        }
    }
}