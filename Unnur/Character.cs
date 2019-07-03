using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unnur
{
    class Character : PhysicalEntity
    {
        public Character(Vector2 dimensions, Vector2 coordinates, Vector2 aabbDimensions) : base(dimensions, coordinates, aabbDimensions)
        {

        }
    }
}
