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
    public class ResearchMenu : Microsoft.Xna.Framework.DrawableGameComponent
    {
        public Dictionary<string, ResearchItem> ResearchTree { get; set; }
        public int ResearchCounter { get; set; }
        public string CurrentlyResearching { get; set; }

        bool showPrereqPopup;
        bool showCurrentlyResearchingPopup;
        ResearchCamera camera;
        SpriteBatch spriteBatch;
        SpriteFont MH30, MH45;
        
        Button OKButton, BackButton, FleetButton;
        Sprite ResearchMap, PrereqPopup, ResearchingPopup, Title, Tutorial;

        public ResearchMenu(Game game)
            : base(game)
        {
        }

        public override void Initialize()
        {
            showCurrentlyResearchingPopup = false;
            showPrereqPopup = false;
            ResearchCounter = 0;
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            camera = new ResearchCamera(((Game1)Game).GraphicsDevice.Viewport);
            ResearchMap = new Sprite(
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Layout"),
                new Vector2(100, -945), 1.0f, 0.0f, 0.1f);
            MH30 = ((Game1)Game).Content.Load<SpriteFont>("Graphics/Fonts/MyriadHebrew-30");
            MH45 = ((Game1)Game).Content.Load<SpriteFont>("Graphics/Fonts/MyriadHebrew-45");
            OKButton = new Button(
                ((Game1)Game).Content.Load<Texture2D>("Graphics/UI/OKButton"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/UI/OKButton-Hover"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/UI/OKButton-Pressed"),
                new Vector2(810, 675));
            OKButton.ButtonPressed += OKButton_ButtonPressed;
            BackButton = new Button(((Game1)Game).Content.Load<Texture2D>("Graphics/UI/BackButton"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/UI/BackButton-Hover"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/UI/BackButton-Pressed"),
                new Vector2(1050, 20));
            BackButton.ButtonPressed += BackButton_ButtonPressed;
            FleetButton = new Button(((Game1)Game).Content.Load<Texture2D>("Graphics/UI/FleetIcon"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/UI/FleetIcon-Hover"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/UI/FleetIcon-Pressed"),
                new Vector2(1175, 20));
            FleetButton.ButtonPressed += FleetButton_ButtonPressed;
            PrereqPopup = new Sprite(((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/PrereqMsg"),
                new Vector2(460, 300), 1.0f, 0.0f, 0.9f);
            ResearchingPopup = new Sprite(((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchingPopup"),
                new Vector2(460, 300), 1.0f, 0.0f, 0.9f);
            Title = new Sprite(((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Title"), 
                new Vector2(0, 0), 1.0f, 0.0f, 1.0f);
            CurrentlyResearching = "";
            ResearchCounter = 0;
            Tutorial = new Sprite(((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Tutorial1"), 
                new Vector2(430, 155), 1.0f, 0.0f, 0.9f);
            #region Research Items
            ResearchTree = new Dictionary<string, ResearchItem>();
            ResearchTree.Add("Space Flight", new ResearchItem(new List<string>(), "Space Flight", 0, 0,
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/SpaceFlight"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem-Hover"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem-Pressed"),
                new Vector2(100, 465), ResearchStates.Researched));
            ResearchTree.Add("Droid Control Systems", new ResearchItem(new List<string> { "Space Flight" },
                "Droid Control Systems", 250, 5,
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/DroidControlSystems"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem-Hover"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem-Pressed"),
                new Vector2(500, -240), ResearchStates.NotResearched));
            ResearchTree.Add("Hyperdrive", new ResearchItem(new List<string> { "Space Flight" },
                "Hyperdrive", 200, 5,
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Hyperdrive"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem-Hover"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem-Pressed"),
                new Vector2(500, 465), ResearchStates.NotResearched));
            ResearchTree.Add("Tractor Beams", new ResearchItem(new List<string> { "Space Flight" },
                "Tractor Beams", 2500, 20,
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/TractorBeam"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem-Hover"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem-Pressed"),
                new Vector2(2500, -945), ResearchStates.NotResearched));
            ResearchTree.Add("Planetary Production Management", new ResearchItem(new List<string> { "Tractor Beam" },
                 "Planetary Production Management", 1400, 14,
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/PPM"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem-Hover"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem-Pressed"),
                new Vector2(2900, -945), ResearchStates.NotResearched));
            ResearchTree.Add("Ion Weapons", new ResearchItem(new List<string> { "Hyperdrive" },
                 "Ion Weapons", 300, 6,
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/IonWeapons"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem-Hover"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem-Pressed"),
                new Vector2(900, -5), ResearchStates.NotResearched));
            ResearchTree.Add("Ion Engines", new ResearchItem(new List<string> { "Ion Weapons" },
                 "Ion Engines", 500, 7,
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/IonEngines"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem-Hover"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem-Pressed"),
                new Vector2(1300, -710), ResearchStates.NotResearched));
            ResearchTree.Add("Turbolasers", new ResearchItem(new List<string> { "Ion Engines" },
                 "Turbolasers", 1000, 10,
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Turbolaser"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem-Hover"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem-Pressed"),
                new Vector2(1700, -710), ResearchStates.NotResearched));
            ResearchTree.Add("Heavy Turbolasers", new ResearchItem(new List<string> { "Turbolasers" },
                 "Heavy Turbolasers", 1500, 15,
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/HeavyTurbolaser"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem-Hover"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem-Pressed"),
                new Vector2(2100, -710), ResearchStates.NotResearched));
            ResearchTree.Add("SEAL Shielding", new ResearchItem(new List<string> { "Heavy Turbolasers" },
                 "SEAL Shielding", 1200, 12,
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/SEALShields"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem-Hover"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem-Pressed"),
                new Vector2(2500, -710), ResearchStates.NotResearched));
            ResearchTree.Add("Proton Weapons", new ResearchItem(new List<string> { "Ion Weapons" },
                 "Proton Weapons", 500, 7,
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ProtonWeapons"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem-Hover"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem-Pressed"),
                new Vector2(1300, -475), ResearchStates.NotResearched));
            ResearchTree.Add("Heavy Ion Weapons", new ResearchItem(new List<string> { "Ion Weapons" },
                 "Heavy Ion Weapons", 1000, 10,
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/HeavyIonWeapons"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem-Hover"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem-Pressed"),
                new Vector2(1300, -240), ResearchStates.NotResearched));
            ResearchTree.Add("Naquadah-Trinium-Carbon Alloys", new ResearchItem(new List<string> { "Ion Weapons" },
                 "Naquadah-Trinium-Carbon Alloys", 1500, 15,
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/NTCAlloys"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem-Hover"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem-Pressed"),
                new Vector2(1300, -5), ResearchStates.NotResearched));
            ResearchTree.Add("Mass Drivers", new ResearchItem(new List<string> { "Hyperdrive" },
                 "Mass Drivers", 600, 6,
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/MassDrivers"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem-Hover"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem-Pressed"),
                new Vector2(900, 1640), ResearchStates.NotResearched));
            ResearchTree.Add("Magnetic Accelerator Cannons", new ResearchItem(new List<string> { "Mass Driver" },
                 "Magnetic Accelerator Cannons", 1500, 15,
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/MACs"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem-Hover"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem-Pressed"),
                new Vector2(1300, 1640), ResearchStates.NotResearched));
            ResearchTree.Add("Nuclear Missiles", new ResearchItem(new List<string> { "Magnetic Accelerator Cannons" },
                 "Nuclear Missiles", 1700, 17,
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/NukeMissiles"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem-Hover"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem-Pressed"),
                new Vector2(1700, 1640), ResearchStates.NotResearched));
            ResearchTree.Add("Nuclear Mines", new ResearchItem(new List<string> { "Magnetic Accelerator Cannons" },
                 "Nuclear Mines", 2000, 20,
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/NukeMines"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem-Hover"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem-Pressed"),
                new Vector2(1700, 1875), ResearchStates.NotResearched));
            ResearchTree.Add("Organic Hulls", new ResearchItem(new List<string> { "Hyperdrive" },
                 "researchitem", 700, 7,
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/OrganicHulls"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem-Hover"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem-Pressed"),
                new Vector2(900, 935), ResearchStates.NotResearched));
            ResearchTree.Add("Trinium-Organic Hulls", new ResearchItem(new List<string> { "Organic Hulls" },
                 "Trinium-Organic Hulls", 1000, 10,
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/TOHulls"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem-Hover"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem-Pressed"),
                new Vector2(1300, 935), ResearchStates.NotResearched));
            ResearchTree.Add("Regenerative Hulls", new ResearchItem(new List<string> { "Trinium-Organic Hulls" },
                 "Regenerative Hulls", 1500, 15,
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/RegenHulls"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem-Hover"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem-Pressed"),
                new Vector2(1700, 935), ResearchStates.NotResearched));
            ResearchTree.Add("Religious Indoctrination", new ResearchItem(new List<string> { "Hyperdrive" },
                 "Religious Indoctrination", 500, 7,
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ReligiousIndoctrination"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem-Hover"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem-Pressed"),
                new Vector2(900, 1170), ResearchStates.NotResearched));
            ResearchTree.Add("Naquadah Power Source", new ResearchItem(new List<string> { "Hyperdrive" },
                 "Naquadah Power Source", 700, 7,
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Naquadah"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem-Hover"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem-Pressed"),
                new Vector2(900, 1405), ResearchStates.NotResearched));
            ResearchTree.Add("Intergalactic Hyperdrive", new ResearchItem(new List<string> { "Hyperdrive" },
                 "Intergalactic Hyperdrive", 3000, 30,
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/IntergalacticHyperdrive"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem-Hover"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem-Pressed"),
                new Vector2(900, 465), ResearchStates.NotResearched));
            ResearchTree.Add("Alien Diplomacy", new ResearchItem(new List<string> { "Intergalactic Hyperdrive" },
                 "Alien Diplomacy", 6000, 60,
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/AlienDiplomacy"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem-Hover"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem-Pressed"),
                new Vector2(1300, 700), ResearchStates.NotResearched));
            ResearchTree.Add("FTL Engines", new ResearchItem(new List<string> { "Intergalactic Hyperdrive" },
                 "FTL Engines", 1400, 14,
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/FTLEngines"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem-Hover"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem-Pressed"),
                new Vector2(1300, 465), ResearchStates.NotResearched));
            ResearchTree.Add("Ring Transporters", new ResearchItem(
                new List<string> { "Religious Indoctrination", "Naquadah Power Source" }, "Ring Transporters", 1200, 12,
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/RingTransporters"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem-Hover"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem-Pressed"),
                new Vector2(1300, 1405), ResearchStates.NotResearched));
            ResearchTree.Add("Mobile Ring Transporters", new ResearchItem(new List<string> { "Ring Transporters" },
                 "Mobile Ring Transporters", 3500, 35,
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/MobRingTransporters"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem-Hover"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem-Pressed"),
                new Vector2(1700, 1405), ResearchStates.NotResearched));
            ResearchTree.Add("Plasma Weapons", new ResearchItem(new List<string> { "Intergalactic Hyperdrive" },
                 "Plasma Weapons", 1700, 17,
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/PlasmaWeapons"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem-Hover"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem-Pressed"),
                new Vector2(1300, 230), ResearchStates.NotResearched));
            ResearchTree.Add("Linked Weapons Systems", new ResearchItem(
                new List<string> { "Heavy Ion Weapons", "Planetary Production Management", "Plasma Weapons" }, "Linked Weapons Systems", 3600, 40,
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/LWS"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem-Hover"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem-Pressed"),
                new Vector2(3300, -5), ResearchStates.NotResearched));
            ResearchTree.Add("Pulse Cannons", new ResearchItem(new List<string> { "Plasma Weapons" },
                 "Pulse Cannons", 2000, 20,
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/PulseCannons"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem-Hover"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem-Pressed"),
                new Vector2(1700, 465), ResearchStates.NotResearched));
            ResearchTree.Add("Energy Projectors", new ResearchItem(
                new List<string> { "Pulse Cannons", "Religious Indoctrination" }, "Energy Projectors", 2800, 28,
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/EnergyProjectors"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem-Hover"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem-Pressed"),
                new Vector2(2100, 935), ResearchStates.NotResearched));
            ResearchTree.Add("Plasma Torpedoes", new ResearchItem(new List<string> { "Energy Projectors" },
                 "Plasma Torpedoes", 3000, 30,
                 ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/PlasmaTorpedoes"),
                 ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem"),
                 ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem-Hover"),
                 ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem-Pressed"),
                new Vector2(2500, 935), ResearchStates.NotResearched));
            ResearchTree.Add("Cloaking Technology", new ResearchItem(new List<string> { "Plasma Weapons" },
                 "Cloaking Technology", 2200, 25,
                 ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Cloak"),
                 ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem"),
                 ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem-Hover"),
                 ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem-Pressed"),
                new Vector2(1700, 230), ResearchStates.NotResearched));
            ResearchTree.Add("Drone Weapons", new ResearchItem(new List<string> { "Cloaking Technology" },
                 "Drone Weapons", 3000, 35,
                 ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Drones"),
                 ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem"),
                 ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem-Hover"),
                 ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem-Pressed"),
                new Vector2(2100, 230), ResearchStates.NotResearched));
            ResearchTree.Add("Zero-Point Energy", new ResearchItem(new List<string> { "Cloaking Technology" },
                 "Zero-Point Energy", 2500, 30,
                 ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ZPM"),
                 ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem"),
                 ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem-Hover"),
                 ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem-Pressed"),
                new Vector2(2100, 465), ResearchStates.NotResearched));
            ResearchTree.Add("Direct Neural Interfaces", new ResearchItem(new List<string> { "Drone Weapons" },
                 "Direct Neural Interfaces", 4000, 45,
                 ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/DNI"),
                 ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem"),
                 ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem-Hover"),
                 ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem-Pressed"),
                new Vector2(2500, 465), ResearchStates.NotResearched));
            ResearchTree.Add("Control Chairs", new ResearchItem(new List<string> { "Direct Neural Interfaces" },
                 "Control Chairs", 5000, 50,
                 ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ControlChair"),
                 ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem"),
                 ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem-Hover"),
                 ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem-Pressed"),
                new Vector2(2900, 935), ResearchStates.NotResearched));

            foreach (ResearchItem item in ResearchTree.Values)
                item.ResearchStarted += ResearchStarted;
            #endregion

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            MouseState state = Mouse.GetState();

            if (!((Game1)Game).User.ResearchTutorial)
                OKButton.Update(state);
            else
            {
                if (!showPrereqPopup && !showCurrentlyResearchingPopup)
                {
                    camera.Update(state);
                    foreach (ResearchItem item in ResearchTree.Values)
                        item.Update(state, camera);
                    FleetButton.Update(state);
                    BackButton.Update(state);
                }
                else
                    OKButton.Update(state);
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, null, null, null, null, camera.Translation);

            foreach (ResearchItem item in ResearchTree.Values)
                item.Draw(spriteBatch, MH45);

            ResearchMap.Draw(spriteBatch);

            spriteBatch.End();

            //Draw all non-world items
            spriteBatch.Begin();

            Title.Draw(spriteBatch);
            BackButton.Draw(spriteBatch);
            FleetButton.Draw(spriteBatch);

            if (!((Game1)Game).User.ResearchTutorial)
            {
                Tutorial.Draw(spriteBatch);
                spriteBatch.DrawString(MH30, "<<To: " + Player.Ranks[((Game1)Game).User.Rank] + " " + ((Game1)Game).User.Character + ">>", new Vector2(465, 335), Color.White);
            }

            if (showCurrentlyResearchingPopup)
                ResearchingPopup.Draw(spriteBatch);

            if (showPrereqPopup)
                PrereqPopup.Draw(spriteBatch);

            if (showCurrentlyResearchingPopup || showPrereqPopup || !((Game1)Game).User.ResearchTutorial)
                OKButton.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void DecrementResearchCounter()
        {
            if (ResearchCounter > 0)
                ResearchCounter--;
            else
                ResearchCompleted();

            if (ResearchCounter <= 0)
                ResearchCompleted();
        }

        void ResearchCompleted()
        {
            ResearchTree[CurrentlyResearching].ResearchState = ResearchStates.Researched;
            CurrentlyResearching = "";
            ResearchCounter = 0;
        }

        void ResearchStarted(ResearchItem sender)
        {
            //Only research something new if you aren't already researching something
            if (ResearchTree.Values.Where(x => x.ResearchState == ResearchStates.Researching).Count() == 0)
            {
                List<ResearchItem> Prerequisites = new List<ResearchItem>();
                List<ResearchItem> Researched = ResearchTree.Values.Where(x => x.ResearchState == ResearchStates.Researched).ToList<ResearchItem>();

                foreach (string item in sender.Prerequisites)
                    Prerequisites.Add(ResearchTree[item]);

                //Can't research unless you have the prereqs!
                if (!Prerequisites.Except(Researched).Any())
                {
                    sender.ResearchState = ResearchStates.Researching;
                    ResearchCounter = sender.TurnsToResearch;
                    CurrentlyResearching = sender.Name;
                }
                else
                    showPrereqPopup = true;
            }
            else
                showCurrentlyResearchingPopup = true;
        }

        void OKButton_ButtonPressed()
        {
            if (!((Game1)Game).User.ResearchTutorial)
                ((Game1)Game).User.ResearchTutorial = true;

            if (showPrereqPopup)
                showPrereqPopup = false;

            if (showCurrentlyResearchingPopup)
                showCurrentlyResearchingPopup = false;
        }

        void BackButton_ButtonPressed()
        {
            ((Game1)Game).GameState = ((Game1)Game).PreviousGameState;
        }

        void FleetButton_ButtonPressed()
        {
            ((Game1)Game).GameState = GameStates.Fleet;
        }
    }
}
