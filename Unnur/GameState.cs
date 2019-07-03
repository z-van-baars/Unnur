using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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

        public void ResetAABBRenderBoxes(GraphicsDevice GraphicsDevice)
        {
            SpriteBatch spriteBatch = new SpriteBatch(GraphicsDevice);
            
            RenderTarget2D dot = new RenderTarget2D(GraphicsDevice, 1, 1);
            GraphicsDevice.SetRenderTarget(dot);
            GraphicsDevice.Clear(Color.Red);
            foreach (Entity entityObject in currentScene.GetEntities())
            {
                spriteBatch.Begin();
                RenderTarget2D newAabbRenderBox = new RenderTarget2D(
                    GraphicsDevice,
                    (int)entityObject.Aabb.GetWidth(),
                    (int)entityObject.Aabb.GetHeight());

                GraphicsDevice.SetRenderTarget(newAabbRenderBox);
                GraphicsDevice.Clear(Color.Transparent);
                float aabbBottom = entityObject.Aabb.GetHeight() - 1;
                for (int x = 0; x < (int)entityObject.Aabb.GetWidth(); x++)
                {
                    spriteBatch.Draw(dot, new Vector2(x, 0), Color.White);

                    spriteBatch.Draw(dot, new Vector2(x, (int)aabbBottom), Color.White);
                    /// spriteBatch.Draw(dot, new Vector2(x, entityObject.Aabb.GetHeight() - 1), Color.White);
                }
                for (int y = 0; y < (int)entityObject.Aabb.GetHeight(); y++)
                {
                    spriteBatch.Draw(dot, new Vector2(0, y), Color.White);
                    spriteBatch.Draw(dot, new Vector2(entityObject.Aabb.GetWidth() - 1, y), Color.White);
                }
                Texture2D newAabbRenderBoxTexture = new Texture2D(
                                                            GraphicsDevice,
                                                            (int)entityObject.Aabb.GetWidth(),
                                                            (int)entityObject.Aabb.GetHeight());
                newAabbRenderBoxTexture = newAabbRenderBox;
                entityObject.Aabb.RenderBox = newAabbRenderBoxTexture;
                spriteBatch.End();
                

            }
            GraphicsDevice.SetRenderTarget(null);

        }

    }
}
