using FleetCom.Classes.Ships;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FleetCom.Graphics.UI
{
    public class ShipSlot : Button
    {
        public delegate void ShipSlotSelect(ShipSlot slot);
        public string RankKey;
        public int XPRequired;
        public bool Selected { get; set; }
        public bool Available { get; set; }
        public IShip Ship { get; set; }
        public ShipSlotSelect ShipSlotSelected;

        Texture2D Popup, Insignia, UnavailableTexture;
        SpriteFont MH15;
        Game1 game;

        public ShipSlot(Texture2D normalTexture, Texture2D hoverTexture, Texture2D downTexture,
            Texture2D unavailableTexture, Texture2D insignia, Texture2D popup, Vector2 position, 
            string rankKey, int xPRequired, Game1 game, bool available, SpriteFont mh15)
            :base (normalTexture, hoverTexture, downTexture, position)
        {
            RankKey = rankKey;
            XPRequired = xPRequired;
            Selected = false;
            Available = available;
            this.game = game;

            MH15 = mh15;
            Insignia = insignia;
            Popup = popup;
            UnavailableTexture = unavailableTexture;
        }

        public override void Update(MouseState currentState)
        {
            mouseState = currentState;

            if (CollisionRect.Contains(new Point(mouseState.X, mouseState.Y)))
            {
                if (mouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                {
                    ButtonState = ButtonStates.Pressed;

                    if (Available)
                        ShipSlotSelected(this);

                    Selected = true;
                }
                else
                    ButtonState = ButtonStates.Hover;
            }
            else
                ButtonState = ButtonStates.Normal;

            if (Selected)
                ButtonState = ButtonStates.Pressed;

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

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (ButtonState == ButtonStates.Hover)
            {
                spriteBatch.Draw(Popup, new Vector2(Position.X + 210, Position.Y - 10), null, Color.White, 0.0f, Vector2.Zero, 1.1f, SpriteEffects.None, 0.9f);
                spriteBatch.DrawString(MH15, Player.Ranks[RankKey], new Vector2(Position.X + 225, Position.Y + 10), Color.White, 0.0f, Vector2.Zero, 
                    1.0f, SpriteEffects.None, 1.0f);
                spriteBatch.DrawString(MH15, String.Format("XP: {0}/{1}", game.User.XP, XPRequired), new Vector2(Position.X + 225, Position.Y + 25),
                    Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 1.0f);
            }

            if (Available)
            {
                spriteBatch.Draw(Texture, Position, null, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.1f);

                if (Selected)
                    spriteBatch.Draw(DownTexture, Position, null, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.1f);

                if (Ship != null)
                    spriteBatch.Draw(Ship.IconTexture, Position, null, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.8f);
            }
            else
            {
                spriteBatch.Draw(UnavailableTexture, Position, null, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.1f);
                spriteBatch.Draw(Insignia, Position, null, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
            }
        }
    }
}
