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
using FleetCom.Classes.Ships.Weapons;
using FleetCom.Classes.Ships.Asgard;


namespace FleetCom
{
    enum MenuStates
    {
        Shop,
        MyShips
    }

    public class FleetMenu : Microsoft.Xna.Framework.DrawableGameComponent
    {
        public Dictionary<string, IShip> Ships;
        public Dictionary<string, IWeapon> Weapons;

        SpriteBatch spriteBatch;

        Sprite Title, Tutorial, NoShipPopup, ISFPopup, ResearchPopup, SlotPopup;
        Texture2D ShipInfo;
        Button BackButton, ResearchButton, TutorialOKButton, OKButton, StoreButton, SellButton, MyFleetButton, NextPageButton, PreviousPageButton, BuyButton;
        ShipSlot ens, ltjg, lt, ltcdr, cdr, cpt, radm, vadm, adm, fadm;
        StoreItem one, two, three, four, five, six, seven, eight, nine, ten;
        SpriteFont MH15, MH45;

        int slotSelected;
        int page = 1;
        bool firstUpdate = true;
        bool noShipPopup = false;
        bool isfPopup = false;
        bool researchPopup = false;
        bool slotPopup = false;

        int switchCounter = 30;
        int pageSwitchCoutner = 15;
        int buyCounter = 10;

