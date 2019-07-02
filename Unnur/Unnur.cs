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



            gameState.currentScene = new Dungeon(new Point(1900, 900));
            gameState.Player = new Player(new Vector2(32, 64), new Vector2(32, 0));
            gameState.currentScene.AddCharacter(gameState.Player);



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
            gameState.Player.Image = Content.Load<Texture2D>("art/player_1");
            enemySprite = Content.Load<Texture2D>("art/enemy_1");
            wallSprite = Content.Load<Texture2D>("art/enemy_1");

            /// Graphics initialization stuff
            foreach (Entity wallObject in gameState.currentScene.GetCollideableEntities())
            {
                if (wallObject.Image == null)
                {
                    RenderTarget2D newWallImage = new RenderTarget2D(GraphicsDevice, (int)wallObject.GetWidth(), (int)wallObject.GetHeight());
                    GraphicsDevice.SetRenderTarget(newWallImage);
                    GraphicsDevice.Clear(Color.Azure);
                    wallObject.Image = newWallImage;
                }

            }
            GraphicsDevice.SetRenderTarget(null);

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
                gameState.Player.SetVelocity(5, 0);
            }
            if (KeyboardState.IsKeyDown(Keys.A))
            {
                DisplayShift.X += 5;
                gameState.Player.SetVelocity(-5, 0);
            }
            if (KeyboardState.IsKeyDown(Keys.W))
            {
                DisplayShift.Y += 5;
                gameState.Player.SetVelocity(0, -5);
            }
            if (KeyboardState.IsKeyDown(Keys.S))
            {
                DisplayShift.Y -= 5;
                gameState.Player.SetVelocity(0, 5);
            }
            if (KeyboardState.IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            // TODO: Add your update logic here
            foreach (MoveableEntity movingEntity in gameState.currentScene.GetMovingEntities())
            {
                movingEntity.ApplyGravity();
                movingEntity.Move();
                
            }
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
            foreach (Entity renderObject in gameState.currentScene.GetEntities())
            {
                spriteBatch.Draw(renderObject.Image, new Vector2(renderObject.GetLeft() + DisplayShift.X, renderObject.GetTop() + DisplayShift.Y), Color.White);
            }

            spriteBatch.End();
            // TODO: Add your drawing code here



            base.Draw(gameTime);
        }
    }
}
