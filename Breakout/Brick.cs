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
    class Brick
    {
        private Color Color;
        public Rectangle boundingBox;
        private Texture2D texture;
        public Vector2 position = new Vector2(0f, 0f);
        public bool alive;
        public Body brickBody;
        private float density = 0f;

        public void LoadContent(ContentManager contentManager, string assetName, World world)
        {
            texture = contentManager.Load<Texture2D>(assetName);
            this.position.X = this.position.X * texture.Width;
            this.position.Y = this.position.Y * texture.Height;
            brickBody = BodyFactory.CreateRectangle(world, texture.Width, texture.Height, density, this);
            brickBody.BodyType = BodyType.Static;
            brickBody.Position = position;

            //boundingBox = new Rectangle((int)(this.position.X), (int)(this.position.Y), texture.Width, texture.Height);
            //boundingBox = new Rectangle(texture.Bounds.Left, texture.Bounds.Top, texture.Bounds.Right, texture.Bounds.Bottom);
            alive = true;
        }

        /*public Rectangle BoundingBox
        {
            get { return boundingBox; }
            set { boundingBox = value; }
        }*/


        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 origin = new Vector2(texture.Width / 2, texture.Height / 2);
            //spriteBatch.Draw(texture, position, Color.White);
            //spriteBatch.Draw(texture, ConvertUnits.ToDisplayUnits(brickBody.Position), null, Color.White, 0, new Vector2(texture.Width / 2, texture.Height / 2), 1f, SpriteEffects.None, 1f);
            //spriteBatch.Draw(texture, brickBody.Position, null, Color.White, brickBody.Rotation, origin, SpriteEffects.None, 0f);
            spriteBatch.Draw(texture, brickBody.Position, Color.White);
        }
    }
}
