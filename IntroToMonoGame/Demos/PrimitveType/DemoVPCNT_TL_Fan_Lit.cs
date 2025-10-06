using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IntroToMonoGame.Demos.PrimitveType
{
    /// <summary>
    /// Draws a 4-blade fan using trianglelist and vertexbuffer
    /// where each blade is rotated 90 degrees around the Y-axis
    /// from the previous blade
    /// </summary>
    /// <see cref="http://rbwhitaker.wikidot.com/index-and-vertex-buffers"/>
    public class DemoVPCNT_TL_Fan_Lit
    {
       
        private VertexPositionColorNormalTexture[] _verts;
        private VertexBuffer _vertsBuffer;
        private Color _vertexColor;
        private Texture2D _texture;
        private Vector3 _diffuseColor;
        private int _specularPower;
        private Vector3 _specularColor;
        private CullMode _cullMode;
        private FillMode _fillMode;
        private RasterizerState _rsState;

        public DemoVPCNT_TL_Fan_Lit(Color vertexColor, 
            Texture2D texture, 
            Vector3 diffuseColor, 
            int specularPower, 
            Vector3 specularColor,
            CullMode cullMode,
            FillMode fillMode)
        {
            _vertexColor = vertexColor;
            _texture = texture;
            _diffuseColor = diffuseColor;
            _specularPower = specularPower;
            _specularColor = specularColor;

            _cullMode = cullMode;
            _fillMode = fillMode;
        }

        public void Initialize(GraphicsDevice graphics)
        {

            //initialize based on user-prefs passed via constructor
            _rsState = new RasterizerState();
            _rsState.CullMode = _cullMode;
            _rsState.FillMode = _fillMode;

            _verts = new[]
            {
                // =========================
                //  +X BLADE  (lies in XY)  → normal = +Z
                //  Tris: (centerTop → tipTop → centerBot), (centerBot → tipTop → tipBot)
                // =========================
                new VertexPositionColorNormalTexture(new Vector3(0f,  0.5f,  0f), _vertexColor,  Vector3.UnitZ, new Vector2(0f, 0f)), // centerTop (+Z)
                new VertexPositionColorNormalTexture(new Vector3(1f,  0.5f,  0f), _vertexColor,  Vector3.UnitZ, new Vector2(1f, 0f)), // tipTop
                new VertexPositionColorNormalTexture(new Vector3(0f, -0.5f,  0f), _vertexColor,  Vector3.UnitZ, new Vector2(0f, 1f)), // centerBot

                new VertexPositionColorNormalTexture(new Vector3(0f, -0.5f,  0f), _vertexColor,  Vector3.UnitZ, new Vector2(0f, 1f)), // centerBot
                new VertexPositionColorNormalTexture(new Vector3(1f,  0.5f,  0f), _vertexColor,  Vector3.UnitZ, new Vector2(1f, 0f)), // tipTop
                new VertexPositionColorNormalTexture(new Vector3(1f, -0.5f,  0f), _vertexColor,  Vector3.UnitZ, new Vector2(1f, 1f)), // tipBot

                // =========================
                //  -X BLADE  (lies in XY)  → normal = +Z
                // =========================
                new VertexPositionColorNormalTexture(new Vector3(0f,  0.5f,  0f), _vertexColor,  Vector3.UnitZ, new Vector2(0f, 0f)), // centerTop (+Z)
                new VertexPositionColorNormalTexture(new Vector3(-1f, 0.5f,  0f), _vertexColor,  Vector3.UnitZ, new Vector2(1f, 0f)), // tipTop
                new VertexPositionColorNormalTexture(new Vector3(0f, -0.5f,  0f), _vertexColor,  Vector3.UnitZ, new Vector2(0f, 1f)), // centerBot

                new VertexPositionColorNormalTexture(new Vector3(0f, -0.5f,  0f), _vertexColor,  Vector3.UnitZ, new Vector2(0f, 1f)), // centerBot
                new VertexPositionColorNormalTexture(new Vector3(-1f, 0.5f,  0f), _vertexColor,  Vector3.UnitZ, new Vector2(1f, 0f)), // tipTop
                new VertexPositionColorNormalTexture(new Vector3(-1f,-0.5f,  0f), _vertexColor,  Vector3.UnitZ, new Vector2(1f, 1f)), // tipBot

                // =========================
                //  +Z BLADE  (stands along +Z) → faces right → normal = +X
                // =========================
                new VertexPositionColorNormalTexture(new Vector3(0f,  0.5f,  0f), _vertexColor,  Vector3.UnitX, new Vector2(0f, 0f)), // centerTop (+X normal)
                new VertexPositionColorNormalTexture(new Vector3(0f,  0.5f,  1f), _vertexColor,  Vector3.UnitX, new Vector2(1f, 0f)), // tipTop (at +Z)
                new VertexPositionColorNormalTexture(new Vector3(0f, -0.5f,  0f), _vertexColor,  Vector3.UnitX, new Vector2(0f, 1f)), // centerBot (+X normal)

                new VertexPositionColorNormalTexture(new Vector3(0f, -0.5f,  0f), _vertexColor,  Vector3.UnitX, new Vector2(0f, 1f)), // centerBot
                new VertexPositionColorNormalTexture(new Vector3(0f,  0.5f,  1f), _vertexColor,  Vector3.UnitX, new Vector2(1f, 0f)), // tipTop
                new VertexPositionColorNormalTexture(new Vector3(0f, -0.5f,  1f), _vertexColor,  Vector3.UnitX, new Vector2(1f, 1f)), // tipBot

                // =========================
                //  -Z BLADE  (stands along -Z) → faces left → normal = -X
                // =========================
                new VertexPositionColorNormalTexture(new Vector3(0f,  0.5f,  0f), _vertexColor, -Vector3.UnitX, new Vector2(0f, 0f)), // centerTop (-X normal)
                new VertexPositionColorNormalTexture(new Vector3(0f,  0.5f, -1f), _vertexColor, -Vector3.UnitX, new Vector2(1f, 0f)), // tipTop (at -Z)
                new VertexPositionColorNormalTexture(new Vector3(0f, -0.5f,  0f), _vertexColor, -Vector3.UnitX, new Vector2(0f, 1f)), // centerBot (-X normal)

                new VertexPositionColorNormalTexture(new Vector3(0f, -0.5f,  0f), _vertexColor, -Vector3.UnitX, new Vector2(0f, 1f)), // centerBot
                new VertexPositionColorNormalTexture(new Vector3(0f,  0.5f, -1f), _vertexColor, -Vector3.UnitX, new Vector2(1f, 0f)), // tipTop
                new VertexPositionColorNormalTexture(new Vector3(0f, -0.5f, -1f), _vertexColor, -Vector3.UnitX, new Vector2(1f, 1f)), // tipBot
            };


            //reservation of space on the VRAM of the GFX card
            _vertsBuffer = new VertexBuffer(graphics,
                typeof(VertexPositionColorNormalTexture),
                _verts.Length, BufferUsage.WriteOnly);

            //loading/serializing the data to the GFX card
            graphics.SetVertexBuffer(_vertsBuffer);
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

            //set cull and fill
            graphics.RasterizerState = _rsState;

            //pointing the GFX card to the vertices ALREADY in VRAM
            graphics.SetVertexBuffer(_vertsBuffer);

            foreach (var pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                graphics.DrawUserPrimitives(
                    PrimitiveType.TriangleList, //all separate triangles
                    _verts,
                   0, 8); //fan on +x has 2 triangles
            }
        }
    }
}
