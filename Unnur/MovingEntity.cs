using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unnur
{
    
    class MovingEntity : CollideableEntity
    {
        public bool PushedRightWall;
        public bool PushesRightWall;

        public bool PushedLeftWall;
        public bool PushesLeftWall;

        public bool WasOnGround;
        public bool OnGround;

        public bool WasAtCeiling;
        public bool AtCeiling;
        public Vector2 PrevCoordinates;
        public Vector2 Velocity;
        public Vector2 PrevVelocity;
        public Vector2 Scale;

        
        public MovingEntity(Vector2 dimensions, Vector2 coordinates, Vector2 aabbDimensions, Scene currentScene) : base(dimensions, coordinates, aabbDimensions, currentScene)
        {
            hasPhysics = true;
        }
        public virtual void UpdatePhysics()
        {
            PrevCoordinates = coordinates;
            PrevVelocity = Velocity;

            WasOnGround = OnGround;
            PushedRightWall = PushesRightWall;
            PushedLeftWall = PushesLeftWall;
            WasAtCeiling = AtCeiling;

            coordinates += Velocity;
            Aabb.SetPosition(coordinates);
            Aabb.Recenter();

        }
        public void SetVelocity(double xTranslation, double yTranslation)
        {
            Vector2 newVelocity = new Vector2((float)(xTranslation), (float)(yTranslation));
            Velocity = newVelocity;
        }
        public void Move()
        {
            Vector2 newCoordinates = new Vector2((int)(coordinates.X + Velocity.X), (int)(coordinates.Y + Velocity.Y));
            coordinates = newCoordinates;
            Aabb.SetPosition(coordinates);
        }
        public void ResetVelocity()
        {
            Velocity = new Vector2(0, 0);
        }

        public Vector2 GetVelocity()
        {
            return Velocity;
        }
        public virtual void Deflect(Entity impactSurface)
        {
            Vector2 unitVector = new Vector2(Velocity.X, Velocity.Y);
            unitVector.Normalize();
            float newXVelocity = (float)(-(Velocity.X + unitVector.X));
            float newYVelocity = (float)(-(Velocity.Y + unitVector.Y));
            Velocity = new Vector2(newXVelocity, newYVelocity);
        }
    }
}
