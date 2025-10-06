using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace IntroToMonoGame
{
    public class DemoDVB_LS_WaveLine
    {
        private DynamicVertexBuffer _dvb;
        private int _vertsCount = 256;
        private float _t;

        public void Initialize(GraphicsDevice graphics)
        {
            _dvb = new DynamicVertexBuffer(graphics, typeof(VertexPositionColor), 
                _vertsCount, BufferUsage.WriteOnly);

            //no point in setting data here
        }

        public void Draw(GameTime gameTime, BasicEffect effect,
            Matrix world, Matrix view, Matrix projection,
            GraphicsDevice graphics)
        {
            _t += (float)gameTime.ElapsedGameTime.TotalSeconds;

            var verts = new VertexPositionColor[_vertsCount];
            for (int i = 0; i < _vertsCount; i++)
            {
                float x = MathHelper.Lerp(-8f, 8f, i / (_vertsCount - 1f));
                float y = (float)Math.Sin(x * 1.2f + _t * 2.0f) * 0.1f;
                var c = new Color((byte)(127 + 127 * Math.Sin(x + _t)), (byte)200, (byte)255);
                verts[i] = new VertexPositionColor(new Vector3(x, y, 0), c);
            }

            _dvb.SetData(verts, 0, _vertsCount, SetDataOptions.Discard);

            effect.World = world;
            effect.View = view;
            effect.Projection = projection;
            effect.VertexColorEnabled = true;

            graphics.SetVertexBuffer(_dvb);

            foreach (var pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                graphics.DrawPrimitives(PrimitiveType.LineStrip, 0, _vertsCount - 1);
            }
        }
    }
}