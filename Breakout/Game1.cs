using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.IO;

using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using FarseerPhysics.Collision;

namespace Breakout
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Paddle paddle;
        Ball ball;
        List<Brick> bricks = new List<Brick>();
        int currentLevel = 1;

        World world;

        public static Game1 Instance;

        public Game1()
        {
            Instance = this;
            graphics = new GraphicsDeviceManager(this);
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
            // TODO: Add your initialization logic here
            paddle = new Paddle();
            ball = new Ball();
            LoadLevel(currentLevel);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            world = new World(new Vector2(0f, 9.8f));
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            ball.LoadContent(this.Content, "Ball", world);
            paddle.LoadContent(this.Content, "paddle", world);

            foreach(Brick brick in bricks)
            {
                brick.LoadContent(this.Content, "brick", world);
            }
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            world.Step((float)gameTime.ElapsedGameTime.TotalSeconds);
            //world.Step(Math.Min((float)gameTime.ElapsedGameTime.TotalSeconds, (1f / 30f)));
            // TODO: Add your update logic here
            paddle.Update(gameTime);
            ball.Update(gameTime, bricks, paddle);

            foreach (Brick brick in bricks)
                brick.Update(gameTime);
            

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            ball.Draw(this.spriteBatch);
            paddle.Draw(this.spriteBatch);

            foreach(Brick brick in bricks)
            {
                if (brick.alive)
                    brick.Draw(this.spriteBatch);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void LoadLevel(int level)
        {
            //Stream fileStream = File.Open("Content\\lvl" + level + ".txt", 0);
            //StreamReader sr = new StreamReader(fileStream);
            var levelData = File.ReadAllLines(@"Levels\lvl" + level + ".txt");

            int rowIndex = 0;
            int colIndex = 0;

            foreach (var line in levelData)
            {
                rowIndex = rowIndex + 1;
                colIndex = 0;
                string[] levelArray = line.Split(',');

                foreach (var e in levelArray)
                {
                    colIndex = colIndex + 1;
                    //bricks = int.Parse(e);
                    if(int.Parse(e) == 1)
                    {
                        Brick newBrick = new Brick();
                        newBrick.position = new Vector2(colIndex, rowIndex);
                        bricks.Add(newBrick);
                    }
                }
            }
        }
    }
}
