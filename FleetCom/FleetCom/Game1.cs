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
        Fleet,
        InGame,
        GameOver
    }

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        public Player User;
        public GameStates GameState;
        public GameStates PreviousGameState;
        public StarCluster selectedCluster;
        public StarSystem selectedSystem;
        public Dictionary<string, Ship> Ships;
        #region GameComponents
        MainMenu MainMenu;
        CharacterSelect CharacterSelect;
        GalaxyMap GalaxyMap;
        public ResearchMenu ResearchMenu;
        public FleetMenu Fleet;
        #endregion

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            #region Intialize Game Components & State
            GameState = GameStates.MainMenu;
            PreviousGameState = GameState;
            MainMenu = new MainMenu(this);
            CharacterSelect = new CharacterSelect(this);
            GalaxyMap = new GalaxyMap(this);
            ResearchMenu = new ResearchMenu(this);
            Fleet = new FleetMenu(this);

            Components.Add(MainMenu);
            Components.Add(CharacterSelect);
            Components.Add(GalaxyMap);
            Components.Add(ResearchMenu);
            Components.Add(Fleet);
            #endregion
            #region Set Graphics Stuff
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            graphics.IsFullScreen = true;
            IsMouseVisible = true;
            graphics.ApplyChanges();
            Window.Title = "FLEETCOM 1.0";
            #endregion
            
            base.Initialize();
            Ships = Utils.InitializeShipsList(this);
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here
        }

        protected override void UnloadContent()
        {
            if (User != null)
                User.SaveCharacter(this);
        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (Keyboard.GetState().IsKeyDown(Keys.LeftAlt) && Keyboard.GetState().IsKeyDown(Keys.F4))
                this.Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift) && Keyboard.GetState().IsKeyDown(Keys.F5))
                this.Exit();

            switch (GameState)
            {
                case GameStates.MainMenu:
                    MainMenu.Enabled = false;
                    MainMenu.Visible = false;
                    CharacterSelect.Visible = false;
                    CharacterSelect.Enabled = false;
                    GalaxyMap.Enabled = false;
                    GalaxyMap.Visible = false;
                    ResearchMenu.Enabled = false;
                    ResearchMenu.Visible = false;
                    Fleet.Enabled = true;
                    Fleet.Visible = true;

                    //MainMenu.Enabled = true;
                    //MainMenu.Visible = true;
                    //CharacterSelect.Visible = false;
                    //CharacterSelect.Enabled = false;
                    //GalaxyMap.Enabled = false;
                    //GalaxyMap.Visible = false;
                    //ResearchMenu.Enabled = false;
                    //ResearchMenu.Visible = false;
                    //Fleet.Enabled = false;
                    //Fleet.Visible = false;
                    break;

                case GameStates.CharacterSelect:
                    MainMenu.Enabled = false;
                    MainMenu.Visible = false;
                    CharacterSelect.Visible = true;
                    CharacterSelect.Enabled = true;
                    GalaxyMap.Enabled = false;
                    GalaxyMap.Visible = false;
                    ResearchMenu.Enabled = false;
                    ResearchMenu.Visible = false;
                    Fleet.Enabled = false;
                    Fleet.Visible = false;
                    break;

                case GameStates.GalaxyMap:
                    MainMenu.Enabled = false;
                    MainMenu.Visible = false;
                    CharacterSelect.Visible = false;
                    CharacterSelect.Enabled = false;
                    GalaxyMap.Enabled = true;
                    GalaxyMap.Visible = true;
                    ResearchMenu.Enabled = false;
                    ResearchMenu.Visible = false;
                    Fleet.Enabled = false;
                    Fleet.Visible = false;
                    break;

                case GameStates.Research:
                    MainMenu.Enabled = false;
                    MainMenu.Visible = false;
                    CharacterSelect.Visible = false;
                    CharacterSelect.Enabled = false;
                    GalaxyMap.Enabled = false;
                    GalaxyMap.Visible = false;
                    ResearchMenu.Enabled = true;
                    ResearchMenu.Visible = true;
                    Fleet.Enabled = false;
                    Fleet.Visible = false;
                    break;

                case GameStates.Fleet:
                    MainMenu.Enabled = false;
                    MainMenu.Visible = false;
                    CharacterSelect.Visible = false;
                    CharacterSelect.Enabled = false;
                    GalaxyMap.Enabled = false;
                    GalaxyMap.Visible = false;
                    ResearchMenu.Enabled = false;
                    ResearchMenu.Visible = false;
                    Fleet.Enabled = true;
                    Fleet.Visible = true;
                    break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(34, 31, 32));

            base.Draw(gameTime);
        }
    }
}
