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

        public PhysicalEntity(Vector2 dimensions, Vector2 coordinates, Vector2 aabbDimensions, Scene currentScene) : base(dimensions, coordinates, aabbDimensions, currentScene)
        {

        }
        public void ApplyGravity()
        {
            int maxSpeed = 11;
            Vector2 newVelocity = new Vector2((float)Velocity.X / 2, Math.Min(maxSpeed, Velocity.Y + 1));
            Velocity = newVelocity;
        }
        public virtual void Update(KeyboardState keyState, MouseState mouseState, Collision collision, Scene currentScene)
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
            OnGround = IsOnGround(collision, currentScene);
            ResetOccupiedTiles(currentScene);

        }
        public bool IsOnGround(Collision collision, Scene currentScene)
        {
            // var center = new Vector2(coordinates.X + Aabb.GetWidth() / 2, coordinates.Y + Aabb.GetHeight() / 2);
            var bottomLeft = new Vector2(Aabb.GetLeft(), Aabb.GetBottom());
            var bottomRight = new Vector2(Aabb.GetRight(), Aabb.GetBottom());
            List<Entity> localCollideables = new List<Entity>();
            /// we add 32 because that's the width of one tile
            for (var checkedTile = bottomLeft; ; checkedTile.X += 32)
            {
                // skip back to the closest tile if we're past the edge of the test line
                checkedTile.X = Math.Min(checkedTile.X, bottomRight.X);
                Point tileIndex = new Point((int)(checkedTile.X / 32), (int)(bottomLeft.Y / 32));
                foreach (Entity localEntity in currentScene.GetTile(tileIndex).GetLocalEntities())
                {
                    if (localEntity.IsCollideable())
                    {
                        localCollideables.Add(localEntity);
                    }
                }

                if (checkedTile.X >= bottomRight.X)
                {
                    break;
                }
            }
            for (var x = bottomLeft.X; x <= bottomRight.X; x++)
            {
                foreach (Entity collideableEntity in localCollideables)
                {
                    if (collision.PointCollisionCheck(new Vector2(x, bottomLeft.Y + 1), collideableEntity))
                    {
                        return true;
                    }
                }
            }
            return false;



        }
    }
}
