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
using FleetCom.Classes.Ships;
using FleetCom.Classes.Ships.Tauri;


namespace FleetCom
{
    enum MenuStates
    {
        Shop,
        MyShips
    }

    public class FleetMenu : Microsoft.Xna.Framework.DrawableGameComponent
    {
        public List<IShip> Ships;

        SpriteBatch spriteBatch;

        Sprite Title, Tutorial, NoShipPopup;
        Texture2D ShipInfo;
        Button BackButton, ResearchButton, OKButton, StoreButton, SellButton, MyFleetButton, NextPageButton, PreviousPageButton, Buy;
        ShipSlot ens, ltjg, lt, ltcdr, cdr, cpt, radm, vadm, adm, fadm;
        StoreItem one, two, three, four, five, six, seven, eight, nine, ten;
        SpriteFont MH15, MH45;

        int slotSelected;
        int page = 1;
        bool firstUpdate = true;
        bool noShipPopup = false;
        bool isfPopup = false;
        int switchCounter = 30;
        int pageSwitchCoutner = 15;

        MenuStates MenuState;

        public FleetMenu(Game game)
            : base(game)
        {
        }

        public override void Initialize()
        {
            slotSelected = 1;
            MenuState = MenuStates.MyShips;

            spriteBatch = new SpriteBatch(((Game1)Game).GraphicsDevice);

            #region Sprites
            Title = new Sprite(
                ((Game1)Game).Content.Load<Texture2D>("Graphics/FleetMenu/Title"),
                new Vector2(0, 0), 1.0f, 0.0f, 1.0f);
            Tutorial = new Sprite(
                ((Game1)Game).Content.Load<Texture2D>("Graphics/FleetMenu/Tutorial"),
                new Vector2(450, 300), 1.0f, 0.0f, 0.9f);
            NoShipPopup = new Sprite(
                ((Game1)Game).Content.Load<Texture2D>("Graphics/FleetMenu/NoShip"),
                new Vector2(480, 450), 1.0f, 0.0f, 0.9f);
            #endregion

            MH15 = ((Game1)Game).Content.Load<SpriteFont>("Graphics/Fonts/MyriadHebrew-15");
            MH45 = ((Game1)Game).Content.Load<SpriteFont>("Graphics/Fonts/MyriadHebrew-45");

            #region ranks
            Texture2D SS, SSHover, SSPressed, SSBlocked, Rank;
            SS = ((Game1)Game).Content.Load<Texture2D>("Graphics/FleetMenu/ShipSlot");
            SSHover = ((Game1)Game).Content.Load<Texture2D>("Graphics/FleetMenu/ShipSlot-Hover");
            SSPressed = ((Game1)Game).Content.Load<Texture2D>("Graphics/FleetMenu/ShipSlot-Pressed");
            SSBlocked = ((Game1)Game).Content.Load<Texture2D>("Graphics/FleetMenu/BlockedShipSlot");
            Rank = ((Game1)Game).Content.Load<Texture2D>("Graphics/FleetMenu/RankPopup");

            ens = new ShipSlot(SS, SSHover, SSPressed, SSBlocked,
                ((Game1)Game).Content.Load<Texture2D>("Graphics/FleetMenu/Ensign"),
                Rank, new Vector2(75, 475), "ENS", 0, ((Game1)Game), true, MH15);
            ltjg = new ShipSlot(SS, SSHover, SSPressed, SSBlocked,
                ((Game1)Game).Content.Load<Texture2D>("Graphics/FleetMenu/LieutenantJr"),
                Rank, new Vector2(300, 475), "LTJG", 1000, ((Game1)Game), false, MH15);
            lt = new ShipSlot(SS, SSHover, SSPressed, SSBlocked,
                ((Game1)Game).Content.Load<Texture2D>("Graphics/FleetMenu/Lieutenant"),
                Rank, new Vector2(525, 475), "LT", 3000, ((Game1)Game), false, MH15);
            ltcdr = new ShipSlot(SS, SSHover, SSPressed, SSBlocked,
                ((Game1)Game).Content.Load<Texture2D>("Graphics/FleetMenu/LieutenantCommander"),
                Rank, new Vector2(750, 475), "LTCDR", 6000, ((Game1)Game), false, MH15);
            cdr = new ShipSlot(SS, SSHover, SSPressed, SSBlocked,
                ((Game1)Game).Content.Load<Texture2D>("Graphics/FleetMenu/Commander"),
                Rank, new Vector2(975, 475), "CDR", 10000, ((Game1)Game), false, MH15);
            cpt = new ShipSlot(SS, SSHover, SSPressed, SSBlocked,
                ((Game1)Game).Content.Load<Texture2D>("Graphics/FleetMenu/Captain"),
                Rank, new Vector2(75, 700), "CPT", 15000, ((Game1)Game), false, MH15);
            radm = new ShipSlot(SS, SSHover, SSPressed, SSBlocked,
                ((Game1)Game).Content.Load<Texture2D>("Graphics/FleetMenu/RearAdmiral"),
                Rank, new Vector2(300, 700), "RADM", 21000, ((Game1)Game), false, MH15);
            vadm = new ShipSlot(SS, SSHover, SSPressed, SSBlocked,
                ((Game1)Game).Content.Load<Texture2D>("Graphics/FleetMenu/ViceAdmiral"),
                Rank, new Vector2(525, 700), "VADM", 28000, ((Game1)Game), false, MH15);
            adm = new ShipSlot(SS, SSHover, SSPressed, SSBlocked,
                ((Game1)Game).Content.Load<Texture2D>("Graphics/FleetMenu/Admiral"),
                Rank, new Vector2(750, 700), "ADM", 36000, ((Game1)Game), false, MH15);
            fadm = new ShipSlot(SS, SSHover, SSPressed, SSBlocked,
                ((Game1)Game).Content.Load<Texture2D>("Graphics/FleetMenu/FleetAdmiral"),
                Rank, new Vector2(975, 700), "FADM", 45000, ((Game1)Game), false, MH15);
            ens.ShipSlotSelected += ShipSlotSelected;
            #endregion

            #region Store Slots
            //TODO: Class should be ready to use. Instantiate, draw, and update. Then test. Then replicate for all 42 ships.
            one = new StoreItem(SS, SSHover, SSPressed, SSBlocked,
                ((Game1)Game).Content.Load<Texture2D>("Graphics/FleetMenu/Ensign"),
                Rank, new Vector2(75, 475), ((Game1)Game), MH15);
            #endregion

            InitializeShips();

            #region buttons
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
            StoreButton = new Button(
                ((Game1)Game).Content.Load<Texture2D>("Graphics/FleetMenu/StoreButton"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/FleetMenu/StoreButton-Hover"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/FleetMenu/StoreButton-Pressed"),
                new Vector2(475, 300));
            SellButton = new Button(
                ((Game1)Game).Content.Load<Texture2D>("Graphics/FleetMenu/SellButton"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/FleetMenu/SellButton-Hover"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/FleetMenu/SellButton-Pressed"),
                new Vector2(1400, 950));
            MyFleetButton = new Button(
                ((Game1)Game).Content.Load<Texture2D>("Graphics/FleetMenu/MyFleetButton"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/FleetMenu/MyFleetButton-Hover"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/FleetMenu/MyFleetButton-Pressed"),
                new Vector2(455, 360));
            NextPageButton = new Button(
                ((Game1)Game).Content.Load<Texture2D>("Graphics/FleetMenu/NextButton"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/FleetMenu/NextButton-Hover"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/FleetMenu/NextButton-Pressed"),
                new Vector2(680, 960));
            PreviousPageButton = new Button(
                ((Game1)Game).Content.Load<Texture2D>("Graphics/FleetMenu/PreviousButton"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/FleetMenu/PreviousButton-Hover"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/FleetMenu/PreviousButton-Pressed"),
                new Vector2(430, 960));

            BackButton.ButtonPressed += BackButton_ButtonPressed;
            ResearchButton.ButtonPressed += ResearchButton_ButtonPressed;
            OKButton.ButtonPressed += OKButton_ButtonPressed;
            StoreButton.ButtonPressed += StoreButton_ButtonPressed;
            SellButton.ButtonPressed += RemoveButton_ButtonPressed;
            MyFleetButton.ButtonPressed += MyFleetButton_ButtonPressed;
            NextPageButton.ButtonPressed += NextPage_ButtonPressed;
            PreviousPageButton.ButtonPressed += PreviousPage_ButtonPressed;
            #endregion

            base.Initialize();
        }

