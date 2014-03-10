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
        #region GameComponents
        MainMenu MainMenu;
        CharacterSelect CharacterSelect;
        GalaxyMap GalaxyMap;
        public ResearchMenu ResearchMenu;
        public FleetMenu FleetMenu;
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
            FleetMenu = new FleetMenu(this);

            Components.Add(MainMenu);
            Components.Add(CharacterSelect);
            Components.Add(GalaxyMap);
            Components.Add(ResearchMenu);
            Components.Add(FleetMenu);
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
            ////For testing -- remove this
            //User = new Player(Characters.Aggressive, true,
            //    Content.Load<string[]>("Data/StarClusterNames").ToList<string>(),
            //    Content.Load<Texture2D>("Graphics/IncursionMap/OwnedTexture"),
            //    Content.Load<Texture2D>("Graphics/IncursionMap/UnderAttackSprite"),
            //    Content.Load<Texture2D>("Graphics/IncursionMap/NormalTexture"),
            //    Content.Load<Texture2D>("Graphics/IncursionMap/SystemStatus"),
            //    Content.Load<SpriteFont>("Graphics/Fonts/MyriadHebrew-45"),
            //    Content.Load<SpriteFont>("Graphics/Fonts/MyriadHebrew-75"),
            //    this);
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
                    MainMenu.Enabled = true;
                    MainMenu.Visible = true;
                    CharacterSelect.Visible = false;
                    CharacterSelect.Enabled = false;
                    GalaxyMap.Enabled = false;
                    GalaxyMap.Visible = false;
                    ResearchMenu.Enabled = false;
                    ResearchMenu.Visible = false;
                    FleetMenu.Enabled = false;
                    FleetMenu.Visible = false;
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
                    FleetMenu.Enabled = false;
                    FleetMenu.Visible = false;
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
                    FleetMenu.Enabled = false;
                    FleetMenu.Visible = false;
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
                    FleetMenu.Enabled = false;
                    FleetMenu.Visible = false;
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
                    FleetMenu.Enabled = true;
                    FleetMenu.Visible = true;
                    break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(34, 31, 32));

            base.Draw(gameTime);
        }

        public void ChangeState(GameStates newState)
        {
            PreviousGameState = GameState;
            GameState = newState;
        }
    }
}
