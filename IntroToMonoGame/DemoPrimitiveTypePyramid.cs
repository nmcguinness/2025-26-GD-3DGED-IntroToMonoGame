using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IntroToMonoGame
{
    public class DemoPrimitiveTypePyramid
    {
        private VertexPositionColor[] _verts;
        public void InitializeVerts()
        {
            _verts = new VertexPositionColor[16];
           
            //line 1s-1e - base front
            _verts[0] = new VertexPositionColor(
                new Vector3(-0.5f, 0, 0.5f), Color.Yellow);
            _verts[1] = new VertexPositionColor(
               new Vector3(0.5f, 0, 0.5f), Color.Yellow);

            //line 2s-2e - base right
            _verts[2] = new VertexPositionColor(
                new Vector3(0.5f, 0, 0.5f), Color.Yellow);
            _verts[3] = new VertexPositionColor(
               new Vector3(0.5f, 0, -0.5f), Color.Yellow);

            //line 3s-3e - base back
            _verts[4] = new VertexPositionColor(
                new Vector3(0.5f, 0, -0.5f), Color.Yellow);
            _verts[5] = new VertexPositionColor(
               new Vector3(-0.5f, 0, -0.5f), Color.Yellow);

            //line 4s-4e - base left
            _verts[6] = new VertexPositionColor(
                new Vector3(-0.5f, 0, -0.5f), Color.Yellow);
            _verts[7] = new VertexPositionColor(
               new Vector3(-0.5f, 0, 0.5f), Color.Yellow);

            //line 5s-5e - front right
            _verts[8] = new VertexPositionColor(
                new Vector3(0, 1, 0), Color.Red);
            _verts[9] = new VertexPositionColor(
               new Vector3(0.5f, 0, 0.5f), Color.Yellow);


        }

        public void Draw(GameTime gameTime)
        {

        }
    }
}