        public void Refresh()
        {
            ens.Available = ((Game1)Game).User.HasAchievedRank("ENS");
            ltjg.Available = ((Game1)Game).User.HasAchievedRank("LTJG");
            lt.Available = ((Game1)Game).User.HasAchievedRank("LT");
            ltcdr.Available = ((Game1)Game).User.HasAchievedRank("LTCDR");
            cdr.Available = ((Game1)Game).User.HasAchievedRank("CDR");
            cpt.Available = ((Game1)Game).User.HasAchievedRank("CPT");
            radm.Available = ((Game1)Game).User.HasAchievedRank("RADM");
            vadm.Available = ((Game1)Game).User.HasAchievedRank("VADM");
            adm.Available = ((Game1)Game).User.HasAchievedRank("ADM");
            fadm.Available = ((Game1)Game).User.HasAchievedRank("FADM");

            ens.Ship = Ships.Where(x => x.Name == ((Game1)Game).User.Fleet[0]).FirstOrDefault<IShip>();

            if (ltjg.Available)
                ltjg.Ship = Ships.Where(x => x.Name == ((Game1)Game).User.Fleet[1]).FirstOrDefault<IShip>();

            if (lt.Available)
                lt.Ship = Ships.Where(x => x.Name == ((Game1)Game).User.Fleet[2]).FirstOrDefault<IShip>();

            if (ltcdr.Available)
                ltcdr.Ship = Ships.Where(x => x.Name == ((Game1)Game).User.Fleet[3]).FirstOrDefault<IShip>();

            if (cdr.Available)
                cdr.Ship = Ships.Where(x => x.Name == ((Game1)Game).User.Fleet[4]).FirstOrDefault<IShip>();

            if (cpt.Available)
                cpt.Ship = Ships.Where(x => x.Name == ((Game1)Game).User.Fleet[5]).FirstOrDefault<IShip>();

            if (radm.Available)
                radm.Ship = Ships.Where(x => x.Name == ((Game1)Game).User.Fleet[6]).FirstOrDefault<IShip>();

            if (vadm.Available)
                vadm.Ship = Ships.Where(x => x.Name == ((Game1)Game).User.Fleet[7]).FirstOrDefault<IShip>();

            if (adm.Available)
                adm.Ship = Ships.Where(x => x.Name == ((Game1)Game).User.Fleet[8]).FirstOrDefault<IShip>();

            if (fadm.Available)
                fadm.Ship = Ships.Where(x => x.Name == ((Game1)Game).User.Fleet[9]).FirstOrDefault<IShip>();
        }

