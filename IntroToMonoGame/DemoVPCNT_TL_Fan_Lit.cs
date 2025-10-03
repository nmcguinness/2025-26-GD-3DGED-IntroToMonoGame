using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;

namespace IntroToMonoGame
{
    public class DemoVPCNT_TL_Fan_Lit
    {
       
        private VertexPositionColorNormalTexture[] _verts;
        private Color _vertexColor;
        private Texture2D _texture;
        private Vector3 _diffuseColor;
        private float _specularPower;
        private Vector3 _specularColor;

        public DemoVPCNT_TL_Fan_Lit(Color vertexColor, Texture2D texture, 
            Vector3 diffuseColor, float specularPower, Vector3 specularColor)
        {
            _vertexColor = vertexColor;
            _texture = texture;
            _diffuseColor = diffuseColor;
            _specularPower = specularPower;
            _specularColor = specularColor;
        }

        public void Initialize()
        {
            VertexPositionColorNormalTexture fanCentreTop
                 = new VertexPositionColorNormalTexture(
                     0.5f * Vector3.UnitY,  //(0,1,0) x 0.5f
                     _vertexColor,
                     Vector3.UnitZ,
                     Vector2.Zero);

            VertexPositionColorNormalTexture fanCentreBottom
                 = new VertexPositionColorNormalTexture(
                     -0.5f * Vector3.UnitY,  //3 x 4bytes
                     _vertexColor, //3 x 4bytes
                     Vector3.UnitZ, //3 x 4bytes
                     Vector2.UnitY); //2 x 4bytes

            _verts = new[]
            {
                //(+X blade - top)
                fanCentreTop,
                    new VertexPositionColorNormalTexture(
                     new Vector3(1,0.5f,0),
                     _vertexColor,
                     Vector3.UnitZ,
                     Vector2.UnitX),  //1
                fanCentreBottom,

                //(+X blade - bottom)
                fanCentreBottom,
                     new VertexPositionColorNormalTexture(
                     new Vector3(1,0.5f,0),
                     _vertexColor,
                     Vector3.UnitZ,
                     Vector2.UnitX), //1
                 new VertexPositionColorNormalTexture(
                     new Vector3(1,-0.5f,0),
                     _vertexColor,
                     Vector3.UnitZ,
                     Vector2.One),  //2
            };

        }

        public void Draw(GameTime gameTime, BasicEffect effect,
          Matrix world, Matrix view, Matrix projection,
         GraphicsDevice graphics)
        {
            effect.World = world;
            effect.View = view;
            effect.Projection = projection;
            effect.Texture = _texture;
            effect.DiffuseColor = _diffuseColor;
            effect.SpecularPower = _specularPower;
            effect.SpecularColor = _specularColor;

            foreach (var pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                graphics.DrawUserPrimitives(
                    Microsoft.Xna.Framework.Graphics.PrimitiveType.TriangleList, //all separate triangles
                    _verts,
                   0, 2); //fan on +x has 2 triangles
            }
        }
    }
}
