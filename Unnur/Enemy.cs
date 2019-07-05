using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unnur
{
    class Enemy : Character
    {
        public Enemy(Vector2 dimensions, Vector2 coordinates, Scene currentScene) : base(dimensions, coordinates, dimensions, currentScene)
        {

        }
    }
}
