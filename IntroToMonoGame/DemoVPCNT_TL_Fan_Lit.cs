using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IntroToMonoGame
{
    public class DemoVPCNT_TL_Fan_Lit
    {
        private Color color;
        private VertexPositionColorNormalTexture[] _verts;
        public void Initialize()
        {
            VertexPositionColorNormalTexture fanCentreTop
                 = new VertexPositionColorNormalTexture(
                     0.5f * Vector3.UnitY,
                     color,
                     Vector3.UnitZ,
                     Vector2.Zero);

            VertexPositionColorNormalTexture fanCentreBottom
                 = new VertexPositionColorNormalTexture(
                     -0.5f * Vector3.UnitY,
                     color,
                     Vector3.UnitZ,
                     Vector2.Zero);

            _verts = new[]
            {
                //(+X blade - top)
                fanCentreTop,
                    new VertexPositionColorNormalTexture(
                     new Vector3(1,0.5f,0),
                     color,
                     Vector3.UnitZ,
                     Vector2.UnitX),  //1
                fanCentreBottom,

                //(+X blade - bottom)
                fanCentreBottom,
                     new VertexPositionColorNormalTexture(
                     new Vector3(1,0.5f,0),
                     color,
                     Vector3.UnitZ,
                     Vector2.UnitX), //1
                 new VertexPositionColorNormalTexture(
                     new Vector3(1,-0.5f,0),
                     color,
                     Vector3.UnitZ,
                     Vector2.One),  //2
            };

        }

        public void Draw(GameTime gameTime, BasicEffect effect,
          Matrix world, Matrix view, Matrix projection,
         GraphicsDevice graphics)
        {

        }
    }
}
