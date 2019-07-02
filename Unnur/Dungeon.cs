using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unnur
{
    class Dungeon : Scene
    {


        public Dungeon(Point dimensions) : base(dimensions)
        {
            Random random = new Random();
            
            Wall floor = new Wall(new Vector2(dimensions.X, 32), new Vector2(0, dimensions.Y - 32));
            Wall wall2 = new Wall(new Vector2(32, 32), new Vector2(random.Next(128, dimensions.X - 32), dimensions.Y - 64));

            AddStaticCollideable(floor);
            AddStaticCollideable(wall2);

        }


        
    }
}
