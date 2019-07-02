using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Unnur
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Unnur : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GameState gameState;

        Point DisplayDimensions;

        private SpriteFont headerFont;
        private Texture2D enemySprite;
        private Texture2D wallSprite;
        private SpriteFont titleFont;

        public Point MousePos;
        public MouseState MouseState;
        public KeyboardState KeyboardState;
        public Point CursorMapPos;
        public Point DisplayShift;

        public Unnur()
        {
            graphics = new GraphicsDeviceManager(this);
            DisplayDimensions = new Point(1600, 900);
            graphics.PreferredBackBufferWidth = (int)DisplayDimensions.X;
            graphics.PreferredBackBufferHeight = (int)DisplayDimensions.Y;
            graphics.IsFullScreen = false;
            TargetElapsedTime = new TimeSpan(0, 0, 0, 0, 17);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            gameState = new GameState();
            IsMouseVisible = true;

            gameState.Player.Image = Content.Load<Texture2D>("art/player_1");
            enemySprite = Content.Load<Texture2D>("art/enemy_1");
            wallSprite = Content.Load<Texture2D>("art/enemy_1");

            foreach (Wall wallObject in gameState.currentDungeon.GetWalls())
            {
                RenderTarget2D newWallImage = new RenderTarget2D(GraphicsDevice, (int)wallObject.GetWidth(), (int)wallObject.GetHeight());
                GraphicsDevice.SetRenderTarget(newWallImage);
                GraphicsDevice.Clear(Color.Azure);

                wallObject.Image = newWallImage;
            }
            GraphicsDevice.SetRenderTarget(null);


            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            var random = new Random();
            var lastKeyboardState = KeyboardState;
            KeyboardState = Keyboard.GetState();
            var lastMouseState = MouseState;
            MouseState = Mouse.GetState();
            MousePos.X = MouseState.X;
            MousePos.Y = MouseState.Y;


            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (KeyboardState.IsKeyDown(Keys.D))
            {
                DisplayShift.X -= 5;
                gameState.Player.Move(5, 0);
            }
            if (KeyboardState.IsKeyDown(Keys.A))
            {
                DisplayShift.X += 5;
                gameState.Player.Move(-5, 0);
            }
            if (KeyboardState.IsKeyDown(Keys.W))
            {
                DisplayShift.Y += 5;
                gameState.Player.Move(0, -5);
            }
            if (KeyboardState.IsKeyDown(Keys.S))
            {
                DisplayShift.Y -= 5;
                gameState.Player.Move(0, 5);
            }
            if (KeyboardState.IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkSlateGray);
            spriteBatch.Begin();
            spriteBatch.Draw(gameState.Player.Image, new Vector2(gameState.Player.GetPos().X + DisplayShift.X, gameState.Player.GetPos().Y + DisplayShift.Y), Color.White);
            spriteBatch.Draw(enemySprite, new Vector2(256 + DisplayShift.X, 900 - 96 + DisplayShift.Y), Color.White);
            foreach (Wall wallObject in gameState.currentDungeon.GetWalls())
            {
                spriteBatch.Draw(wallObject.Image, new Vector2(wallObject.GetLeft() + DisplayShift.X, wallObject.GetTop() + DisplayShift.Y), Color.White);
            }

            spriteBatch.End();
            // TODO: Add your drawing code here



            base.Draw(gameTime);
        }
    }
}
