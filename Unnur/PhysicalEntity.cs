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
        private int maxSpeed = 1;

        public PhysicalEntity(Vector2 dimensions, Vector2 coordinates, Vector2 aabbDimensions, Scene currentScene) : base(dimensions, coordinates, aabbDimensions, currentScene)
        {

        }
        public void ApplyGravity()
        {
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
            var bottomLeft = new Vector2(Aabb.GetLeft(), Aabb.GetBottom());
            var bottomRight = new Vector2(Aabb.GetRight(), Aabb.GetBottom());

            int tileWidth = 32;

            int initialX = (int)bottomLeft.X / tileWidth;
            int numTiles = (int)(bottomRight.X - bottomLeft.X) / tileWidth;
            /// we add one here so that we can test the area immediately UNDER the AABB
            int yIndex = (int)(bottomLeft.Y + 1) / tileWidth;



            return Enumerable
                .Range(initialX, initialX + numTiles)                    // x indices
                .Select(x => currentScene.GetTile(new Point(x, yIndex))) // tiles in the x range
                .SelectMany(tile => tile.GetLocalEntities())             // entities in the tile
                .Where(entity => entity.IsCollideable())                 // collidable only
                .Any(entity => Enumerable                                // do they actually collide?
                    .Range((int)bottomLeft.X, (int)bottomRight.X)                 // x indices
                    .Select(x => new Vector2(x, bottomLeft.Y + 1)) // vector coords
                    .Any(v => collision.PointCollisionCheck(v, entity))        // collision check
                );
        }
    }
}
