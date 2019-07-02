using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unnur
{
    class Player
    {
        private Vector2 coordinates;
        public Texture2D Image;

        public Player()
        {
            coordinates = new Vector2(32, 900  - 96);

        }

        public Vector2 GetPos()
        {
            return coordinates;
        }

        public void Move(double xTranlsation, double yTranslation)
        {
            Vector2 newCoordinates = new Vector2((int)(coordinates.X + xTranlsation), (int)(coordinates.Y + yTranslation));
            coordinates = newCoordinates;
        }
    }
}
