using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unnur
{
    class MoveableEntity : CollideableEntity
    {
        public MoveableEntity(Vector2 dimensions, Vector2 coordinates) : base(dimensions, coordinates)
        {

        }
    }
}
