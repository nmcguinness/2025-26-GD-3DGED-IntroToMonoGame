using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IntroToMonoGame
{
    public class DemoVPC_TL_Triangle
    {
        private VertexPositionColor[] _verts;
        public void Initialize()
        {
            _verts = new VertexPositionColor[5];

            //rectangle
            _verts[0] = new VertexPositionColor(
                new Vector3(0, 0.5f, 0), Color.Red); //top 
            _verts[1] = new VertexPositionColor(
               new Vector3(0.5f, 0, 0), Color.Green);  //bottom right
            _verts[2] = new VertexPositionColor(
                new Vector3(-0.5f, 0, 0), Color.Blue);  //bottom left
        }

        public void Draw(GameTime gameTime,
            BasicEffect effect, Matrix world,
            Matrix view, Matrix projection,
            GraphicsDevice graphics)
        {
            effect.World = world;
            effect.View = view;
            effect.Projection = projection;

            
            foreach (var pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                graphics.DrawUserPrimitives(
                    PrimitiveType.TriangleList,
                    _verts,
                   0, 1); //1 primitive = 1 triangle
            }
        }
    }
}
