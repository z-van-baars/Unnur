using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unnur
{
    class Collision
    {
        public Collision()
        {

        }
        public bool CollisionCheck(Entity entity1, Entity entity2, Vector2 velocity)
        {
            Vector2 center1 = GetCenterpoint(entity1, velocity);

            return CheckInside(
                center1,
                new Vector2(entity2.GetLeft() - entity1.GetWidth() / 2, entity2.GetTop() - entity1.GetHeight() / 2),
                new Vector2(entity2.GetWidth() + entity1.GetWidth() / 2, entity2.GetHeight() + entity1.GetHeight() / 2));
        }
        public bool CheckInside(Vector2 point, Vector2 origin, Vector2 dimensions)
        {
            return ((point.X >= origin.X)
                && (point.X <= origin.X + dimensions.X)
                && (point.Y >= origin.Y)
                && (point.Y <= origin.Y + dimensions.Y));
        }
        public Vector2 GetCenterpoint(Entity entity1, Vector2 velocity)
        {
            return new Vector2(
                entity1.GetPos().X + velocity.X + entity1.GetWidth() / 2,
                entity1.GetPos().Y + velocity.Y + entity1.GetHeight() / 2);
        }
    }
}
