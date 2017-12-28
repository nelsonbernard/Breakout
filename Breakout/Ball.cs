using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using FarseerPhysics;

namespace Breakout
{
    class Ball
    {
        private Vector2 velocity;
        private float speed = .1f;
        public Rectangle boundingBox;
        private Texture2D texture;
        public Vector2 position = new Vector2(0, 0);
        public Body ballBody;
        

        private float gravity = 0.1f;

        public void LoadContent(ContentManager contentManager, string assetName, World world)
        {
            texture = contentManager.Load<Texture2D>(assetName);
            position.X = (float)Game1.Instance.GraphicsDevice.Viewport.Width / 2;
            position.Y = (float)(Game1.Instance.GraphicsDevice.Viewport.Height * .33 - 400);
            velocity = new Vector2(1.5f, -1.5f);
            //boundingBox = new Rectangle((int)(this.position.X + 5), (int)(this.position.Y + 5), texture.Width - 5, texture.Height - 5);
            ballBody = BodyFactory.CreateCircle(world, ConvertUnits.ToSimUnits(texture.Width / 2), 1f, null);
            ballBody.BodyType = BodyType.Dynamic;
            ballBody.Position = new Microsoft.Xna.Framework.Vector2(position.X, position.Y);
            //boundingBox = new Rectangle(texture.Bounds.Left, texture.Bounds.Top, texture.Bounds.Right, texture.Bounds.Bottom);

        }

        /*public Rectangle BoundingBox
        {
            get { return this.boundingBox; }
        }*/

        public void Update(GameTime gameTime, List<Brick> bricks, Paddle paddle)
        {
            velocity.Y += gravity;
            position.Y += velocity.Y;
            position.X += velocity.X;

            //boundingBox = new Rectangle((int)this.position.X, (int)(this.position.Y), (texture.Width), (texture.Height));
            boundingBox = new Rectangle((int)(this.position.X + 3), (int)(this.position.Y + 3), (int)(texture.Width - 3), (int)(texture.Height - 3));

            CheckWallCollision();
            CheckPaddleCollision(paddle);
            CheckBrickCollision(bricks);
        }

        public void CheckBrickCollision(List<Brick> bricks)
        {
            //this.position += this.velocity * speed;
            foreach (Brick brick in bricks)
            {
                Rectangle brickRect = brick.boundingBox;
                
                if(this.boundingBox.Intersects(brickRect) && brick.alive)
                {
                    Rectangle intersectionRect = Rectangle.Intersect(this.boundingBox, brickRect);
                   
                    bool flipX, flipY;

                    var brickTop = brickRect.Top;
                    var brickBottom = brickRect.Bottom;
                    var brickLeft = brickRect.Left;
                    var brickRight = brickRect.Right;


                    flipX = (this.boundingBox.Left <= brickRect.Right || this.boundingBox.Right >= brickRect.Left);
                    flipY = (this.boundingBox.Top <= brickRect.Bottom || this.boundingBox.Bottom >= brickRect.Top);

                    if(flipX)
                    {
                        velocity.X = -velocity.X + speed;
                    }

                    if(flipY)
                    {
                        velocity.Y = -velocity.Y + speed;
                    }

                    
                    brick.alive = false;
                }
            }
        }

        public void CheckPaddleCollision(Paddle paddle)
        {
            Rectangle paddleRect = paddle.boundingBox;

            if(boundingBox.Intersects(paddle.boundingBox))
            {
                Rectangle iRect = Rectangle.Intersect(this.boundingBox, paddleRect);
                velocity.Y *= -1;
            }
        }

        public void CheckWallCollision()
        {
            //this.position += this.velocity * speed;

            if (position.X <= 0)
            {
                position.X = 0;
                velocity.X = -velocity.X;
            }

            if (position.X + texture.Width >= Game1.Instance.GraphicsDevice.Viewport.Width)
            {
                position.X = Game1.Instance.GraphicsDevice.Viewport.Width - this.texture.Width;
                velocity.X = -velocity.X;
            }

            if (position.Y <= 0)
            {
                position.Y = 0;
                velocity.Y = -velocity.Y;
            }

            if (this.position.Y + this.texture.Height >= Game1.Instance.GraphicsDevice.Viewport.Height)
            {
                this.position.Y = Game1.Instance.GraphicsDevice.Viewport.Height - this.texture.Height;
                this.velocity.Y = -velocity.Y;
            }
        }

        /*public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }*/

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 1);
        }
    }
}
