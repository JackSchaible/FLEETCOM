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
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class GalaxyMap : Microsoft.Xna.Framework.DrawableGameComponent
    {
        SpriteBatch spriteBatch;

        Sprite background;
        Sprite Tutorial1, Tutorial2, Tutorial3, Tutorial4, Title;
        SpriteFont MH30;

        Button OKButton, ResearchButton, FleetButton;

        int wait;
        bool firstUpdate;

        public GalaxyMap(Game game)
            : base(game)
        {
        }

        public override void Initialize()
        {
            wait = 10;
            firstUpdate = true;
            spriteBatch = new SpriteBatch(((Game1)Game).GraphicsDevice);

            background = new Sprite(
                ((Game1)Game).Content.Load<Texture2D>(@"Graphics/Environments/starfield"),
                Vector2.Zero, 1.0f, 0.0f, 0.0f);
            Tutorial1 = new Sprite(
                ((Game1)Game).Content.Load<Texture2D>("Graphics/IncursionMap/Tutorial1"),
                new Vector2(450, 325), 1.0f, 0.0f, 0.9f);
            Tutorial2 = new Sprite(
                ((Game1)Game).Content.Load<Texture2D>("Graphics/IncursionMap/Tutorial2"),
                new Vector2(450, 290), 1.0f, 0.0f, 0.9f);
            Tutorial3 = new Sprite(
                ((Game1)Game).Content.Load<Texture2D>("Graphics/IncursionMap/Tutorial3"),
                new Vector2(500, 300), 1.0f, 0.0f, 0.9f);
            Tutorial4 = new Sprite(
                ((Game1)Game).Content.Load<Texture2D>("Graphics/IncursionMap/Tutorial4"),
                new Vector2(500, 300), 1.0f, 0.0f, 0.9f);
            Title = new Sprite(
                ((Game1)Game).Content.Load<Texture2D>("Graphics/IncursionMap/Title"),
                new Vector2(25, 30), 1.0f, 0.0f, 1.0f);

            OKButton = new Button(
                ((Game1)Game).Content.Load<Texture2D>("Graphics/UI/OKButton"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/UI/OKButton-Hover"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/UI/OKButton-Pressed"),
                new Vector2(830, 830));
            ResearchButton = new Button(
                ((Game1)Game).Content.Load<Texture2D>("Graphics/UI/ResearchButton"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/UI/ResearchButton-Hover"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/UI/ResearchButton-Pressed"),
                new Vector2(1800,10));
            FleetButton = new Button(
                ((Game1)Game).Content.Load<Texture2D>("Graphics/UI/FleetIcon"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/UI/FleetIcon-Hover"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/UI/FleetIcon-Pressed"),
                new Vector2(1650, 10));

            OKButton.ButtonPressed += OKButton_ButtonPressed;
            ResearchButton.ButtonPressed += ResearchButton_ButtonPressed;
            FleetButton.ButtonPressed += FleetButton_ButtonPressed;

            MH30 = ((Game1)Game).Content.Load<SpriteFont>("Graphics/Fonts/MyriadHebrew-30");

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (firstUpdate)
            {
                foreach (StarCluster item in ((Game1)Game).User.Map.StarClusters)
                    item.ClusterSelected += new OnSelected(ClusterSelected);

                firstUpdate = false;
            }

            MouseState state = Mouse.GetState();

            switch (((Game1)Game).User.TutorialStep)
            {
                case TutortialSteps.GalaxyMap1:
                    if (wait == 0)
                        OKButton.Update(state);
                    break;

                case TutortialSteps.GalaxyMap2:
                    if (wait == 0)
                        OKButton.Update(state);
                    break;

                case TutortialSteps.GalaxyMap3:
                    if (wait == 0)
                        OKButton.Update(state);
                    break;

                case TutortialSteps.GalaxyMap4:
                    if (wait == 0)
                        OKButton.Update(state);
                    break;

                default:
                    foreach (StarCluster item in ((Game1)Game).User.Map.StarClusters)
                        item.Update(state);

                    ResearchButton.Update(state);
                    FleetButton.Update(state);
                    break;
            }

            if (wait != 0)
                wait--;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);

            background.Draw(spriteBatch);
            Title.Draw(spriteBatch);
            ResearchButton.Draw(spriteBatch);
            FleetButton.Draw(spriteBatch);

            foreach (StarCluster item in ((Game1)Game).User.Map.StarClusters)
                item.Draw(spriteBatch);

            string to = String.Format("<<To: {0} {1}>>", Player.Ranks[((Game1)Game).User.Rank], ((Game1)Game).User.Character);

            switch (((Game1)Game).User.TutorialStep)
            {
                case TutortialSteps.GalaxyMap1:
                    Tutorial1.Draw(spriteBatch);
                    spriteBatch.DrawString(MH30, to, new Vector2(480, 504), Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 1.0f);
                    OKButton.Draw(spriteBatch);
                    break;

                case TutortialSteps.GalaxyMap2:
                    Tutorial2.Draw(spriteBatch);
                    spriteBatch.DrawString(MH30, to, new Vector2(490, 454), Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 1.0f);
                    OKButton.Draw(spriteBatch);
                    break;

                case TutortialSteps.GalaxyMap3:
                    Tutorial3.Draw(spriteBatch);
                    spriteBatch.DrawString(MH30, to, new Vector2(530,475), Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 1.0f);
                    OKButton.Draw(spriteBatch);
                    break;

                case TutortialSteps.GalaxyMap4:
                    Tutorial4.Draw(spriteBatch);
                    spriteBatch.DrawString(MH30, to, new Vector2(530,475), Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 1.0f);
                    OKButton.Draw(spriteBatch);
                    break;
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        void OKButton_ButtonPressed()
        {
            wait = 10;
            if (((Game1)Game).User.TutorialStep == TutortialSteps.GalaxyMap1)
                OKButton.Position = new Vector2(815, 855);
            if (((Game1)Game).User.TutorialStep == TutortialSteps.GalaxyMap2)
                OKButton.Position = new Vector2(880, 805);

            ((Game1)Game).User.TutorialStep++;
        }

        void ClusterSelected(StarCluster item)
        {
            ((Game1)Game).selectedCluster = item;
        }

        void ResearchButton_ButtonPressed()
        {
            ((Game1)Game).PreviousGameState = GameStates.GalaxyMap;
            ((Game1)Game).GameState = GameStates.Research;
        }

        void FleetButton_ButtonPressed()
        {
            ((Game1)Game).FleetMenu.Refresh();
            ((Game1)Game).GameState = GameStates.Fleet;
        }
    }
}
