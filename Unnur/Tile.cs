using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unnur
{
    class Tile
    {
        private Point coordinates;
        private Point dimensions = new Point(32, 32);
        private List<Entity> localEntities = new List<Entity>();
        public Tile(Point coordinates)
        {
            this.coordinates = coordinates;
        }
        public Point GetPos()
        {
            return coordinates;
        }
        public int GetWidth()
        {
            return dimensions.X;
        }
        public int GetHeight()
        {
            return dimensions.Y;
        }
        public void AddMember(Entity newMember)
        {
            localEntities.Add(newMember);
        }
        public void RemoveMember(Entity oldMember)
        {
            localEntities.Remove(oldMember);
        }
        public List<Entity> GetLocalEntities()
        {
            return localEntities;
        }
    }
}
