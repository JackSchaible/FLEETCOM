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
    public class StoreItem : Button
    {
        public delegate void StoreItemSelect(StoreItem item);
        public bool Selected { get; set; }
        public bool Available { get; set; }
        public IShip Ship { get; set; }
        public StoreItemSelect StoreItemSelected;

        Texture2D Popup, UnavailableTexture;
        SpriteFont MH15;
        Game1 game;

        public StoreItem(Texture2D normalTexture, Texture2D hoverTexture, Texture2D downTexture,
            Texture2D unavailableTexture, Texture2D shipTexture, Texture2D popup, Vector2 position,
            Game1 game, SpriteFont mh15)
            : base (normalTexture, hoverTexture, downTexture, position)
        {
            Available = !Ship.Prerequisites.Except(game.ResearchMenu.ResearchTree.Values.Where(x => x.ResearchState == ResearchStates.Researched)).Any();
            Selected = false;
            this.game = game;

            MH15 = mh15;
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
                        StoreItemSelected(this);

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
                int counter = 0;
                foreach(ResearchItem item in Ship.Prerequisites.Except(game.ResearchMenu.ResearchTree.Values.Where(x => x.ResearchState == ResearchStates.Researched)))
                {
                    //Display unresearched prereqs
                    if (counter == 0)
                        spriteBatch.DrawString(MH15, item.Name, new Vector2(Position.X + 240, Position.Y + 50), Color.White, 0.0f, Vector2.Zero,
                            1.0f, SpriteEffects.None, 1.0f);
                    else if (counter == 1)
                        spriteBatch.DrawString(MH15, item.Name, new Vector2(Position.X + 240, Position.Y + 70), Color.White, 0.0f, Vector2.Zero,
                            1.0f, SpriteEffects.None, 1.0f);

                    counter++;
                }

                spriteBatch.Draw(Popup, new Vector2(Position.X + 225, Position.Y), null, Color.White, 0.0f, Vector2.Zero, 1.1f, SpriteEffects.None, 0.9f);
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
                spriteBatch.Draw(Ship.IconTexture, Position, null, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
            }
        }
    }
}
