using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unnur
{
    class Player : Character
    {

        public Player(Vector2 dimensions, Vector2 coordinates) : base(dimensions, coordinates)
        {
            coordinates = new Vector2();

        }
    }
}
