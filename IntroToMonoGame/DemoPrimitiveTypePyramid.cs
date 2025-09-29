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

            //peak
            _verts[0] = new VertexPositionColor(
                new Vector3(0, 1, 0), Color.Red);
            
            //line 1s-1e - base front
            _verts[1] = new VertexPositionColor(
                new Vector3(-0.5f, 0, 0.5f), Color.Yellow);
            _verts[2] = new VertexPositionColor(
               new Vector3(0.5f, 0, 0.5f), Color.Yellow);

            //line 2s-2e - base right
            _verts[3] = new VertexPositionColor(
                new Vector3(0.5f, 0, 0.5f), Color.Yellow);
            _verts[4] = new VertexPositionColor(
               new Vector3(0.5f, 0, -0.5f), Color.Yellow);

            //line 3s-3e - base back
            _verts[5] = new VertexPositionColor(
                new Vector3(0.5f, 0, -0.5f), Color.Yellow);
            _verts[6] = new VertexPositionColor(
               new Vector3(-0.5f, 0, -0.5f), Color.Yellow);

            //line 4s-4e - base left
            _verts[7] = new VertexPositionColor(
                new Vector3(-0.5f, 0, -0.5f), Color.Yellow);
            _verts[8] = new VertexPositionColor(
               new Vector3(-0.5f, 0, 0.5f), Color.Yellow);



        }

        public void Draw(GameTime gameTime)
        {

        }
    }
}
