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
    public class MainMenu : Microsoft.Xna.Framework.DrawableGameComponent
    {
        Game baseGame;
        List<ISprite> Sprites;
        List<Button> Buttons;
        SpriteBatch spriteBatch;

        public MainMenu(Game game)
            : base(game)
        {
            baseGame = game;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            Sprites = new List<ISprite>();
            Buttons = new List<Button>();
            spriteBatch = new SpriteBatch(((Game1)Game).GraphicsDevice);
            
            Sprites.Add(new Sprite(((Game1)baseGame).Content.Load<Texture2D>(@"Graphics/MainMenu/Title"),
                new Vector2(600, 125), 1.0f, 0.0f, 0.0f));

            Button newGame = new Button(((Game1)baseGame).Content.Load<Texture2D>(@"Graphics/MainMenu/NewGameButton"),
                ((Game1)baseGame).Content.Load<Texture2D>(@"Graphics/MainMenu/NewGameButton-Hover"),
                ((Game1)baseGame).Content.Load<Texture2D>(@"Graphics/MainMenu/NewGameButton-Pressed"),
                new Vector2(690, 315));
            Button loadGame = new Button(((Game1)baseGame).Content.Load<Texture2D>(@"Graphics/MainMenu/LoadGameButton"),
                ((Game1)baseGame).Content.Load<Texture2D>(@"Graphics/MainMenu/LoadGameButton-Hover"),
                ((Game1)baseGame).Content.Load<Texture2D>(@"Graphics/MainMenu/LoadGameButton-Pressed"),
                new Vector2(690, 525));
            Button achievements = new Button(((Game1)baseGame).Content.Load<Texture2D>(@"Graphics/MainMenu/Achievements"),
                ((Game1)baseGame).Content.Load<Texture2D>(@"Graphics/MainMenu/Achievements-Hover"),
                ((Game1)baseGame).Content.Load<Texture2D>(@"Graphics/MainMenu/Achievements-Pressed"),
                new Vector2(690, 735));

            newGame.ButtonPressed += NewGame;
            loadGame.ButtonPressed += LoadGame;
            achievements.ButtonPressed += Achievements;

            Buttons.Add(newGame);
            Buttons.Add(loadGame);
            Buttons.Add(achievements);
            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            MouseState state = Mouse.GetState();

            foreach (ISprite item in Sprites)
                item.Update();

            foreach (Button item in Buttons)
                item.Update(state);

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                ((Game1)Game).Exit();

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            foreach (ISprite item in Sprites)
                item.Draw(spriteBatch);

            foreach (Button item in Buttons)
                item.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void NewGame()
        {
            ((Game1)Game).GameState = GameStates.CharacterSelect;
        }

        private void LoadGame()
        {
            ((Game1)Game).GameState = GameStates.LoadMenu;
        }

        private void Achievements()
        {
            ((Game1)Game).GameState = GameStates.Achievements;
        }
    }
}
