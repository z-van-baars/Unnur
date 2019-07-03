using Microsoft.Xna.Framework;
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
        private MouseState mouseState;
        private MouseState lastMouseState;
        private KeyboardState keyboardState;
        private KeyboardState lastKeyboardState;
        public Point DisplayShift;
        public Dungeon currentScene;
        public Player Player;

        public GameState()
        {
        }
        public void LogInputs(KeyboardState newKeyboardState, MouseState newMouseState)
        {
            LogKeyState(newKeyboardState);
            LogMouseState(newMouseState);
        }
        public void LogMouseState(MouseState newMouseState)
        {
            mouseState = newMouseState;
        }
        public MouseState GetMouseState()
        {
            return mouseState;
        }
        public MouseState GetLastMouseState()
        {
            return lastMouseState;
        }
        public void LogKeyState(KeyboardState newKeyboardState)
        {
            keyboardState = newKeyboardState;
        }
        public KeyboardState GetKeyState()
        {
            return keyboardState;
        }
        public KeyboardState GetLastKeyState()
        {
            return lastKeyboardState;
        }
        public void ArchiveInputs()
        {
            lastKeyboardState = keyboardState;
            lastMouseState = mouseState;
        }


    }
}
