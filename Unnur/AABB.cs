using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unnur
{
    class AABB
    {
        private Vector2 dimensions;
        private Vector2 coordinates;
        public Texture2D RenderBox;
        public AABB(Vector2 dimensions, Vector2 coordinates)
        {
            this.dimensions = dimensions;
            this.coordinates = coordinates;

        }
        public void SetPosition(Vector2 newCoordinates)
        {
            coordinates = newCoordinates;
        }
        public float GetWidth()
        {
            return dimensions.X;
        }
        public float GetHeight()
        {
            return dimensions.Y;
        }
        public Vector2 GetPos()
        {
            return coordinates;
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