        public override void Update(GameTime gameTime)
        {
            if (switchCounter > 0)
                switchCounter--;

            if (pageSwitchCoutner > 0)
                pageSwitchCoutner--;

            if (firstUpdate)
            {
                firstUpdate = false;
                Refresh();
            }

            MouseState state = Mouse.GetState();

            if (false/*!((Game1)Game).User.FleetTutorial*/)
            {
                OKButton.Update(state);
            }
            else
            {
                if (noShipPopup)
                    OKButton.Update(state);
                else
                {
                    BackButton.Update(state);
                    ResearchButton.Update(state);

                    switch (MenuState)
                    {
                        case MenuStates.Shop:
                            UpdateStore(state);
                            break;

                        case MenuStates.MyShips:
                            UpdateMyShips(state);
                            break;
                    }
                }
            }

            base.Update(gameTime);
        }

        void UpdateMyShips(MouseState state)
        {
            StoreButton.Update(state);

            ens.Update(state);
            ltjg.Update(state);
            lt.Update(state);
            ltcdr.Update(state);
            cdr.Update(state);
            cpt.Update(state);
            radm.Update(state);
            vadm.Update(state);
            adm.Update(state);
            fadm.Update(state);

            switch (slotSelected)
            {
                case 1:
                    ens.Selected = true;
                    ltjg.Selected = false;
                    lt.Selected = false;
                    ltcdr.Selected = false;
                    cdr.Selected = false;
                    cpt.Selected = false;
                    radm.Selected = false;
                    vadm.Selected = false;
                    adm.Selected = false;
                    fadm.Selected = false;

                    if (ens.Ship != null)
                        ShipInfo = ens.Ship.InfoTexture;
                    else
                        ShipInfo = null;
                    break;

                case 2:
                    ens.Selected = false;
                    ltjg.Selected = true;
                    lt.Selected = false;
                    ltcdr.Selected = false;
                    cdr.Selected = false;
                    cpt.Selected = false;
                    radm.Selected = false;
                    vadm.Selected = false;
                    adm.Selected = false;
                    fadm.Selected = false;

                    if (ltjg.Ship != null)
                        ShipInfo = ltjg.Ship.InfoTexture;
                    else
                        ShipInfo = null;
                    break;

                case 3:
                    ens.Selected = false;
                    ltjg.Selected = false;
                    lt.Selected = true;
                    ltcdr.Selected = false;
                    cdr.Selected = false;
                    cpt.Selected = false;
                    radm.Selected = false;
                    vadm.Selected = false;
                    adm.Selected = false;
                    fadm.Selected = false;

                    if (lt.Ship != null)
                        ShipInfo = lt.Ship.InfoTexture;
                    else
                        ShipInfo = null;
                    break;

                case 4:
                    ens.Selected = false;
                    ltjg.Selected = false;
                    lt.Selected = false;
                    ltcdr.Selected = true;
                    cdr.Selected = false;
                    cpt.Selected = false;
                    radm.Selected = false;
                    vadm.Selected = false;
                    adm.Selected = false;
                    fadm.Selected = false;

                    if (ltcdr.Ship != null)
                        ShipInfo = ltcdr.Ship.InfoTexture;
                    else
                        ShipInfo = null;
                    break;
                    
                case 5:
                    ens.Selected = false;
                    ltjg.Selected = false;
                    lt.Selected = false;
                    ltcdr.Selected = false;
                    cdr.Selected = true;
                    cpt.Selected = false;
                    radm.Selected = false;
                    vadm.Selected = false;
                    adm.Selected = false;
                    fadm.Selected = false;

                    if (cdr.Ship != null)
                        ShipInfo = cdr.Ship.InfoTexture;
                    else
                        ShipInfo = null;
                    break;

                case 6:
                    ens.Selected = false;
                    ltjg.Selected = false;
                    lt.Selected = false;
                    ltcdr.Selected = false;
                    cdr.Selected = false;
                    cpt.Selected = true;
                    radm.Selected = false;
                    vadm.Selected = false;
                    adm.Selected = false;
                    fadm.Selected = false;

                    if (cpt.Ship != null)
                        ShipInfo = cpt.Ship.InfoTexture;
                    else
                        ShipInfo = null;
                    break;

                case 7:
                    ens.Selected = false;
                    ltjg.Selected = false;
                    lt.Selected = false;
                    ltcdr.Selected = false;
                    cdr.Selected = false;
                    cpt.Selected = false;
                    radm.Selected = true;
                    vadm.Selected = false;
                    adm.Selected = false;
                    fadm.Selected = false;

                    if (radm.Ship != null)
                        ShipInfo = radm.Ship.InfoTexture;
                    else
                        ShipInfo = null;
                    break;

                case 8:
                    ens.Selected = false;
                    ltjg.Selected = false;
                    lt.Selected = false;
                    ltcdr.Selected = false;
                    cdr.Selected = false;
                    cpt.Selected = false;
                    radm.Selected = false;
                    vadm.Selected = true;
                    adm.Selected = false;
                    fadm.Selected = false;

                    if (vadm.Ship != null)
                        ShipInfo = vadm.Ship.InfoTexture;
                    else
                        ShipInfo = null;
                    break;

                case 9:
                    ens.Selected = false;
                    ltjg.Selected = false;
                    lt.Selected = false;
                    ltcdr.Selected = false;
                    cdr.Selected = false;
                    cpt.Selected = false;
                    radm.Selected = false;
                    vadm.Selected = false;
                    adm.Selected = true;
                    fadm.Selected = false;

                    if (adm.Ship != null)
                        ShipInfo = adm.Ship.InfoTexture;
                    else
                        ShipInfo = null;
                    break;

                case 10:
                    ens.Selected = false;
                    ltjg.Selected = false;
                    lt.Selected = false;
                    ltcdr.Selected = false;
                    cdr.Selected = false;
                    cpt.Selected = false;
                    radm.Selected = false;
                    vadm.Selected = false;
                    adm.Selected = false;
                    fadm.Selected = true;

                    if (fadm.Ship != null)
                        ShipInfo = fadm.Ship.InfoTexture;
                    else
                        ShipInfo = null;
                    break;
            }

            if (ShipInfo != null)
                SellButton.Update(state);
        }

