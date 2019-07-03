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
        public MoveableEntity(Vector2 dimensions, Vector2 coordinates, Vector2 aabbDimensions) : base(dimensions, coordinates, aabbDimensions)
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
            Aabb.SetPosition(coordinates);
        }
        public void ResetVelocity()
        {
            velocity = new Vector2(0, 0);
        }

        public Vector2 GetVelocity()
        {
            return velocity;
        }
        public virtual void Deflect(Entity impactSurface)
        {
            float newXVelocity = (float)(-velocity.X * 0.5);
            float newYVelocity = (float)(-velocity.Y * 0.5);
            if (Math.Abs(newXVelocity) < 0.1)
            {
                newXVelocity = 0;
            }
            if (Math.Abs(newYVelocity) < 0.1)
            {
                newYVelocity = 0;
            }
            velocity = new Vector2(newXVelocity, newYVelocity);
        }
    }
}
