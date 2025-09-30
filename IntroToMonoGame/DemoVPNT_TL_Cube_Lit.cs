using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IntroToMonoGame
{
    public class DemoVPNT_TL_Cube_Lit
    {
        private VertexPositionNormalTexture[] _verts;
        private Texture2D _texture;

        public DemoVPNT_TL_Cube_Lit(Texture2D texture)
        {
            _texture = texture;
        }

        public void InitializeVert()
        {
            var hL = 0.5f; //half-length/width/depth

            _verts = new[]
            {
                //front-top-left
                new VertexPositionNormalTexture(
                   new Vector3(-hL, hL, hL), Vector3.UnitZ, new Vector2(0,0)), //1
                 new VertexPositionNormalTexture(
                   new Vector3(hL, hL, hL), Vector3.UnitZ, new Vector2(1,0)), //2
                  new VertexPositionNormalTexture(
                   new Vector3(-hL, -hL, hL), Vector3.UnitZ, new Vector2(0,1)), //4

                  //front-bottom-right         
                 new VertexPositionNormalTexture(
                   new Vector3(hL, hL, hL), Vector3.UnitZ, new Vector2(1,0)), //2
                 new VertexPositionNormalTexture(
                   new Vector3(hL, -hL, hL), Vector3.UnitZ, new Vector2(1,1)), //3
                  new VertexPositionNormalTexture(
                   new Vector3(-hL, -hL, hL), Vector3.UnitZ, new Vector2(0,1)), //4

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

            //RasterizerState rsState = new RasterizerState();
            //rsState.CullMode = CullMode.None;      
            //rsState.FillMode = FillMode.WireFrame;
            //graphics.RasterizerState = rsState;

            foreach (var pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                graphics.DrawUserPrimitives(
                    PrimitiveType.TriangleList, //all separate triangles
                    _verts,
                   0, 2); //cube = 6 sides = 2x6 triangles
            }
        }
    }
}
