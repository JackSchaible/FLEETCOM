using FleetCom.Graphics;
using FleetCom.Graphics.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FleetCom
{
    public enum ResearchStates
    {
        NotResearched,
        Researching,
        Researched
    }
    public delegate void ResearchStart(ResearchItem sender);
    public class ResearchItem : Button
    {
        public List<string> Prerequisites { get; set; }
        public bool Researched { get; set; }
        public string Name { get; set; }
        public int Cost { get; set; }
        public int TurnsResearched { get; set; }
        public int TurnsToResearch { get; set; }
        public ResearchStates ResearchState { get; set; }
        public ResearchStart ResearchStarted;

        private Texture2D PopupTexture;

        public ResearchItem(List<string> prereqs, string name, int cost, int turnsToResearch, 
            Texture2D popupTexture, Texture2D normalTexture, Texture2D hoverTexture, Texture2D pressedTexture,
            Vector2 position, ResearchStates researchState)
            : base (normalTexture, hoverTexture, pressedTexture, position)
        {
            Prerequisites = prereqs;
            ResearchState = researchState;
            Name = name;
            Cost = cost;
            TurnsToResearch = turnsToResearch;
            PopupTexture = popupTexture;
            TurnsResearched = 0;
        }

        public void Update(MouseState currentState, Camera camera)
        {
            mouseState = currentState;
            Vector2 mouse = Vector2.Transform(new Vector2(currentState.X, currentState.Y), camera.InverseTransform);

            if (CollisionRect.Contains(new Point((int)mouse.X, (int)mouse.Y)))
            {
                if (mouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                {
                    ButtonState = ButtonStates.Pressed;
                    Texture = DownTexture;

                    if (ResearchState == ResearchStates.NotResearched)
                        ResearchStarted(this);
                }
                else
                {
                    ButtonState = ButtonStates.Hover;
                    Texture = HoverTexture;
                }
            }
            else
            {
                ButtonState = ButtonStates.Normal;

                if (ResearchState == ResearchStates.Researched)
                    Texture = DownTexture;
                else if (ResearchState == ResearchStates.Researching)
                    Texture = HoverTexture;
                else
                    Texture = NormalTexture;
            }
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            if (ResearchState == ResearchStates.Researching)
                    spriteBatch.DrawString(font, (TurnsResearched / TurnsToResearch).ToString("P"), new Vector2(Position.X + 150, Position.Y + 150), new Color(247.0f, 148.0f, 30.0f));

            if (ButtonState == ButtonStates.Hover)
                spriteBatch.Draw(PopupTexture, new Vector2(Position.X + 150, Position.Y + 150), null, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.5f);

            spriteBatch.Draw(Texture, Position, null, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.5f);
        }
    }
}
