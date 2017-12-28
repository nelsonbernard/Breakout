using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using FarseerPhysics.Collision;

namespace Breakout
{
    class Paddle
    {
        public Vector2 position = new Vector2(0f, 0f);
        private Texture2D texture;
        public Rectangle boundingBox;
        Body bodyPaddle;

        public void LoadContent(ContentManager contentManager, string assetName, World world)
        {
            texture = contentManager.Load<Texture2D>(assetName);
            float positionY = Game1.Instance.GraphicsDevice.Viewport.Height - texture.Height - 15;
            float positionX = Game1.Instance.GraphicsDevice.Viewport.Width / 2 - (texture.Width / 2);
            position = new Vector2(positionX, positionY);
            boundingBox = new Rectangle((int)(this.position.X), (int)(this.position.Y), texture.Width, texture.Height);
            bodyPaddle = BodyFactory.CreateRectangle(world, ConvertUnits.ToSimUnits(texture.Width / 2), ConvertUnits.ToSimUnits(texture.Height / 2), 1, this);
            bodyPaddle.BodyType = BodyType.Dynamic;
            bodyPaddle.Position = position;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
            //spriteBatch.Draw(texture, position, null, Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 1);
        }

        public void Update(GameTime gameTime)
        {

            KeyboardState currentKeyboardState = Keyboard.GetState();
            UpdateMovement(currentKeyboardState);
            boundingBox = new Rectangle((int)(this.position.X), (int)(this.position.Y), texture.Width, texture.Height);
        }

        private void UpdateMovement(KeyboardState keyboardState)
        {
            if (keyboardState.IsKeyDown(Keys.Left) == true)
            {
                position.X = position.X - 5;
                if(position.X < Game1.Instance.GraphicsDevice.Viewport.Bounds.Left)
                    position.X = Game1.Instance.GraphicsDevice.Viewport.Bounds.Left;
                
            }
            if (keyboardState.IsKeyDown(Keys.Right) == true)
            {
                position.X = position.X + 5;
                if (position.X > Game1.Instance.GraphicsDevice.Viewport.Bounds.Right - texture.Width)
                    position.X = Game1.Instance.GraphicsDevice.Viewport.Bounds.Right - texture.Width;
            }

            boundingBox = new Rectangle((int)(this.position.X), (int)(this.position.Y), texture.Width, texture.Height);
        }
    }
}
