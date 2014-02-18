using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FleetCom.Graphics
{
    public class AnimatedSprite : Sprite
    {
        public int FrameWidth;
        public int FrameHeight;

        private Rectangle rectangle;
        private int sheetWidth;
        private int sheetHeight;
        private int currentX;
        private int currentY;
        
        public AnimatedSprite(int framewidth, int frameheight, 
            Texture2D texture, Vector2 position, SpriteFont font)
            : base(texture, position, 1.0f, 0.0f, 0.8f)
        {
            FrameWidth = framewidth;
            FrameHeight = frameheight;

            sheetWidth = texture.Width / framewidth;
            sheetHeight = texture.Height / frameheight;
            currentX = currentY = 0;

            rectangle = new Rectangle(0, 0, framewidth, frameheight);
        }

        public override void Update()
        {
            rectangle.X = currentX * FrameWidth;
            rectangle.Y = currentY * FrameHeight;

            if (currentX == sheetWidth - 1)
            {
                if (currentY == sheetHeight - 1)
                {
                    currentX = currentY = 0;
                }
                else
                {
                    currentY++;
                    currentX = 0;
                }
            }
            else
            {
                currentX++;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, rectangle, Color.White,
                Rotation, Vector2.Zero, Scale, SpriteEffects.None, LayerDepth);
        }
    }
}
