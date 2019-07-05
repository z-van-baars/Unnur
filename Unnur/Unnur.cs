using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

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
        private Texture2D collisionHelper;
        private Texture2D wallSprite;
        private Texture2D rockSprite;
        private SpriteFont titleFont;

        private Collision collision;
        public Point MousePos;
        public Point CursorMapPos;
        

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
            gameState.DisplayShift = new Point(200, 10);
            gameState.currentScene = new Dungeon(new Point(60, 30));
            gameState.Player = new Player(new Vector2(32, 64), new Vector2(32, 600), gameState.currentScene);
            gameState.currentScene.AddCharacter(gameState.Player);
            collision = new Collision();


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
            rockSprite = Content.Load<Texture2D>("art/rock");
            collisionHelper = Content.Load<Texture2D>("art/collision_visualizer");

            /// Graphics initialization stuff
            /// This just generates on the fly textures for the wall objects
            foreach (Entity wallObject in gameState.currentScene.GetCollideableEntities())
            {
                if (wallObject.Image == null)
                {
                    RenderTarget2D newWallImage = new RenderTarget2D(GraphicsDevice, (int)wallObject.GetWidth(), (int)wallObject.GetHeight());
                    GraphicsDevice.SetRenderTarget(newWallImage);
                    GraphicsDevice.Clear(Color.Azure);
                    wallObject.Image = newWallImage;
                    GraphicsDevice.SetRenderTarget(null);
                }

            }
            gameState.ResetAABBRenderBoxes(GraphicsDevice);

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
            gameState.LogInputs(Keyboard.GetState(), Mouse.GetState());


            MousePos.X = gameState.GetMouseState().X;
            MousePos.Y = gameState.GetMouseState().Y;


            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
                || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }
            if (gameState.GetKeyState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }
            if (gameState.GetMouseState().LeftButton == ButtonState.Released
                && gameState.GetLastMouseState().LeftButton == ButtonState.Pressed)
            {
                Rock newRock = new Rock(new Vector2(20, 20),
                                        new Vector2(
                                            MousePos.X - gameState.DisplayShift.X,
                                            MousePos.Y - gameState.DisplayShift.Y),
                                        gameState.currentScene);
                newRock.Image = rockSprite;
                gameState.currentScene.AddPhysicalEntity(newRock);
                gameState.ResetAABBRenderBoxes(GraphicsDevice);
            }
            
            gameState.DisplayShift = new Point(
                -(int)gameState.Player.GetLeft() + (int)(DisplayDimensions.X / 2) - 32,
                -(int)gameState.Player.GetTop() + (int)DisplayDimensions.Y / 2 - 64);

            foreach (PhysicalEntity physEntity in gameState.currentScene.GetPhysicalEntities())
            {
                physEntity.Update(gameState.GetKeyState(), gameState.GetMouseState(), collision, gameState.currentScene);
            }
            gameState.ArchiveInputs();
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
                spriteBatch.Draw(renderObject.Image, new Vector2(
                    renderObject.GetLeft() + gameState.DisplayShift.X,
                    renderObject.GetTop() + gameState.DisplayShift.Y), Color.White);
                List<Point> tilesOccupied;
                tilesOccupied = renderObject.GetOccupiedTiles();
                foreach (Point tileOccupied in tilesOccupied)
                {
                    if (renderObject.IsCollideable())
                    {
                        spriteBatch.Draw(collisionHelper, new Vector2(tileOccupied.X * 32 + gameState.DisplayShift.X, tileOccupied.Y * 32 + gameState.DisplayShift.Y), Color.White);

                    }
                }
                spriteBatch.Draw(renderObject.Aabb.RenderBox, new Vector2(
                    renderObject.Aabb.GetPos().X + gameState.DisplayShift.X,
                    renderObject.Aabb.GetPos().Y + gameState.DisplayShift.Y), Color.White);
            }

            spriteBatch.End();
            // TODO: Add your drawing code here



            base.Draw(gameTime);
        }
    }
}
