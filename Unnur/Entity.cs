using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unnur
{
    class Entity
    {
        private Vector2 coordinates;
        private Vector2 dimensions;
        public Texture2D Image;

        public Entity(Vector2 dimensions, Vector2 coordinates)
        {
            this.coordinates = coordinates;
            this.dimensions = dimensions;
        }
        public float GetWidth()
        {
            return dimensions.X;
        }
        public float GetHeight()
        {
            return dimensions.Y;
        }
        public float GetTop()
        {
            return coordinates.Y;
        }
        public float GetRight()
        {
            return coordinates.X + dimensions.X;
        }
        public float GetLeft()
        {
            return coordinates.X;
        }
        public float GetBottom()
        {
            return coordinates.Y + dimensions.Y;
        }
    }
}
