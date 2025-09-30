using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IntroToMonoGame
{
    public class DemoVPC_LL_Pyramid
    {
        private VertexPositionColor[] _verts;
        public void InitializeVerts()
        {
            _verts = new VertexPositionColor[16];
           
            //how big is a vertexpositioncolor entry?
             // position - 3 x 4 bytes (float) = 12 bytes
             // color - 3 x 4 bytes (float) = 12 bytes

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
                new Vector3(0, 0.5f, 0), Color.Red);
            _verts[9] = new VertexPositionColor(
               new Vector3(0.5f, 0, 0.5f), Color.Yellow);

            //line 6s-6e - back right
            _verts[10] = new VertexPositionColor(
                new Vector3(0, 0.5f, 0), Color.Red);
            _verts[11] = new VertexPositionColor(
               new Vector3(0.5f, 0, -0.5f), Color.Yellow);

            //line 7s-7e - back left
            _verts[12] = new VertexPositionColor(
                new Vector3(0, 0.5f, 0), Color.Red);
            _verts[13] = new VertexPositionColor(
               new Vector3(-0.5f, 0, -0.5f), Color.Yellow);

            //line 8s-8e - front left
            _verts[14] = new VertexPositionColor(
                new Vector3(0, 0.5f, 0), Color.Red);
            _verts[15] = new VertexPositionColor(
               new Vector3(-0.5f, 0, 0.5f), Color.Yellow);
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
                    PrimitiveType.LineList, 
                    _verts, 
                   0, 8);
            }
        }
    }
}
