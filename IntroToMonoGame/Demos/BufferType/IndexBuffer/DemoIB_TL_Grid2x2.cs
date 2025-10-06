using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IntroToMonoGame
{
    public class DemoIB_TL_Grid2x2
    {
        private VertexBuffer _vb;
        private IndexBuffer _ib;
        private int _primitiveCount;

        public void Initialize(GraphicsDevice graphics)
        {
            int w = 2, h = 2;
            var verts = new VertexPositionColor[(w + 1) * (h + 1)];
            int k = 0;
            for (int y = 0; y <= h; y++)
                for (int x = 0; x <= w; x++)
                {
                    float xf = x - w / 2f, yf = y - h / 2f;
                    verts[k++] = new VertexPositionColor(new Vector3(xf, yf, 0), new Color(x * 120, y * 120, 255));
                }

            var idx = new short[w * h * 6];
            int i = 0;
            for (int y = 0; y < h; y++)
                for (int x = 0; x < w; x++)
                {
                    int v0 = y * (w + 1) + x;
                    int v1 = v0 + 1;
                    int v2 = (y + 1) * (w + 1) + x;
                    int v3 = v2 + 1;

                    idx[i++] = (short)v0; idx[i++] = (short)v1; idx[i++] = (short)v2;
                    idx[i++] = (short)v2; idx[i++] = (short)v1; idx[i++] = (short)v3;
                }
            _primitiveCount = w * h * 2;

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
                graphics.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0,_primitiveCount);
            }
        }
    }
}