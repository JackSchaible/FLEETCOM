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
using System.IO;
using System.Xml.Linq;


namespace FleetCom
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class CharacterSelect : Microsoft.Xna.Framework.DrawableGameComponent
    {
        Sprite Title, Char1Desc, Char2Desc, Char3Desc, Char4Desc, OverwritePopup;
        Button Char1Button, Char2Button, Char3Button, Char4Button, NextButton, OKButton, CancelButton;
        SpriteBatch spriteBatch;
        int selectedCharacter;
        bool isOverwriteWindowUp;
        List<string> systemNames;
        Texture2D starClusterNormalTexture, starClusterUnderAttackTexture, starClusterUnownedTexture, clusterStatusTexture;
        SpriteFont MH45, MH75;

        public CharacterSelect(Game game)
            : base(game)
        {

        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            spriteBatch = new SpriteBatch(((Game1)Game).GraphicsDevice);
            selectedCharacter = 1;
            isOverwriteWindowUp = false;

            systemNames = ((Game1)Game).Content.Load<string[]>("Data/StarClusterNames").ToList<string>();
            Title = new Sprite(((Game1)Game).Content.Load<Texture2D>("Graphics/CharacterSelect/Title"),
                new Vector2(60, 50), 1.0f, 0.0f, 1.0f);
            Char1Desc = new Sprite(((Game1)Game).Content.Load<Texture2D>("Graphics/CharacterSelect/Character1Desc"),
                new Vector2(90, 280), 1.0f, 0.0f, 1.0f);
            Char2Desc = new Sprite(((Game1)Game).Content.Load<Texture2D>("Graphics/CharacterSelect/Character2Desc"),
                new Vector2(90, 700), 1.0f, 0.0f, 1.0f);
            Char3Desc = new Sprite(((Game1)Game).Content.Load<Texture2D>("Graphics/CharacterSelect/Character3Desc"),
                new Vector2(1080, 280), 1.0f, 0.0f, 1.0f);
            Char4Desc = new Sprite(((Game1)Game).Content.Load<Texture2D>("Graphics/CharacterSelect/Character4Desc"),
                new Vector2(1080, 700), 1.0f, 0.0f, 1.0f);
            OverwritePopup = new Sprite(((Game1)Game).Content.Load<Texture2D>("Graphics/CharacterSelect/CharacterExistsPopup"),
                new Vector2(465, 275), 1.0f, 0.0f, 1.0f);

            starClusterUnderAttackTexture = ((Game1)Game).Content.Load<Texture2D>("Graphics/IncursionMap/UnderAttackSprite");
            starClusterNormalTexture = ((Game1)Game).Content.Load<Texture2D>("Graphics/IncursionMap/OwnedTexture");
            starClusterUnownedTexture = ((Game1)Game).Content.Load<Texture2D>("Graphics/IncursionMap/NormalTexture");
            clusterStatusTexture = ((Game1)Game).Content.Load<Texture2D>("Graphics/IncursionMap/SystemStatus");
            MH45 = ((Game1)Game).Content.Load<SpriteFont>("Graphics/Fonts/MyriadHebrew-45");
            MH75 = ((Game1)Game).Content.Load<SpriteFont>("Graphics/Fonts/MyriadHebrew-75");

            Char1Button = new Button(((Game1)Game).Content.Load<Texture2D>("Graphics/CharacterSelect/Character1Button"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/CharacterSelect/Character1Button-Hover"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/CharacterSelect/Character1Button-Selected"),
                new Vector2(90, 300)
                );
            Char2Button = new Button(((Game1)Game).Content.Load<Texture2D>("Graphics/CharacterSelect/Character1Button"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/CharacterSelect/Character1Button-Hover"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/CharacterSelect/Character1Button-Selected"),
                new Vector2(90, 720)
                );
            Char3Button = new Button(((Game1)Game).Content.Load<Texture2D>("Graphics/CharacterSelect/Character1Button"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/CharacterSelect/Character1Button-Hover"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/CharacterSelect/Character1Button-Selected"),
                new Vector2(1080, 300)
                );
            Char4Button = new Button(((Game1)Game).Content.Load<Texture2D>("Graphics/CharacterSelect/Character1Button"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/CharacterSelect/Character1Button-Hover"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/CharacterSelect/Character1Button-Selected"),
                new Vector2(1080, 720)
                );
            NextButton = new Button(((Game1)Game).Content.Load<Texture2D>("Graphics/CharacterSelect/NextButton"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/CharacterSelect/NextButton-Hover"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/CharacterSelect/NextButton-Pressed"),
                new Vector2(1620, 50)
                );
            OKButton = new Button(((Game1)Game).Content.Load<Texture2D>("Graphics/CharacterSelect/OKButton"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/CharacterSelect/OKButton-Hover"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/CharacterSelect/OKButton-Pressed"),
                new Vector2(1020, 660)
                );
            CancelButton = new Button(((Game1)Game).Content.Load<Texture2D>("Graphics/CharacterSelect/CancelButton"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/CharacterSelect/CancelButton-Hover"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/CharacterSelect/CancelButton-Pressed"),
                new Vector2(600, 660)
                );

            Char1Button.ButtonPressed += Character1Selected;
            Char2Button.ButtonPressed += Character2Selected;
            Char3Button.ButtonPressed += Character3Selected;
            Char4Button.ButtonPressed += Character4Selected;
            NextButton.ButtonPressed += NextButtonPressed;
            CancelButton.ButtonPressed += CancelOverwrite;
            OKButton.ButtonPressed += Overwrite;

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            MouseState state = Mouse.GetState();

            if (isOverwriteWindowUp)
            {
                CancelButton.Update(state);
                OKButton.Update(state);
            }
            else
            {
                Char1Button.Update(state);
                Char2Button.Update(state);
                Char3Button.Update(state);
                Char4Button.Update(state);
                NextButton.Update(state);
            }

            switch (selectedCharacter)
            {
                case 1:
                    Char1Button.ButtonState = ButtonStates.Pressed;
                    break;

                case 2:
                    Char2Button.ButtonState = ButtonStates.Pressed;
                    break;

                case 3:
                    Char3Button.ButtonState = ButtonStates.Pressed;
                    break;

                case 4:
                    Char4Button.ButtonState = ButtonStates.Pressed;
                    break;
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            Title.Draw(spriteBatch);
            Char1Desc.Draw(spriteBatch);
            Char2Desc.Draw(spriteBatch);
            Char3Desc.Draw(spriteBatch);
            Char4Desc.Draw(spriteBatch);
            Char1Button.Draw(spriteBatch);
            Char2Button.Draw(spriteBatch);
            Char3Button.Draw(spriteBatch);
            Char4Button.Draw(spriteBatch);
            NextButton.Draw(spriteBatch);

            if (isOverwriteWindowUp)
            {
                OverwritePopup.Draw(spriteBatch);
                OKButton.Draw(spriteBatch);
                CancelButton.Draw(spriteBatch);
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }

        void Character1Selected()
        {
            selectedCharacter = 1;
        }

        void Character2Selected()
        {
            selectedCharacter = 2;
        }

        void Character3Selected()
        {
            selectedCharacter = 3;
        }

        void Character4Selected()
        {
            selectedCharacter = 4;
        }

        void NextButtonPressed()
        {
            Characters characterType = new Characters();

            switch (selectedCharacter)
            {
                case 1:
                    characterType = Characters.Aggressive;
                    break;

                case 2:
                    characterType = Characters.Defensive;
                    break;

                case 3:
                    characterType = Characters.Fast;
                    break;

                case 4:
                    characterType = Characters.Tech;
                    break;
            }

            string filename = "Players/" + characterType.ToString() + ".bin";

            if (File.Exists(filename))
            {
                isOverwriteWindowUp = true;
            }
            else
            {
                ((Game1)Game).User = new Player(characterType, true, systemNames,
                    starClusterNormalTexture, starClusterUnderAttackTexture, starClusterUnownedTexture,
                    clusterStatusTexture, MH45, MH75);
                ((Game1)Game).GameState = GameStates.GalaxyMap;
            }
        }

        void CancelOverwrite()
        {
            isOverwriteWindowUp = false;
        }

        void Overwrite()
        {
            Characters characterType = new Characters();

            switch (selectedCharacter)
            {
                case 1:
                    characterType = Characters.Aggressive;
                    break;

                case 2:
                    characterType = Characters.Defensive;
                    break;

                case 3:
                    characterType = Characters.Fast;
                    break;

                case 4:
                    characterType = Characters.Tech;
                    break;
            }

            string filename = "Players/" + characterType.ToString() + ".bin";
            ((Game1)Game).User = new Player(characterType, true, systemNames,
                    starClusterNormalTexture, starClusterUnderAttackTexture, 
                    starClusterUnownedTexture, clusterStatusTexture, MH45, MH75);
            ((Game1)Game).GameState = GameStates.GalaxyMap;
        }
    }
}
