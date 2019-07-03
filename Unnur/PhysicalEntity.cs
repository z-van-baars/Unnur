using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unnur
{
    class PhysicalEntity : MovingEntity
    {


        public PhysicalEntity(Vector2 dimensions, Vector2 coordinates, Vector2 aabbDimensions) : base(dimensions, coordinates, aabbDimensions)
        {

        }
        public void ApplyGravity()
        {
            int maxSpeed = 11;
            Vector2 newVelocity = new Vector2((float)Velocity.X / 2, Math.Min(maxSpeed, Velocity.Y + 1));
            Velocity = newVelocity;
        }
    }
}
