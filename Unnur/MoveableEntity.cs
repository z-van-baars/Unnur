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
        public virtual void Deflect()
        {
            velocity = new Vector2(-velocity.X, -velocity.Y);
        }
    }
}
