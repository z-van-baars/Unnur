using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unnur
{
    class GameState
    {
        private MouseState MouseState;
        private KeyboardState KeyboardState;
        public Dungeon currentDungeon;
        public Player Player;

        public GameState()
        {
            currentDungeon = new Dungeon();
            Player = new Player();
        }


    }
}
