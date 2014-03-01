using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using FleetCom.Graphics;
using FleetCom.Graphics.UI;


namespace FleetCom
{
    public class FleetMenu : Microsoft.Xna.Framework.DrawableGameComponent
    {
        SpriteBatch spriteBatch;

        Sprite Title, Tutorial;
        Button BackButton, ResearchButton, OKButton;
        bool MyFleet;

        //remove
        bool hideTutorial = false;

        public FleetMenu(Game game)
            : base(game)
        {
        }

        public override void Initialize()
        {
            MyFleet = false;

            spriteBatch = new SpriteBatch(((Game1)Game).GraphicsDevice);

            Title = new Sprite(
                ((Game1)Game).Content.Load<Texture2D>("Graphics/FleetMenu/Title"),
                new Vector2(0, 0), 1.0f, 0.0f, 1.0f);
            Tutorial = new Sprite(
                ((Game1)Game).Content.Load<Texture2D>("Graphics/FleetMenu/Tutorial"),
                new Vector2(450, 300), 1.0f, 0.0f, 0.9f);

            BackButton = new Button(
                ((Game1)Game).Content.Load<Texture2D>("Graphics/UI/BackButton"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/UI/BackButton-Hover"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/UI/BackButton-Pressed"),
                new Vector2(850, 30));
            ResearchButton = new Button(
                ((Game1)Game).Content.Load<Texture2D>("Graphics/UI/ResearchButton"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/UI/ResearchButton-Hover"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/UI/ResearchButton-Pressed"),
                new Vector2(1000, 30));
            OKButton = new Button(
                ((Game1)Game).Content.Load<Texture2D>("Graphics/UI/OKButton"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/UI/OKButton-Hover"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/UI/OKButton-Pressed"),
                new Vector2(830, 850));
            BackButton.ButtonPressed += BackButton_ButtonPressed;
            ResearchButton.ButtonPressed += ResearchButton_ButtonPressed;
            OKButton.ButtonPressed += OKButton_ButtonPressed;

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            MouseState state = Mouse.GetState();

            if (false/*!((Game1)Game).User.FleetTutorial*/)
            {
                OKButton.Update(state);
            }
            else
            {
                BackButton.Update(state);
                ResearchButton.Update(state);
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            Title.Draw(spriteBatch);
            BackButton.Draw(spriteBatch);
            ResearchButton.Draw(spriteBatch);

            if (false/*!((Game1)Game).User.FleetTutorial*/)
            {
                Tutorial.Draw(spriteBatch);
                OKButton.Draw(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        void ResearchButton_ButtonPressed()
        {
            ((Game1)Game).GameState = GameStates.Research;
        }

        void BackButton_ButtonPressed()
        {
            ((Game1)Game).GameState = ((Game1)Game).PreviousGameState;
        }

        void OKButton_ButtonPressed()
        {
            //((Game1)Game).User.FleetTutorial = true;
            hideTutorial = true;
        }
    }
}