        void UpdateStore(MouseState state)
        {
            MyFleetButton.Update(state);
            PreviousPageButton.Update(state);
            NextPageButton.Update(state);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);

            if (false/*!((Game1)Game).User.FleetTutorial*/)
            {
                Tutorial.Draw(spriteBatch);
                OKButton.Draw(spriteBatch);
            }

            Title.Draw(spriteBatch);
            BackButton.Draw(spriteBatch);
            ResearchButton.Draw(spriteBatch);

            if (noShipPopup)
            {
                NoShipPopup.Draw(spriteBatch);
                OKButton.Draw(spriteBatch);
            }

            switch (MenuState)
            {
                case MenuStates.Shop:
                    DrawStore(spriteBatch);
                    break;

                case MenuStates.MyShips:
                    DrawMyShips(spriteBatch);
                    break;
            }

            

            spriteBatch.End();

            base.Draw(gameTime);
        }

        void DrawMyShips(SpriteBatch spriteBatch)
        {
            StoreButton.Draw(spriteBatch);
            ens.Draw(spriteBatch);
            ltjg.Draw(spriteBatch);
            lt.Draw(spriteBatch);
            ltcdr.Draw(spriteBatch);
            cdr.Draw(spriteBatch);
            cpt.Draw(spriteBatch);
            radm.Draw(spriteBatch);
            vadm.Draw(spriteBatch);
            adm.Draw(spriteBatch);
            fadm.Draw(spriteBatch);

            if (ShipInfo != null)
            {
                spriteBatch.Draw(ShipInfo, new Vector2(1250, 225), null, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.1f);
                SellButton.Draw(spriteBatch);
            }
        }

