using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IntroToMonoGame
{
    public class DemoIB_TL_Cube
    {
        private VertexBuffer _vb;
        private IndexBuffer _ib;
        private Matrix _localWorld = Matrix.Identity;

        public void Initialize(GraphicsDevice graphics)
        {
            float v = 1f;
            var verts = new[]
            {
                new VertexPositionColor(new Vector3(-v, -v, -v), Color.Red),    // 0
                new VertexPositionColor(new Vector3( v, -v, -v), Color.Green),  // 1
                new VertexPositionColor(new Vector3( v,  v, -v), Color.Blue),   // 2
                new VertexPositionColor(new Vector3(-v,  v, -v), Color.Yellow), // 3
                new VertexPositionColor(new Vector3(-v, -v,  v), Color.Cyan),   // 4
                new VertexPositionColor(new Vector3( v, -v,  v), Color.Magenta),// 5
                new VertexPositionColor(new Vector3( v,  v,  v), Color.White),  // 6
                new VertexPositionColor(new Vector3(-v,  v,  v), Color.Orange), // 7
            };

            var idx = new short[]
            {
                0,1,2, 0,2,3, // -Z
                4,6,5, 4,7,6, // +Z
                0,3,7, 0,7,4, // -X
                1,5,6, 1,6,2, // +X
                0,4,5, 0,5,1, // -Y
                3,2,6, 3,6,7  // +Y
            };

            _vb = new VertexBuffer(graphics, typeof(VertexPositionColor), verts.Length, BufferUsage.WriteOnly);
            _vb.SetData(verts);

            _ib = new IndexBuffer(graphics, IndexElementSize.SixteenBits, idx.Length, BufferUsage.WriteOnly);
            _ib.SetData(idx);
        }

        public void Draw(GameTime gameTime, BasicEffect effect,
            Matrix world, Matrix view, Matrix projection,
            GraphicsDevice graphics)
        {
            // Spin to reveal winding/culling issues (keep CullNone while learning)
            _localWorld *= Matrix.CreateRotationY(0.01f) * Matrix.CreateRotationX(0.005f);

            effect.World = _localWorld * world;
            effect.View = view;
            effect.Projection = projection;
            effect.VertexColorEnabled = true;

            graphics.SetVertexBuffer(_vb);
            graphics.Indices = _ib;

            foreach (var pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                graphics.DrawIndexedPrimitives(PrimitiveType.TriangleList,0, 0, 12);
            }
        }
    }
}