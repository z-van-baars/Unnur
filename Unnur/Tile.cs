using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unnur
{
    class Tile
    {
        private Point coordinates;
        private Point dimensions = new Point(32, 32);
        public Tile(Point coordinates)
        {
            this.coordinates = coordinates;
        }
        public Point GetPos()
        {
            return coordinates;
        }
        public int GetWidth()
        {
            return dimensions.X;
        }
        public int GetHeight()
        {
            return dimensions.Y;
        }
    }
}