        void DrawStore(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(MH45, String.Format("Current Funds: {0} Generic Galactic Credits", 
                ((Game1)Game).User.GenericGalacticCredits), new Vector2(5, 225), Color.White);
            spriteBatch.DrawString(MH45, String.Format("{0}/5", page), new Vector2(570, 985), Color.White);

            PreviousPageButton.Draw(spriteBatch);
            NextPageButton.Draw(spriteBatch);
            MyFleetButton.Draw(spriteBatch);
        }

        void InitializeShips()
        {
            Ships = new List<IShip>();

            Ships.Add(new F302_Ref(
                ((Game1)Game).Content.Load<Texture2D>("Graphics/Ships/F302/F-302"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/Ships/F302/Icon"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/Ships/F302/InfoCard"),
                new List<ResearchItem> { ((Game1)Game).ResearchMenu.ResearchTree["Space Flight"] },
                null, null));
            //result.Add("BC-303", new Ship(new List<ResearchItem> { game.ResearchMenu.ResearchTree["Hyperdrive"] } ));
            //result.Add("BC-304", new Ship(new List<ResearchItem> { game.ResearchMenu.ResearchTree["Intergalactic Hyperdrive"] }));
            //result.Add("BC-304 Refit", new Ship(new List<ResearchItem> { game.ResearchMenu.ResearchTree["Alien Diplomacy"], game.ResearchMenu.ResearchTree["Cloaking Technology"] }));
            //result.Add("Bilskirnir", new Ship(new List<ResearchItem> { game.ResearchMenu.ResearchTree["Ion Weapons"] }));
            //result.Add("O'Neill", new Ship(new List<ResearchItem> { game.ResearchMenu.ResearchTree["Naquadah-Trinium-Carbon Alloys"] }));
            //result.Add("Death Glider", new Ship(new List<ResearchItem> { game.ResearchMenu.ResearchTree["Naquadah Power Source"] }));
            //result.Add("Ha'tak", new Ship(new List<ResearchItem> { game.ResearchMenu.ResearchTree["Religious Indoctrination"], game.ResearchMenu.ResearchTree["Ring Transporters"] }));
            //result.Add("Dart", new Ship(new List<ResearchItem> { game.ResearchMenu.ResearchTree["Organic Hulls"] }));
            //result.Add("Wraith Cruiser", new Ship(new List<ResearchItem> { game.ResearchMenu.ResearchTree["Trinium-Organic Hulls"] }));
            //result.Add("Hive Ship", new Ship(new List<ResearchItem> { game.ResearchMenu.ResearchTree["Regenerative Hulls"] }));
            //result.Add("Ori Fighter", new Ship(new List<ResearchItem> { game.ResearchMenu.ResearchTree["Religious Indoctrination"] }));
            //result.Add("Ori Warship", new Ship(new List<ResearchItem> { game.ResearchMenu.ResearchTree["Religious Indoctrination"], game.ResearchMenu.ResearchTree["Control Chairs"] }));
            //result.Add("Seed Ship", new Ship(new List<ResearchItem> { game.ResearchMenu.ResearchTree["FTL Engines"] }));
            //result.Add("Gateship", new Ship(new List<ResearchItem> { game.ResearchMenu.ResearchTree["Drone Weapons"] }));
            //result.Add("Aurora", new Ship(new List<ResearchItem> { game.ResearchMenu.ResearchTree["Drone Weapons"], game.ResearchMenu.ResearchTree["Zero-Point Energy"], game.ResearchMenu.ResearchTree["Direct Neural Interfaces"] }));
            //result.Add("TIE Fighter", new Ship(new List<ResearchItem> { game.ResearchMenu.ResearchTree["Ion Engines"] }));
            //result.Add("Acclamator", new Ship(new List<ResearchItem> { game.ResearchMenu.ResearchTree["Turbolasers"] }));
            //result.Add("Victory", new Ship(new List<ResearchItem> { game.ResearchMenu.ResearchTree["Ion Engines"], game.ResearchMenu.ResearchTree["Tractor Beams"] }));
            //result.Add("Imperial", new Ship(new List<ResearchItem> { game.ResearchMenu.ResearchTree["Heavy Turbolasers"], game.ResearchMenu.ResearchTree["Planetary Production Management"] }));
            //result.Add("X-Wing", new Ship(new List<ResearchItem> { game.ResearchMenu.ResearchTree["Proton Weapons"] }));
            //result.Add("Nebulon-B", new Ship(new List<ResearchItem> { game.ResearchMenu.ResearchTree["Turbolasers"] }));
            //result.Add("Liberty", new Ship(new List<ResearchItem> { game.ResearchMenu.ResearchTree["Tractor Beams"], game.ResearchMenu.ResearchTree["Heavy Turbolasers"] }));
            //result.Add("Home One", new Ship(new List<ResearchItem> { game.ResearchMenu.ResearchTree["SEAL Shielding"] }));
            //result.Add("Sith Fighter", new Ship(new List<ResearchItem> { game.ResearchMenu.ResearchTree["Ion Engines"] }));
            //result.Add("Leviathan", new Ship(new List<ResearchItem> { game.ResearchMenu.ResearchTree["Turbolasers"], game.ResearchMenu.ResearchTree["Tractor Beams"] }));
            //result.Add("Aurek", new Ship(new List<ResearchItem> { game.ResearchMenu.ResearchTree["Proton Weapons"] }));
            //result.Add("Hammerhead", new Ship(new List<ResearchItem> { game.ResearchMenu.ResearchTree["Tractor Beams"], game.ResearchMenu.ResearchTree["Proton Weapons"] }));
            //result.Add("Vulture Droid", new Ship(new List<ResearchItem> { game.ResearchMenu.ResearchTree["Droid Control Systems"] }));
            //result.Add("Lucrehulk", new Ship(new List<ResearchItem> { game.ResearchMenu.ResearchTree["Droid Control Systems"], game.ResearchMenu.ResearchTree["Heavy Turbolasers"] }));
            //result.Add("Providence", new Ship(new List<ResearchItem> { game.ResearchMenu.ResearchTree["Proton Weapons"], game.ResearchMenu.ResearchTree["Heavy Turbolasers"] }));
            //result.Add("Crusader", new Ship(new List<ResearchItem> { game.ResearchMenu.ResearchTree["Hyperdrive"] }));
            //result.Add("Keldabe", new Ship(new List<ResearchItem> { game.ResearchMenu.ResearchTree["Turbolasers"], game.ResearchMenu.ResearchTree["Heavy Ion Weapons"] }));
            //result.Add("Vengeance", new Ship(new List<ResearchItem> { game.ResearchMenu.ResearchTree["Mass Drivers"] }));
            //result.Add("Aggressor", new Ship(new List<ResearchItem> { game.ResearchMenu.ResearchTree["Linked Weapons Systems"] }));
            //result.Add("Longsword", new Ship(new List<ResearchItem> { game.ResearchMenu.ResearchTree["Mass Drivers"] }));
            //result.Add("Charon", new Ship(new List<ResearchItem> { game.ResearchMenu.ResearchTree["Magnetic Accelerator Cannons"] }));
            //result.Add("Halcyon", new Ship(new List<ResearchItem> { game.ResearchMenu.ResearchTree["Nuclear Missiles"] }));
            //result.Add("Phoenix", new Ship(new List<ResearchItem> { game.ResearchMenu.ResearchTree["Nuclear Missiles"], game.ResearchMenu.ResearchTree["Nuclear Mines"] }));
            //result.Add("Seraph", new Ship(new List<ResearchItem> { game.ResearchMenu.ResearchTree["Religious Indoctrination"], game.ResearchMenu.ResearchTree["Plasma Weapons"] }));
            //result.Add("Heavy Corvette", new Ship(new List<ResearchItem> { game.ResearchMenu.ResearchTree["Religious Indoctrination"], game.ResearchMenu.ResearchTree["Plasma Weapons"] }));
            //result.Add("CCS", new Ship(new List<ResearchItem> { game.ResearchMenu.ResearchTree["Energy Projectors"] }));
            //result.Add("CAS", new Ship(new List<ResearchItem> { game.ResearchMenu.ResearchTree["Plasma Torpedoes"] }));
        }

