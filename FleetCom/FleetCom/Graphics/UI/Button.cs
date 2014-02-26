using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FleetCom.Graphics.UI
{
    public enum ButtonStates
    {
        Normal,
        Hover,
        Pressed
    }
    public delegate void ButtonPress();
    public class Button
    {
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }
        public event ButtonPress ButtonPressed;
        public ButtonStates ButtonState { get; set; }
        public Rectangle CollisionRect { get; set; }
        public Texture2D NormalTexture { get; set; }
        public Texture2D HoverTexture { get; set; }
        public Texture2D DownTexture { get; set; }

        protected MouseState mouseState;
        protected MouseState previousMouseState;

        public Button(Texture2D normalTexture, Texture2D hoverTexture, Texture2D downTexture,
            Vector2 position)
        {
            NormalTexture = normalTexture;
            HoverTexture = hoverTexture;
            DownTexture = downTexture;
            Texture = NormalTexture;
            Position = position;

            mouseState = Mouse.GetState();
            previousMouseState = mouseState;

            CollisionRect = new Rectangle((int)Position.X, (int)Position.Y,
                    Texture.Width,
                    Texture.Height);
        }

        public virtual void Update(MouseState currentState)
        {
            mouseState = currentState;

            if (CollisionRect.Contains(new Point(mouseState.X, mouseState.Y)))
            {
                if (mouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                {
                    ButtonState = ButtonStates.Pressed;
                    ButtonPressed();
                }
                else
                    ButtonState = ButtonStates.Hover;
            }
            else
                ButtonState = ButtonStates.Normal;

            switch (ButtonState)
            {
                case ButtonStates.Normal:
                    Texture = NormalTexture;
                    break;

                case ButtonStates.Hover:
                    Texture = HoverTexture;
                    break;

                case ButtonStates.Pressed:
                    Texture = DownTexture;
                    break;
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, null, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 1.0f);
        }
    }
}
