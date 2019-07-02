using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unnur
{
    class Dungeon
    {
        private Point dimensions;
        private List<Entity> walls = new List<Entity>();
        private List<Entity> units = new List<Entity>();

        public Dungeon()
        {
            Random random = new Random();
            
            dimensions = new Point(1900, 900);
            Wall floor = new Wall(new Vector2(dimensions.X, 32), new Vector2(0, dimensions.Y - 32));
            Wall wall2 = new Wall(new Vector2(32, 32), new Vector2(random.Next(128, dimensions.X - 32), dimensions.Y - 64));

            walls.Add(floor);
            walls.Add(wall2);

        }

        public List<Entity> GetWalls()
        {
            return walls;
        }
        
    }
}