        bool IsFleetEmpty()
        {
            bool result = true;

            foreach (string item in ((Game1)Game).User.Fleet)
                if (item != null)
                    if (item != "")
                        result = false;

            return result;
        }

        void ShipSlotSelected(ShipSlot slot)
        {
            switch (slot.RankKey)
            {
                case "ENS":
                    slotSelected = 1;
                    break;

                case "LTJG":
                    slotSelected = 2;
                    break;

                case "LT":
                    slotSelected = 3;
                    break;

                case "LTCDR":
                    slotSelected = 4;
                    break;

                case "CDR":
                    slotSelected = 5;
                    break;

                case "CPT":
                    slotSelected = 6;
                    break;

                case "RADM":
                    slotSelected = 7;
                    break;

                case "VADM":
                    slotSelected = 8;
                    break;

                case "ADM":
                    slotSelected = 9;
                    break;

                case "FADM":
                    slotSelected = 10;
                    break;
            }
        }

        void PreviousPage_ButtonPressed()
        {
            if (pageSwitchCoutner == 0)
            {
                pageSwitchCoutner = 15;
                if (page > 1)
                    page--;
            }
        }

        void NextPage_ButtonPressed()
        {
            if (pageSwitchCoutner == 0)
            {
                pageSwitchCoutner = 15;
                if (page < 5)
                    page++;
            }
        }

