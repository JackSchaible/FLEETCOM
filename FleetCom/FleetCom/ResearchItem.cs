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
    public class ResearchItem : Button
    {
        public List<string> Prerequisites { get; set; }
        public bool Researched { get; set; }
        public string Name { get; set; }
        public int Cost { get; set; }
        public int TurnsResearched { get; set; }
        public int TurnsToResearch { get; set; }
        public ResearchStates ResearchState { get; set; }

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

        public override void Update(MouseState currentState, Camera camera)
        {
            base.Update(currentState, camera);
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            switch (ResearchState)
            {
                case ResearchStates.Researching:
                    spriteBatch.DrawString(font, (TurnsResearched / TurnsToResearch).ToString("P"), new Vector2(Position.X, Position.Y + 150), Color.White);
                    break;

                case ResearchStates.Researched:
                    spriteBatch.Draw(DownTexture, Position, null, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 1.0f);
                    break;
            }

            if (ButtonState == ButtonStates.Hover)
                spriteBatch.Draw(PopupTexture, new Vector2(Position.X + 150, Position.Y + 150), null, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 1.0f);

            base.Draw(spriteBatch);
        }
    }
}
