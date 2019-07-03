using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unnur
{
    class PhysicalEntity : MovingEntity
    {
        public enum PhysicalObjectState
        {
            Rest,
            Fall
        }
        public PhysicalObjectState CurrentState = PhysicalObjectState.Rest;
        private int maxSpeed = 11;

        public PhysicalEntity(Vector2 dimensions, Vector2 coordinates, Vector2 aabbDimensions) : base(dimensions, coordinates, aabbDimensions)
        {

        }
        public void ApplyGravity()
        {
            int maxSpeed = 11;
            Vector2 newVelocity = new Vector2((float)Velocity.X / 2, Math.Min(maxSpeed, Velocity.Y + 1));
            Velocity = newVelocity;
        }
        public virtual void Update(KeyboardState keyState, MouseState mouseState)
        {
            switch (CurrentState)
            {
                case PhysicalObjectState.Rest:
                    SetVelocity(0, 0);
                    /// some spritesheet setting logic should go here too
                    if (!OnGround)
                    {
                        CurrentState = PhysicalObjectState.Fall;
                        break;
                    }
                    break;
                case PhysicalObjectState.Fall:
                    /// gravity thingus dingus
                    /// 
                    /// Animation timing stuff goes here
                    SetVelocity(Velocity.X, Math.Min(maxSpeed, Velocity.Y + 1));

                    //if we hit the ground
                    if (OnGround)
                    {
                        //if there's no movement change state to standing

                        CurrentState = PhysicalObjectState.Rest;
                        SetVelocity(Velocity.X, 0);
                    }
                    break;
            }

            UpdatePhysics();

            if (OnGround && !WasOnGround)
            {
                /// play landing sound effect
            }
            if (!WasAtCeiling && AtCeiling
                || !PushedLeftWall && PushesLeftWall
                || !PushedRightWall && PushesRightWall)
            {
                /// play bumping sound if it's different from planding sound
            }
        }
    }
}
