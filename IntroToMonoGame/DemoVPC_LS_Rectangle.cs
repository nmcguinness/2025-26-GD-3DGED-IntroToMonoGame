using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IntroToMonoGame
{
    public class DemoPrimitiveTypeRect
    {
        private VertexPositionColor[] _verts;
        public void Initialize()
        {
            _verts = new VertexPositionColor[5];

            //how big is a vertexpositioncolor entry?
            // position - 3 x 4 bytes (float) = 12 bytes
            // color - 3 x 4 bytes (float) = 12 bytes

            //rectangle
            _verts[0] = new VertexPositionColor(
                new Vector3(0.5f, 0.5f, 0), Color.Red); //top right
            _verts[1] = new VertexPositionColor(
               new Vector3(0.5f, -0.5f, 0), Color.Green);  //bottom right
            _verts[2] = new VertexPositionColor(
                new Vector3(-0.5f, -0.5f, 0), Color.Blue);  //bottom left
            _verts[3] = new VertexPositionColor(
               new Vector3(-0.5f, 0.5f, 0), Color.Yellow);  //top left
            _verts[4] = new VertexPositionColor(
                new Vector3(0.5f, 0.5f, 0), Color.Red);    //top right
         
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
                    PrimitiveType.LineStrip,
                    _verts,
                   0, 4); //4-sides and start at _verts[0]
            }
        }
    }
}
