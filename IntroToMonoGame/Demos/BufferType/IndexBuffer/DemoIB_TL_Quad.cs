using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IntroToMonoGame
{
    public class DemoIB_TL_Quad
    {
        private VertexBuffer _vb;
        private IndexBuffer _ib;

        public void Initialize(GraphicsDevice graphics)
        {
            var verts = new[]
            {
                new VertexPositionColor(new Vector3(-1, 1,0), Color.Red),    // 0
                new VertexPositionColor(new Vector3( 1, 1,0), Color.Green),  // 1
                new VertexPositionColor(new Vector3(-1,-1,0), Color.Blue),   // 2
                new VertexPositionColor(new Vector3( 1,-1,0), Color.Yellow), // 3
            };
            var idx = new short[] { 0, 1, 2, 2, 1, 3 }; // 2 triangles

            _vb = new VertexBuffer(graphics, typeof(VertexPositionColor), verts.Length, BufferUsage.WriteOnly);
            _vb.SetData(verts);

            _ib = new IndexBuffer(graphics, IndexElementSize.SixteenBits, idx.Length, BufferUsage.WriteOnly);
            _ib.SetData(idx);
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
            graphics.Indices = _ib;

            foreach (var pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                graphics.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, 2);
            }
        }
    }
}