        void ResearchButton_ButtonPressed()
        {
            if (!IsFleetEmpty())
                ((Game1)Game).GameState = GameStates.Research;
            else
            {
                noShipPopup = true;
            }
        }

        void BackButton_ButtonPressed()
        {
            if (!IsFleetEmpty())
                ((Game1)Game).GameState = ((Game1)Game).PreviousGameState;
            else
            {
                noShipPopup = true;
            }
        }

        void OKButton_ButtonPressed()
        {
            if (noShipPopup)
                noShipPopup = false;
            else
                ((Game1)Game).User.FleetTutorial = true;
        }

        void StoreButton_ButtonPressed()
        {
            if (switchCounter == 0)
            {
                switchCounter = 30;
                MenuState = MenuStates.Shop;
            }
        }

        void MyFleetButton_ButtonPressed()
        {
            if (switchCounter == 0)
            {
                switchCounter = 30;
                MenuState = MenuStates.MyShips;
            }
        }

        void RemoveButton_ButtonPressed()
        {
            switch (slotSelected)
            {
                case 1:
                    if (ens.Ship != null)
                    {
                        ((Game1)Game).User.GenericGalacticCredits += ens.Ship.Cost;
                        ens.Ship = null;
                        ((Game1)Game).User.Fleet[0] = null;
                    }
                    break;

                case 2:
                    if (ltjg.Ship != null)
                    {
                        ((Game1)Game).User.GenericGalacticCredits += ltjg.Ship.Cost;
                        ltjg.Ship = null;
                        ((Game1)Game).User.Fleet[1] = null;
                    }
                    break;

                case 3:
                    if (lt.Ship != null)
                    {
                        ((Game1)Game).User.GenericGalacticCredits += lt.Ship.Cost;
                        lt.Ship = null;
                        ((Game1)Game).User.Fleet[2] = null;
                    }
                    break;

                case 4:
                    if (ltcdr.Ship != null)
                    {
                        ((Game1)Game).User.GenericGalacticCredits += ltcdr.Ship.Cost;
                        ltcdr.Ship = null;
                        ((Game1)Game).User.Fleet[3] = null;
                    }
                    break;

                case 5:
                    if (cdr.Ship != null)
                    {
                        ((Game1)Game).User.GenericGalacticCredits += cdr.Ship.Cost;
                        cdr.Ship = null;
                        ((Game1)Game).User.Fleet[4] = null;
                    }
                    break;

                case 6:
                    if (cpt.Ship != null)
                    {
                        ((Game1)Game).User.GenericGalacticCredits += cpt.Ship.Cost;
                        cpt.Ship = null;
                        ((Game1)Game).User.Fleet[5] = null;
                    }
                    break;

                case 7:
                    if (radm.Ship != null)
                    {
                        ((Game1)Game).User.GenericGalacticCredits += radm.Ship.Cost;
                        radm.Ship = null;
                        ((Game1)Game).User.Fleet[6] = null;
                    }
                    break;

                case 8:
                    if (vadm.Ship != null)
                    {
                        ((Game1)Game).User.GenericGalacticCredits += vadm.Ship.Cost;
                        vadm.Ship = null;
                        ((Game1)Game).User.Fleet[7] = null;
                    }
                    break;

                case 9:
                    if (adm.Ship != null)
                    {
                        ((Game1)Game).User.GenericGalacticCredits += adm.Ship.Cost;
                        adm.Ship = null;
                        ((Game1)Game).User.Fleet[8] = null;
                    }
                    break;

                case 10:
                    if (fadm.Ship != null)
                    {
                        ((Game1)Game).User.GenericGalacticCredits += fadm.Ship.Cost;
                        fadm.Ship = null;
                        ((Game1)Game).User.Fleet[9] = null;
                    }
                    break;
            }
        }
    }
}
