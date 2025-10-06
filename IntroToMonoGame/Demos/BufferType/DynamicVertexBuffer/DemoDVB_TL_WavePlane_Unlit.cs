using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace IntroToMonoGame
{
    /// <summary>
    /// Animated ocean-like triangle plane:
    /// DynamicVertexBuffer (per-frame Y updates) + IndexBuffer (static triangles).
    /// </summary>
    public class DemoDVB_TL_WavePlane_Unlit
    {
        private DynamicVertexBuffer _dvb;
        private IndexBuffer _ib;

        // Grid settings (quads = GridX * GridZ)
        private const int GridX = 64;   // columns of quads
        private const int GridZ = 64;   // rows of quads
        private const float Spacing = 0.25f; // world units between vertices

        private int _vertexCount;
        private int _primitiveCount;
        private float _time;

        // Wave params
        private const float Amp1 = 0.25f;  // amplitude of primary wave
        private const float Amp2 = 0.15f;  // amplitude of secondary wave
        private const float Freq1 = 1.2f;  // spatial frequency X
        private const float Freq2 = 0.8f;  // spatial frequency Z
        private const float Speed1 = 1.6f; // temporal speed
        private const float Speed2 = 1.1f;

        public void Initialize(GraphicsDevice graphics)
        {
            // Total vertices = (GridX+1) * (GridZ+1)
            _vertexCount = (GridX + 1) * (GridZ + 1);
            _primitiveCount = GridX * GridZ * 2; // two tris per quad

            // Static index buffer for the whole plane
            var indices = new short[_primitiveCount * 3];
            int i = 0;
            for (int z = 0; z < GridZ; z++)
            {
                for (int x = 0; x < GridX; x++)
                {
                    int v0 = z * (GridX + 1) + x;
                    int v1 = v0 + 1;
                    int v2 = (z + 1) * (GridX + 1) + x;
                    int v3 = v2 + 1;

                    // tri A
                    indices[i++] = (short)v0;
                    indices[i++] = (short)v1;
                    indices[i++] = (short)v2;
                    // tri B
                    indices[i++] = (short)v2;
                    indices[i++] = (short)v1;
                    indices[i++] = (short)v3;
                }
            }
            _ib = new IndexBuffer(graphics, IndexElementSize.SixteenBits, indices.Length, BufferUsage.WriteOnly);
            _ib.SetData(indices);

            // Dynamic vertex buffer (positions & colors updated every frame)
            _dvb = new DynamicVertexBuffer(graphics, typeof(VertexPositionColor), _vertexCount, BufferUsage.WriteOnly);

            // Prime the buffer once so it draws on first frame
            UploadVertices(graphics, 0f);
        }

        public void Draw(GameTime gameTime, BasicEffect effect,
            Matrix world, Matrix view, Matrix projection,
            GraphicsDevice graphics)
        {
            _time += (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Update vertex positions & colors each frame
            UploadVertices(graphics, _time);

            // Effect
            effect.World = world;
            effect.View = view;
            effect.Projection = projection;
            effect.VertexColorEnabled = true;

            // Bind buffers & draw
            graphics.SetVertexBuffer(_dvb);
            graphics.Indices = _ib;

            foreach (var pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                graphics.DrawIndexedPrimitives(
                    PrimitiveType.TriangleList,
                    0,
                    0,
                    _primitiveCount);
            }
        }

        private void UploadVertices(GraphicsDevice graphics, float t)
        {
            var verts = new VertexPositionColor[_vertexCount];

            // Center the plane around origin
            float halfW = GridX * Spacing * 0.5f;
            float halfD = GridZ * Spacing * 0.5f;

            int k = 0;
            for (int z = 0; z <= GridZ; z++)
            {
                for (int x = 0; x <= GridX; x++)
                {
                    float wx = x * Spacing - halfW;
                    float wz = z * Spacing - halfD;

                    // Two superimposed traveling waves (x & z directions)
                    float y =
                        Amp1 * (float)Math.Sin(Freq1 * wx + Speed1 * t) +
                        Amp2 * (float)Math.Cos(Freq2 * wz - Speed2 * t);

                    // Simple color by height (nice visual feedback)
                    float h = MathHelper.Clamp((y + (Amp1 + Amp2)) / (2f * (Amp1 + Amp2)), 0f, 1f);
                    var col = new Color(
                        (byte)MathHelper.Lerp(10, 40, h),   // deep blue-ish
                        (byte)MathHelper.Lerp(80, 130, h),  // green
                        (byte)MathHelper.Lerp(130, 220, h)); // light/cap

                    verts[k++] = new VertexPositionColor(new Vector3(wx, y, wz), col);
                }
            }

            // Full rewrite each frame
            _dvb.SetData(verts, 0, _vertexCount, SetDataOptions.Discard);
        }
    }
}
