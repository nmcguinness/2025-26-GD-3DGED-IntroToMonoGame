using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IntroToMonoGame.Demos.BufferType.VerrtexBuffer
{
    public class DemoVB_TL_Triangle
    {
        private VertexBuffer _vb;

        public void Initialize(GraphicsDevice graphics)
        {
            var verts = new[]
            {
                new VertexPositionColor(new Vector3(-1f,-1f,0f), Color.Red),
                new VertexPositionColor(new Vector3( 1f,-1f,0f), Color.Green),
                new VertexPositionColor(new Vector3( 0f, 1f,0f), Color.Blue),
            };
            _vb = new VertexBuffer(graphics, typeof(VertexPositionColor), 
                verts.Length, BufferUsage.WriteOnly);
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
                graphics.DrawPrimitives(PrimitiveType.TriangleList, 0, 1); // 1 tri
            }
        }
    }
}