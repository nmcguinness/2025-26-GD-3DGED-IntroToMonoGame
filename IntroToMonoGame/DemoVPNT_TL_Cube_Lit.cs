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

        public void Initialize()
        {
            var hL = 0.5f; //half-length/width/depth

            _verts = new[]
            {
                //front-top-left (+Z face)
                new VertexPositionNormalTexture(
                    new Vector3(-hL,  hL,  hL), Vector3.UnitZ, new Vector2(0,0)), //1
                new VertexPositionNormalTexture(
                    new Vector3( hL,  hL,  hL), Vector3.UnitZ, new Vector2(1,0)), //2
                new VertexPositionNormalTexture(
                    new Vector3(-hL, -hL,  hL), Vector3.UnitZ, new Vector2(0,1)), //3

                //front-bottom-right (+Z face)
                new VertexPositionNormalTexture(
                    new Vector3( hL,  hL,  hL), Vector3.UnitZ, new Vector2(1,0)), //2
                new VertexPositionNormalTexture(
                    new Vector3( hL, -hL,  hL), Vector3.UnitZ, new Vector2(1,1)), //4
                new VertexPositionNormalTexture(
                    new Vector3(-hL, -hL,  hL), Vector3.UnitZ, new Vector2(0,1)), //3

                //back-top-left (-Z face)
                new VertexPositionNormalTexture(
                    new Vector3( hL,  hL, -hL), -Vector3.UnitZ, new Vector2(0,0)), //6
                new VertexPositionNormalTexture(
                    new Vector3(-hL,  hL, -hL), -Vector3.UnitZ, new Vector2(1,0)), //5
                new VertexPositionNormalTexture(
                    new Vector3( hL, -hL, -hL), -Vector3.UnitZ, new Vector2(0,1)), //8

                //back-bottom-right (-Z face)
                new VertexPositionNormalTexture(
                    new Vector3(-hL,  hL, -hL), -Vector3.UnitZ, new Vector2(1,0)), //5
                new VertexPositionNormalTexture(
                    new Vector3(-hL, -hL, -hL), -Vector3.UnitZ, new Vector2(1,1)), //7
                new VertexPositionNormalTexture(
                    new Vector3( hL, -hL, -hL), -Vector3.UnitZ, new Vector2(0,1)), //8

                //left-top-left (-X face)
                new VertexPositionNormalTexture(
                    new Vector3(-hL,  hL,  -hL), -Vector3.UnitX, new Vector2(0,0)), //5
                new VertexPositionNormalTexture(
                    new Vector3(-hL,  hL, hL), -Vector3.UnitX, new Vector2(1,0)), //1
                new VertexPositionNormalTexture(
                    new Vector3(-hL, -hL,  -hL), -Vector3.UnitX, new Vector2(0,1)), //7

                //left-bottom-right (-X face)
                new VertexPositionNormalTexture(
                    new Vector3(-hL,  hL, hL), -Vector3.UnitX, new Vector2(1,0)), //1
                new VertexPositionNormalTexture(
                    new Vector3(-hL, -hL, hL), -Vector3.UnitX, new Vector2(1,1)), //3
                new VertexPositionNormalTexture(
                    new Vector3(-hL, -hL,  -hL), -Vector3.UnitX, new Vector2(0,1)), //7

                //right-top-left (+X face)
                new VertexPositionNormalTexture(
                    new Vector3( hL,  hL, hL), Vector3.UnitX, new Vector2(0,0)), //2
                new VertexPositionNormalTexture(
                    new Vector3( hL,  hL,  -hL), Vector3.UnitX, new Vector2(1,0)), //6
                new VertexPositionNormalTexture(
                    new Vector3( hL, -hL, hL), Vector3.UnitX, new Vector2(0,1)), //4

                //right-bottom-right (+X face)
                new VertexPositionNormalTexture(
                    new Vector3( hL,  hL,  -hL), Vector3.UnitX, new Vector2(1,0)), //6
                new VertexPositionNormalTexture(
                    new Vector3( hL, -hL,  -hL), Vector3.UnitX, new Vector2(1,1)), //8
                new VertexPositionNormalTexture(
                    new Vector3( hL, -hL, hL), Vector3.UnitX, new Vector2(0,1)), //4

                //top-top-left (+Y face)
                new VertexPositionNormalTexture(
                    new Vector3(-hL,  hL, -hL), Vector3.UnitY, new Vector2(0,0)), //5
                new VertexPositionNormalTexture(
                    new Vector3( hL,  hL, -hL), Vector3.UnitY, new Vector2(1,0)), //6
                new VertexPositionNormalTexture(
                    new Vector3(-hL,  hL,  hL), Vector3.UnitY, new Vector2(0,1)), //1

                //top-bottom-right (+Y face)
                new VertexPositionNormalTexture(
                    new Vector3( hL,  hL, -hL), Vector3.UnitY, new Vector2(1,0)), //6
                new VertexPositionNormalTexture(
                    new Vector3( hL,  hL,  hL), Vector3.UnitY, new Vector2(1,1)), //2
                new VertexPositionNormalTexture(
                    new Vector3(-hL,  hL,  hL), Vector3.UnitY, new Vector2(0,1)), //1

                //bottom-top-left (-Y face)
                new VertexPositionNormalTexture(
                    new Vector3(-hL, -hL,  hL), -Vector3.UnitY, new Vector2(0,0)), //3
                new VertexPositionNormalTexture(
                    new Vector3( hL, -hL,  hL), -Vector3.UnitY, new Vector2(1,0)), //4
                new VertexPositionNormalTexture(
                    new Vector3(-hL, -hL, -hL), -Vector3.UnitY, new Vector2(0,1)), //7

                //bottom-bottom-right (-Y face)
                new VertexPositionNormalTexture(
                    new Vector3( -hL, -hL,  -hL), -Vector3.UnitY, new Vector2(0,1)), //7
                new VertexPositionNormalTexture(
                    new Vector3( hL, -hL, hL), -Vector3.UnitY, new Vector2(1,0)), //4
                new VertexPositionNormalTexture(
                    new Vector3(hL, -hL, -hL), -Vector3.UnitY, new Vector2(1,1)), //8
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
                   0, 12); //cube = 6 sides = 2x6 triangles
            }
        }
    }
}