        IShip selected;

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
                new Vector2(450, 300), 1.0f, 0.0f, 0.9f);
            ResearchPopup = new Sprite(
                ((Game1)Game).Content.Load<Texture2D>("Graphics/FleetMenu/PrereqPopup"),
                new Vector2(450, 300), 1.0f, 0.0f, 1.0f);
            ISFPopup = new Sprite(
                ((Game1)Game).Content.Load<Texture2D>("Graphics/FleetMenu/ISFPopup"),
                new Vector2(450, 300), 1.0f, 0.0f, 1.0f);
            SlotPopup = new Sprite(
                ((Game1)Game).Content.Load<Texture2D>("Graphics/FleetMenu/SlotPopup"),
                new Vector2(450, 300), 1.0f, 0.0f, 1.0f);
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
            ltjg.ShipSlotSelected += ShipSlotSelected;
            lt.ShipSlotSelected += ShipSlotSelected;
            ltcdr.ShipSlotSelected += ShipSlotSelected;
            cpt.ShipSlotSelected += ShipSlotSelected;
            radm.ShipSlotSelected += ShipSlotSelected;
            vadm.ShipSlotSelected += ShipSlotSelected;
            adm.ShipSlotSelected += ShipSlotSelected;
            fadm.ShipSlotSelected += ShipSlotSelected;
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
                new Vector2(830, 650));
            TutorialOKButton = new Button(
                ((Game1)Game).Content.Load<Texture2D>("Graphics/UI/OKButton"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/UI/OKButton-Hover"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/UI/OKButton-Pressed"),
                new Vector2(850, 830));
            StoreButton = new Button(
                ((Game1)Game).Content.Load<Texture2D>("Graphics/FleetMenu/StoreButton"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/FleetMenu/StoreButton-Hover"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/FleetMenu/StoreButton-Pressed"),
                new Vector2(475, 300), 0.8f);
            SellButton = new Button(
                ((Game1)Game).Content.Load<Texture2D>("Graphics/FleetMenu/SellButton"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/FleetMenu/SellButton-Hover"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/FleetMenu/SellButton-Pressed"),
                new Vector2(1400, 950));
            BuyButton = new Button(
                ((Game1)Game).Content.Load<Texture2D>("Graphics/FleetMenu/BuyButton"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/FleetMenu/BuyButton-Hover"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/FleetMenu/BuyButton-Pressed"),
                new Vector2(1400, 950));
            MyFleetButton = new Button(
                ((Game1)Game).Content.Load<Texture2D>("Graphics/FleetMenu/MyFleetButton"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/FleetMenu/MyFleetButton-Hover"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/FleetMenu/MyFleetButton-Pressed"),
                new Vector2(455, 360), 0.8f);
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
            TutorialOKButton.ButtonPressed += TutorialOKButton_ButtonPressed;
            StoreButton.ButtonPressed += StoreButton_ButtonPressed;
            SellButton.ButtonPressed += RemoveButton_ButtonPressed;
            BuyButton.ButtonPressed += BuyButton_ButtonPressed;
            MyFleetButton.ButtonPressed += MyFleetButton_ButtonPressed;
            NextPageButton.ButtonPressed += NextPage_ButtonPressed;
            PreviousPageButton.ButtonPressed += PreviousPage_ButtonPressed;
            #endregion

            base.Initialize();
        }

        void InitializeShips()
        {
            ContentManager game = ((Game1)Game).Content;
            Dictionary<string, ResearchItem> researchTree = ((Game1)Game).ResearchMenu.ResearchTree;

            #region Weapons
            Weapons = new Dictionary<string, IWeapon>();
            Weapons.Add("Railgun", new Railguns_Ref(
                game.Load<Texture2D>("Graphics/Ships/Weapons/Railgun")));
            Weapons.Add("Missile", new Missile_Ref(
                game.Load<Texture2D>("Graphics/Ships/Weapons/Missile")));
            Weapons.Add("Plasma Beam", new PlasmaBeam_Ref(
                game.Load<Texture2D>("Graphics/Ships/Weapons/PlasmaBeam")));
            Weapons.Add("Ion Gun", new IonGunR(
                game.Load<Texture2D>("Graphics/Ships/Weapons/IonGun")));
            #endregion
            #region Ships
            Ships = new Dictionary<string, IShip>();

            Ships.Add("F-302", new F302_Ref(
                game.Load<Texture2D>("Graphics/Ships/F302/F-302"),
                game.Load<Texture2D>("Graphics/Ships/F302/Icon"),
                game.Load<Texture2D>("Graphics/Ships/F302/InfoCard"),
                researchTree, Weapons));
            Ships.Add("BC-303", new BC303_Ref(
                game.Load<Texture2D>("Graphics/Ships/BC-303/BC-303"),
                game.Load<Texture2D>("Graphics/Ships/BC-303/Icon"),
                game.Load<Texture2D>("Graphics/Ships/BC-303/InfoCard"),
                researchTree, Weapons));
            Ships.Add("BC-304", new BC304_Ref(
                game.Load<Texture2D>("Graphics/Ships/BC-304/BC-304"),
                game.Load<Texture2D>("Graphics/Ships/BC-304/Icon"),
                game.Load<Texture2D>("Graphics/Ships/BC-304/InfoCard"),
                researchTree, Weapons));
            Ships.Add("Bilskirnir", new Bilskirnir_Ref(
                game.Load<Texture2D>("Graphics/Ships/Bilskirnir/Bilskirnir"),
                game.Load<Texture2D>("Graphics/Ships/Bilskirnir/Icon"),
                game.Load<Texture2D>("Graphics/Ships/Bilskirnir/InfoCard"),
                researchTree, Weapons));
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
            //result.Add("Ori Fighter", new Ship(new List<ResearchItem> { game.ResearchMenu.ResearchTree["Religious Indoctrination"], game.ResearchMenu.ResearchTree["Mobile Ring Transporters"] }));
            //result.Add("Ori Warship", new Ship(new List<ResearchItem> { game.ResearchMenu.ResearchTree["Religious Indoctrination"], game.ResearchMenu.ResearchTree["Control Chairs"] }));
            //result.Add("Seed Ship", new Ship(new List<ResearchItem> { game.ResearchMenu.ResearchTree["FTL Engines"] }));
            //result.Add("Gateship", new Ship(new List<ResearchItem> { game.ResearchMenu.ResearchTree["Drone Weapons"] }));
            //result.Add("Aurora", new Ship(new List<ResearchItem> { game.ResearchMenu.ResearchTree["Zero-Point Energy"], game.ResearchMenu.ResearchTree["Direct Neural Interfaces"] }));
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
            #endregion

            #region Store Slots
            Texture2D SS, SSHover, SSPressed, SSBlocked, Popup;
            SS = ((Game1)Game).Content.Load<Texture2D>("Graphics/FleetMenu/ShipSlot");
            SSHover = ((Game1)Game).Content.Load<Texture2D>("Graphics/FleetMenu/ShipSlot-Hover");
            SSPressed = ((Game1)Game).Content.Load<Texture2D>("Graphics/FleetMenu/ShipSlot-Pressed");
            SSBlocked = ((Game1)Game).Content.Load<Texture2D>("Graphics/FleetMenu/BlockedShipSlot");
            Popup = ((Game1)Game).Content.Load<Texture2D>("Graphics/FleetMenu/ResearchPopup");

            one = new StoreItem(SS, SSHover, SSPressed, SSBlocked,
                ((Game1)Game).Content.Load<Texture2D>("Graphics/FleetMenu/Ensign"),
                Popup, new Vector2(50, 500), 1, ((Game1)Game), MH15, Ships["F-302"]);

            two = new StoreItem(SS, SSHover, SSPressed, SSBlocked,
                ((Game1)Game).Content.Load<Texture2D>("Graphics/FleetMenu/Ensign"),
                Popup, new Vector2(275, 500), 2, ((Game1)Game), MH15, Ships["F-302"]);

            three = new StoreItem(SS, SSHover, SSPressed, SSBlocked,
                ((Game1)Game).Content.Load<Texture2D>("Graphics/FleetMenu/Ensign"),
                Popup, new Vector2(500, 500), 3, ((Game1)Game), MH15, Ships["F-302"]);

            four = new StoreItem(SS, SSHover, SSPressed, SSBlocked,
                ((Game1)Game).Content.Load<Texture2D>("Graphics/FleetMenu/Ensign"),
                Popup, new Vector2(725, 500), 4, ((Game1)Game), MH15, Ships["F-302"]);

            five = new StoreItem(SS, SSHover, SSPressed, SSBlocked,
                ((Game1)Game).Content.Load<Texture2D>("Graphics/FleetMenu/Ensign"),
                Popup, new Vector2(950, 500), 5, ((Game1)Game), MH15, Ships["F-302"]);

            six = new StoreItem(SS, SSHover, SSPressed, SSBlocked,
                ((Game1)Game).Content.Load<Texture2D>("Graphics/FleetMenu/Ensign"),
                Popup, new Vector2(50, 725), 6, ((Game1)Game), MH15, Ships["F-302"]);

            seven = new StoreItem(SS, SSHover, SSPressed, SSBlocked,
                ((Game1)Game).Content.Load<Texture2D>("Graphics/FleetMenu/Ensign"),
                Popup, new Vector2(275, 725), 7, ((Game1)Game), MH15, Ships["F-302"]);

            eight = new StoreItem(SS, SSHover, SSPressed, SSBlocked,
                ((Game1)Game).Content.Load<Texture2D>("Graphics/FleetMenu/Ensign"),
                Popup, new Vector2(500, 725), 8, ((Game1)Game), MH15, Ships["F-302"]);

            nine = new StoreItem(SS, SSHover, SSPressed, SSBlocked,
                ((Game1)Game).Content.Load<Texture2D>("Graphics/FleetMenu/Ensign"),
                Popup, new Vector2(725, 725), 9, ((Game1)Game), MH15, Ships["F-302"]);

            ten = new StoreItem(SS, SSHover, SSPressed, SSBlocked,
                ((Game1)Game).Content.Load<Texture2D>("Graphics/FleetMenu/Ensign"),
                Popup, new Vector2(950, 725), 10, ((Game1)Game), MH15, Ships["F-302"]);

            one.StoreItemSelected += ItemSelected_Handler;
            two.StoreItemSelected += ItemSelected_Handler;
            three.StoreItemSelected += ItemSelected_Handler;
            four.StoreItemSelected += ItemSelected_Handler;
            five.StoreItemSelected += ItemSelected_Handler;
            six.StoreItemSelected += ItemSelected_Handler;
            seven.StoreItemSelected += ItemSelected_Handler;
            eight.StoreItemSelected += ItemSelected_Handler;
            nine.StoreItemSelected += ItemSelected_Handler;
            ten.StoreItemSelected += ItemSelected_Handler;
            #endregion
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

            ens.Ship = Ships[((Game1)Game).User.Fleet[0]];

            if (ltjg.Available)
                ltjg.Ship = Ships[((Game1)Game).User.Fleet[1]];

            if (lt.Available)
                lt.Ship = Ships[((Game1)Game).User.Fleet[2]]; ;

            if (ltcdr.Available)
                ltcdr.Ship = Ships[((Game1)Game).User.Fleet[3]];

            if (cdr.Available)
                cdr.Ship = Ships[((Game1)Game).User.Fleet[4]];

            if (cpt.Available)
                cpt.Ship = Ships[((Game1)Game).User.Fleet[5]];

            if (radm.Available)
                radm.Ship = Ships[((Game1)Game).User.Fleet[6]];

            if (vadm.Available)
                vadm.Ship = Ships[((Game1)Game).User.Fleet[7]];

            if (adm.Available)
                adm.Ship = Ships[((Game1)Game).User.Fleet[8]];

            if (fadm.Available)
                fadm.Ship = Ships[((Game1)Game).User.Fleet[9]];
        }

        public override void Update(GameTime gameTime)
        {
            if (switchCounter > 0)
                switchCounter--;

            if (pageSwitchCoutner > 0)
                pageSwitchCoutner--;

            if (buyCounter > 0)
                buyCounter--;

            if (firstUpdate)
            {
                firstUpdate = false;
                Refresh();
            }

            MouseState state = Mouse.GetState();

            if (!((Game1)Game).User.FleetTutorial)
                TutorialOKButton.Update(state);
            else if (researchPopup || isfPopup || slotPopup)
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
                            if (switchCounter == 0)
                                UpdateStore(state);
                            break;

                        case MenuStates.MyShips:
                            if (switchCounter == 0)
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

            one.Update(state);
            two.Update(state);
            three.Update(state);
            four.Update(state);
            five.Update(state);
            six.Update(state);
            seven.Update(state);
            eight.Update(state);
            nine.Update(state);
            ten.Update(state);

            switch (slotSelected)
            {
                case 1:
                    one.Selected = true;
                    two.Selected = false;
                    three.Selected = false;
                    four.Selected = false;
                    five.Selected = false;
                    six.Selected = false;
                    seven.Selected = false;
                    eight.Selected = false;
                    nine.Selected = false;
                    ten.Selected = false;

                    if (one.Ship != null)
                    {
                        ShipInfo = one.Ship.InfoTexture;
                        selected = one.Ship;
                    }
                    else
                        ShipInfo = null;
                    break;

                case 2:
                    one.Selected = false;
                    two.Selected = true;
                    three.Selected = false;
                    four.Selected = false;
                    five.Selected = false;
                    six.Selected = false;
                    seven.Selected = false;
                    eight.Selected = false;
                    nine.Selected = false;
                    ten.Selected = false;

                    if (two.Ship != null)
                    {
                        ShipInfo = two.Ship.InfoTexture;
                        selected = two.Ship;
                    }
                    else
                        ShipInfo = null;
                    break;

                case 3:
                    one.Selected = false;
                    two.Selected = false;
                    three.Selected = true;
                    four.Selected = false;
                    five.Selected = false;
                    six.Selected = false;
                    seven.Selected = false;
                    eight.Selected = false;
                    nine.Selected = false;
                    ten.Selected = false;

                    if (three.Ship != null)
                    {
                        ShipInfo = three.Ship.InfoTexture;
                        selected = three.Ship;
                    }
                    else
                        ShipInfo = null;
                    break;

                case 4:
                    one.Selected = false;
                    two.Selected = false;
                    three.Selected = false;
                    four.Selected = true;
                    five.Selected = false;
                    six.Selected = false;
                    seven.Selected = false;
                    eight.Selected = false;
                    nine.Selected = false;
                    ten.Selected = false;

                    if (four.Ship != null)
                    {
                        ShipInfo = four.Ship.InfoTexture;
                        selected = four.Ship;
                    }
                    else
                        ShipInfo = null;
                    break;

                case 5:
                    one.Selected = false;
                    two.Selected = false;
                    three.Selected = false;
                    four.Selected = false;
                    five.Selected = true;
                    six.Selected = false;
                    seven.Selected = false;
                    eight.Selected = false;
                    nine.Selected = false;
                    ten.Selected = false;

                    if (five.Ship != null)
                    {
                        ShipInfo = five.Ship.InfoTexture;
                        selected = five.Ship;
                    }
                    else
                        ShipInfo = null;
                    break;

                case 6:
                    one.Selected = false;
                    two.Selected = false;
                    three.Selected = false;
                    four.Selected = false;
                    five.Selected = false;
                    six.Selected = true;
                    seven.Selected = false;
                    eight.Selected = false;
                    nine.Selected = false;
                    ten.Selected = false;

                    if (six.Ship != null)
                    {
                        ShipInfo = six.Ship.InfoTexture;
                        selected = six.Ship;
                    }
                    else
                        ShipInfo = null;
                    break;

                case 7:
                    one.Selected = false;
                    two.Selected = false;
                    three.Selected = false;
                    four.Selected = false;
                    five.Selected = false;
                    six.Selected = false;
                    seven.Selected = true;
                    eight.Selected = false;
                    nine.Selected = false;
                    ten.Selected = false;

                    if (seven.Ship != null)
                    {
                        ShipInfo = seven.Ship.InfoTexture;
                        selected = seven.Ship;
                    }
                    else
                        ShipInfo = null;
                    break;

                case 8:
                    one.Selected = false;
                    two.Selected = false;
                    three.Selected = false;
                    four.Selected = false;
                    five.Selected = false;
                    six.Selected = false;
                    seven.Selected = false;
                    eight.Selected = true;
                    nine.Selected = false;
                    ten.Selected = false;

                    if (eight.Ship != null)
                    {
                        ShipInfo = eight.Ship.InfoTexture;
                        selected = eight.Ship;
                    }
                    else
                        ShipInfo = null;
                    break;

                case 9:
                    one.Selected = false;
                    two.Selected = false;
                    three.Selected = false;
                    four.Selected = false;
                    five.Selected = false;
                    six.Selected = false;
                    seven.Selected = false;
                    eight.Selected = false;
                    nine.Selected = true;
                    ten.Selected = false;

                    if (nine.Ship != null)
                    {
                        ShipInfo = nine.Ship.InfoTexture;
                        selected = nine.Ship;
                    }
                    else
                        ShipInfo = null;
                    break;

                case 10:
                    one.Selected = false;
                    two.Selected = false;
                    three.Selected = false;
                    four.Selected = false;
                    five.Selected = false;
                    six.Selected = false;
                    seven.Selected = false;
                    eight.Selected = false;
                    nine.Selected = false;
                    ten.Selected = true;

                    if (ten.Ship != null)
                    {
                        ShipInfo = ten.Ship.InfoTexture;
                        selected = ten.Ship;
                    }
                    else
                        ShipInfo = null;
                    break;
            }

            if (ShipInfo != null)
                BuyButton.Update(state);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);

            if (!((Game1)Game).User.FleetTutorial)
            {
                Tutorial.Draw(spriteBatch);
                TutorialOKButton.Draw(spriteBatch);
            }

            if (researchPopup || isfPopup || slotPopup)
                OKButton.Draw(spriteBatch);

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

            if (ShipInfo != null)
            {
                spriteBatch.Draw(ShipInfo, new Vector2(1250, 225), null, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.1f);

                if (MenuState == MenuStates.MyShips)
                    SellButton.Draw(spriteBatch);
                else
                    BuyButton.Draw(spriteBatch);
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
        }

        void DrawStore(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(MH45, String.Format("Current Funds: {0} Generic Galactic Credits", 
                ((Game1)Game).User.GenericGalacticCredits), new Vector2(5, 225), Color.White);
            spriteBatch.DrawString(MH45, String.Format("{0}/5", page), new Vector2(570, 985), Color.White);

            PreviousPageButton.Draw(spriteBatch);
            NextPageButton.Draw(spriteBatch);
            MyFleetButton.Draw(spriteBatch);
            one.Draw(spriteBatch);
            two.Draw(spriteBatch);
            three.Draw(spriteBatch);
            four.Draw(spriteBatch);
            five.Draw(spriteBatch);
            six.Draw(spriteBatch);
            seven.Draw(spriteBatch);
            eight.Draw(spriteBatch);
            nine.Draw(spriteBatch);
            ten.Draw(spriteBatch);

            if (researchPopup)
                ResearchPopup.Draw(spriteBatch);
            else if (isfPopup)
                ISFPopup.Draw(spriteBatch);
            else if (slotPopup)
                SlotPopup.Draw(spriteBatch);
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

        void ItemSelected_Handler(StoreItem item)
        {
            slotSelected = item.Slot;
        }

        void PreviousPage_ButtonPressed()
        {
            if (pageSwitchCoutner == 0)
            {
                pageSwitchCoutner = 15;

                if (page > 1)
                    page--;

                changePage(page);
            }
        }

        void NextPage_ButtonPressed()
        {
            if (pageSwitchCoutner == 0)
            {
                pageSwitchCoutner = 15;

                if (page < 5)
                    page++;

                changePage(page);
            }
        }

        void changePage(int page)
        {
            switch (page)
            {
                case 1:
                    one.Ship = Ships["F-302"];
                    two.Ship = Ships["F-302"];
                    three.Ship = Ships["F-302"];
                    four.Ship = Ships["F-302"];
                    five.Ship = Ships["F-302"];
                    six.Ship = Ships["F-302"];
                    seven.Ship = Ships["F-302"];
                    eight.Ship = Ships["F-302"];
                    nine.Ship = Ships["F-302"];
                    ten.Ship = Ships["F-302"];
                    break;

                case 2:
                    one.Ship = Ships["BC-303"];
                    two.Ship = Ships["BC-303"];
                    three.Ship = Ships["BC-303"];
                    four.Ship = Ships["BC-303"];
                    five.Ship = Ships["BC-303"];
                    six.Ship = Ships["BC-303"];
                    seven.Ship = Ships["BC-303"];
                    eight.Ship = Ships["BC-303"];
                    nine.Ship = Ships["BC-303"];
                    ten.Ship = Ships["BC-303"];
                    break;

                case 3:
                    one.Ship = Ships["BC-304"];
                    two.Ship = Ships["BC-304"];
                    three.Ship = Ships["BC-304"];
                    four.Ship = Ships["BC-304"];
                    five.Ship = Ships["BC-304"];
                    six.Ship = Ships["BC-304"];
                    seven.Ship = Ships["BC-304"];
                    eight.Ship = Ships["BC-304"];
                    nine.Ship = Ships["BC-304"];
                    ten.Ship = Ships["BC-304"];
                    break;

                case 4:
                    one.Ship = Ships["Bilskirnir"];
                    two.Ship = Ships["Bilskirnir"];
                    three.Ship = Ships["Bilskirnir"];
                    four.Ship = Ships["Bilskirnir"];
                    five.Ship = Ships["Bilskirnir"];
                    six.Ship = Ships["Bilskirnir"];
                    seven.Ship = Ships["Bilskirnir"];
                    eight.Ship = Ships["Bilskirnir"];
                    nine.Ship = Ships["Bilskirnir"];
                    ten.Ship = Ships["Bilskirnir"];
                    break;

                case 5:
                    one.Ship = Ships["F-302"];
                    two.Ship = Ships["BC-303"];
                    three.Ship = Ships["BC-304"];
                    four.Ship = Ships["Bilskirnir"];
                    five.Ship = Ships["F-302"];
                    six.Ship = Ships["F-302"];
                    seven.Ship = Ships["F-302"];
                    eight.Ship = Ships["F-302"];
                    nine.Ship = Ships["F-302"];
                    ten.Ship = Ships["F-302"];
                    break;
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
            else if (isfPopup)
                isfPopup = false;
            else if (researchPopup)
                researchPopup = false;
            else if (slotPopup)
                slotPopup = false;

            switchCounter = 30;
        }

        void TutorialOKButton_ButtonPressed()
        {
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

        void BuyButton_ButtonPressed()
        {
            if (buyCounter == 0)
            {
                buyCounter = 10;
                bool bought = true;
                List<ResearchItem> researched = ((Game1)Game).ResearchMenu.ResearchTree.Values.Where(x => x.ResearchState == ResearchStates.Researched).ToList<ResearchItem>();
                List<ResearchItem> prereqs = selected.Prerequisites;

                //Check for funds and research, then show slot select popup; if slot occupied, show sell popup
                //Don't have the prereqs
                if (prereqs.Except(researched).Any())
                    researchPopup = true;
                else if (((Game1)Game).User.GenericGalacticCredits < selected.Cost)
                    isfPopup = true;
                else
                {
                    if (ens.Available && ens.Ship == null)
                        ens.Ship = selected;
                    else if (ltjg.Available && ltjg.Ship == null)
                        ltjg.Ship = selected;
                    else if (lt.Available && lt.Ship == null)
                            lt.Ship = selected;
                    else if (ltcdr.Available && ltcdr.Ship == null)
                            ltcdr.Ship = selected;
                    else if (cdr.Available && cdr.Ship == null)
                            cdr.Ship = selected;
                    else if (cpt.Available && cpt.Ship == null)
                            cpt.Ship = selected;
                    else if (radm.Available && radm.Ship == null)
                            radm.Ship = selected;
                    else if (vadm.Available && vadm.Ship == null)
                            vadm.Ship = selected;
                    else if (adm.Available && adm.Ship == null)
                            adm.Ship = selected;
                    else if (fadm.Available && fadm.Ship == null)
                            fadm.Ship = selected;
                    else
                    {
                        slotPopup = true;
                        bought = false;
                    }

                    if (bought)
                    {
                        ((Game1)Game).User.GenericGalacticCredits -= selected.Cost;
                        MenuState = MenuStates.MyShips;
                        switchCounter = 30;
                    }
                }
            }
        }
    }
}
