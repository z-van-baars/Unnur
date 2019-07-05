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
            
            Wall floor = new Wall(new Vector2(dimensions.X, 1), new Vector2(0, dimensions.Y - 1), this);
            Wall wall2 = new Wall(new Vector2(1, 1), new Vector2(random.Next(0, dimensions.X), dimensions.Y - 2), this);

            AddStaticCollideable(floor);
            AddStaticCollideable(wall2);

        }


        
    }
}
