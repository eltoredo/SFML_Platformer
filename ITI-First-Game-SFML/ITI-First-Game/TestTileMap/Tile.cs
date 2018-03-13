using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;

namespace ITI_First_Game
{
    class Tile
    {
        // Position in the tileset
        public int _X;
        public int _Y;

        public Color _color; // Color of the tile in the map file

        public Tile(int X, int Y, Color color)
        {
            _X = X;
            _Y = Y;

            _color = color;
        }
    }
}
