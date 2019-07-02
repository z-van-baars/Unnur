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
        protected Vector2 coordinates;
        protected Vector2 dimensions;
        protected bool hasPhysics;
        protected bool isCollideable;
        protected bool isAnimated;
        public Texture2D Image;

        public Entity(Vector2 dimensions, Vector2 coordinates)
        {
            this.coordinates = coordinates;
            this.dimensions = dimensions;
            hasPhysics = false;
        }
        public float GetWidth()
        {
            return dimensions.X;
        }
        public float GetHeight()
        {
            return dimensions.Y;
        }
        public bool HasPhysics()
        {
            return hasPhysics;
        }
        public bool IsCollideable()
        {
            return isCollideable;
        }
        public bool IsAnimated()
        {
            return isAnimated;
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
