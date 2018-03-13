using SFML.Graphics;
using SFML.Window;
using SFML.System;

namespace ITI_First_Game
{
    class Tilemap : Drawable
    {
        private Texture _tileset; // Our tileset
        private VertexArray _vertexArray; // What we will draw and generate based on a map file

        private int _width;
        private int _height;
        private float _tileTextureDimension; // How big a tile is in a texture (generally 64*64)
        private float _tileWorldDimension; // In which size one tile will be drawn to the screen (as we wish)

        public Tilemap(Texture tileset, int width, int height, float tileTextureDimension, float tileWorldDimension)
        {
            _tileset = tileset;

            _width = width;
            _height = height;
            _tileTextureDimension = tileTextureDimension;
            _tileWorldDimension = tileWorldDimension;

            _vertexArray = new VertexArray(PrimitiveType.Quads, (uint)(width * height * 4));

            Tile _tile = new Tile(0, 1, Color.White); // It takes the first tile in the tileset (0 for the x * 64 and 1 for the y * 64)
            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    AddTileVertices(_tile, new Vector2f((float)x, (float)y));
                }
            }
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            states.Texture = _tileset;
            target.Draw(_vertexArray, states);
        }

        private void AddTileVertices(Tile tile, Vector2f position) // Add 4 vertices to our tiles
        {
            _vertexArray.Append(new Vertex((new Vector2f(0.0f, 0.0f) + position) * _tileWorldDimension,
                new Vector2f(_tileTextureDimension * tile._X, _tileTextureDimension * tile._Y)));

            _vertexArray.Append(new Vertex((new Vector2f(1.0f, 0.0f) + position) * _tileWorldDimension,
                new Vector2f(_tileTextureDimension * tile._X + _tileTextureDimension, _tileTextureDimension * tile._Y)));

            _vertexArray.Append(new Vertex((new Vector2f(1.0f, 1.0f) + position) * _tileWorldDimension,
                new Vector2f(_tileTextureDimension * tile._X + _tileTextureDimension, _tileTextureDimension * tile._Y + _tileTextureDimension)));

            _vertexArray.Append(new Vertex((new Vector2f(0.0f, 1.0f) + position) * _tileWorldDimension,
                new Vector2f(_tileTextureDimension * tile._X, _tileTextureDimension * tile._Y + _tileTextureDimension)));
        }
    }
}
