using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unnur
{
    class Wall : CollideableEntity
    {


        public Wall(Vector2 dimensions, Vector2 coordinates, Scene currentScene) : base(
            new Vector2(dimensions.X * 32, dimensions.Y * 32),
            new Vector2(coordinates.X * 32, coordinates.Y * 32),
            new Vector2(dimensions.X * 32, dimensions.Y * 32),
            currentScene)
        {
        }



    }
}
