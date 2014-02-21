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

namespace FleetCom
{
    public enum GameStates
    {
        MainMenu,
        LoadMenu,
        Achievements,
        CharacterSelect,
        BaseReview,
        GalaxyMap,
        PauseMenu,
        Research,
        InGame,
        GameOver
    }

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        public Player User;
        public GameStates GameState;
        public GameStates PreviousGameState;
        public StarCluster selectedCluster;
        public StarSystem selectedSystem;
        public Dictionary<string, ResearchItem> ResearchTree;
        public Dictionary<string, Ship> Ships;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        #region GameComponents
        MainMenu mainMenu;
        CharacterSelect characterSelect;
        GalaxyMap galaxyMap;
        ResearchMenu researchMenu;
        #endregion

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            #region Intialize Game Components & State
            GameState = GameStates.MainMenu;
            PreviousGameState = GameState;
            mainMenu = new MainMenu(this);
            characterSelect = new CharacterSelect(this);
            galaxyMap = new GalaxyMap(this);
            researchMenu = new ResearchMenu(this);

            Components.Add(mainMenu);
            Components.Add(characterSelect);
            Components.Add(galaxyMap);
            Components.Add(researchMenu);
            #endregion
            #region Set Graphics Stuff
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            //graphics.IsFullScreen = true;
            IsMouseVisible = true;
            graphics.ApplyChanges();
            Window.Title = "FLEETCOM 1.0";
            #endregion
            ResearchTree = Utils.InitializeResearchTree(this);
            Ships = Utils.InitializeShipsList(this);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            if (User != null)
                User.SaveCharacter(this);
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (Keyboard.GetState().IsKeyDown(Keys.LeftAlt) && Keyboard.GetState().IsKeyDown(Keys.F4))
                this.Exit();

            switch (GameState)
            {
                case GameStates.MainMenu:
                    mainMenu.Enabled = true;
                    mainMenu.Visible = true;
                    characterSelect.Visible = false;
                    characterSelect.Enabled = false;
                    galaxyMap.Enabled = false;
                    galaxyMap.Visible = false;
                    researchMenu.Enabled = false;
                    researchMenu.Visible = false;
                    break;

                case GameStates.CharacterSelect:
                    mainMenu.Enabled = false;
                    mainMenu.Visible = false;
                    characterSelect.Visible = true;
                    characterSelect.Enabled = true;
                    galaxyMap.Enabled = false;
                    galaxyMap.Visible = false;
                    researchMenu.Enabled = false;
                    researchMenu.Visible = false;
                    break;

                case GameStates.GalaxyMap:
                    mainMenu.Enabled = false;
                    mainMenu.Visible = false;
                    characterSelect.Visible = false;
                    characterSelect.Enabled = false;
                    galaxyMap.Enabled = true;
                    galaxyMap.Visible = true;
                    researchMenu.Enabled = false;
                    researchMenu.Visible = false;
                    break;

                case GameStates.Research:
                    mainMenu.Enabled = false;
                    mainMenu.Visible = false;
                    characterSelect.Visible = false;
                    characterSelect.Enabled = false;
                    galaxyMap.Enabled = false;
                    galaxyMap.Visible = false;
                    researchMenu.Enabled = true;
                    researchMenu.Visible = true;
                    break;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(34, 31, 32));

            base.Draw(gameTime);
        }
    }
}
