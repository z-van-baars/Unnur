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
        public AABB Aabb;
        public Texture2D Image;
        public List<Point> OccupiedTiles;

        public Entity(Vector2 dimensions, Vector2 coordinates, Vector2 aabbDimensions)
        {
            this.coordinates = coordinates;
            this.dimensions = dimensions;
            hasPhysics = false;
            Vector2 aabbCoordinates = new Vector2(coordinates.X + (dimensions.X - aabbDimensions.X) / 2, coordinates.Y + (dimensions.Y - aabbDimensions.Y) / 2);
            Aabb = new AABB(aabbDimensions, aabbCoordinates);
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
        public List<Point> GetOccupiedTiles()
        {
            List<Point> occupiedTiles = new List<Point>();
            Point topLeftTile = new Point((int)Math.Floor(GetLeft() / 32), (int)Math.Floor(GetTop() / 32));
            Point topRightTile = new Point((int)Math.Floor((GetRight() - 1) / 32), topLeftTile.Y);
            Point bottomLeftTile = new Point(topLeftTile.X, (int)Math.Floor((GetBottom() - 1) / 32));
            Point bottomRightTile = new Point(topRightTile.X, bottomLeftTile.Y);
            for (int y = topLeftTile.Y; y <= bottomLeftTile.Y; y++)
            {
                for (int x = topLeftTile.X; x <= topRightTile.X; x++)
                {
                    occupiedTiles.Add(new Point(x, y));
                }
            }
            return occupiedTiles;
        }
        public void SetOccupiedTiles()
        {
            OccupiedTiles = GetOccupiedTiles();
        }

    }
}
