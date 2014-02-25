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
        public Texture2D Texture
        {
            get
            {
                Texture2D result = null;

                switch (ButtonState)
                {
                    case ButtonStates.Normal:
                        result = NormalTexture;
                        break;

                    case ButtonStates.Hover:
                        result = HoverTexture;
                        break;

                    case ButtonStates.Pressed:
                        result = DownTexture;
                        break;
                }

                return result;
            }
        }
        public Vector2 Position { get; set; }
        public event ButtonPress ButtonPressed;
        public ButtonStates ButtonState { get; set; }
        public Rectangle CollisionRect { get; set; }

        protected Texture2D NormalTexture;
        protected Texture2D HoverTexture;
        protected Texture2D DownTexture;
        private MouseState mouseState;
        private MouseState previousMouseState;

        public Button(Texture2D normalTexture, Texture2D hoverTexture, Texture2D downTexture,
            Vector2 position)
        {
            NormalTexture = normalTexture;
            HoverTexture = hoverTexture;
            DownTexture = downTexture;
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
        }

        public virtual void Update(MouseState currentState, Camera camera)
        {
            mouseState = currentState;
            Vector2 mouse = Vector2.Transform(new Vector2(currentState.X, currentState.Y), camera.InverseTransform);
            
            if (CollisionRect.Contains(new Point((int)mouse.X, (int)mouse.Y)))
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
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, null, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 1.0f);
        }
    }
}
