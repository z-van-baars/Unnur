using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unnur
{
    
    class MoveableEntity : CollideableEntity
    {
        protected Vector2 velocity;
        public MoveableEntity(Vector2 dimensions, Vector2 coordinates) : base(dimensions, coordinates)
        {
            hasPhysics = true;
        }

        public void SetVelocity(double xTranslation, double yTranslation)
        {
            Vector2 newVelocity = new Vector2((float)(velocity.X + xTranslation), (float)(velocity.Y + yTranslation));
            velocity = newVelocity;
        }
        public void Move()
        {
            Vector2 newCoordinates = new Vector2((int)(coordinates.X + velocity.X), (int)(coordinates.Y + velocity.Y));
            coordinates = newCoordinates;
        }
        public void ApplyGravity()
        {
            int maxSpeed = 11;
            Vector2 newVelocity = new Vector2(velocity.X, Math.Min(maxSpeed, velocity.Y + 1));
            velocity = newVelocity;
        }
    }
}
