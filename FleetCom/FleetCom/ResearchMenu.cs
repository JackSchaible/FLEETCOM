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

        Camera camera;
        SpriteBatch spriteBatch;
        SpriteFont font;

        Sprite ResearchMap;

        public ResearchMenu(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            camera = new Camera(((Game1)Game).GraphicsDevice.Viewport);
            ResearchMap = new Sprite(
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Layout"),
                new Vector2(100, -945), 1.0f, 0.0f, 0.1f);
            font = ((Game1)Game).Content.Load<SpriteFont>("Graphics/Fonts/MyriadHebrew-45");

            #region Research Items
            ResearchTree = new Dictionary<string, ResearchItem>();
            ResearchTree.Add("Space Flight", new ResearchItem(new List<string>(), "Space Flight", 0, 0,
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/SpaceFlight"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem-Hover"),
                ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/ResearchItem-Pressed"),
                new Vector2(100, 465), ResearchStates.Researched));
            //ResearchTree.Add("Droid Control Systems", new ResearchItem(new List<string> { "Space Flight" }, 
            //    "Droid Control Systems", 250, 5,
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Droid Control Systems"),
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Droid Control Systems-Hover"),
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Droid Control Systems-Pressed"),
            //    new Vector2(500, -240), ResearchStates.NotResearched));
            //ResearchTree.Add("Hyperdrive", new ResearchItem(new List<string> { "Space Flight" },  
            //    "Hyperdrive", 200, 5,
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Hyperdrive"),
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Hyperdrive-Hover"),
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Hyperdrive-Pressed"),
            //    new Vector2(500, 70), ResearchStates.NotResearched));
            //ResearchTree.Add("Tractor Beams", new ResearchItem(new List<string> { "Space Flight" },  
            //    "Tractor Beams", 2500, 20,
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Tractor Beam"),
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Tractor Beam-Hover"),
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Tractor Beam-Pressed"),
            //    new Vector2(2500, -945), ResearchStates.NotResearched));
            //ResearchTree.Add("Planetary Production Management", new ResearchItem(new List<string> { "Tractor Beam" },
            //     "Planetary Production Management", 1400, 14,
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Planetary Production Management"),
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Planetary Production Management-Hover"),
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Planetary Production Management-Pressed"),
            //    new Vector2(2900, -945), ResearchStates.NotResearched));
            //ResearchTree.Add("Ion Weapons", new ResearchItem(new List<string> { "Hyperdrive" }, 
            //     "Ion Weapons", 300, 6,
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Ion Weapons"),
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Ion Weapons-Hover"),
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Ion Weapons-Pressed"),
            //    new Vector2(895, -612), ResearchStates.NotResearched));
            //ResearchTree.Add("Ion Engines", new ResearchItem(new List<string> { "Ion Weapons" }, 
            //     "Ion Engines", 500, 7,
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Ion Engines"),
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Ion Engines-Hover"),
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Ion Engines-Pressed"),
            //    new Vector2(1300, -710), ResearchStates.NotResearched));
            //ResearchTree.Add("Turbolasers", new ResearchItem(new List<string> { "Ion Engines" },
            //     "Turbolasers", 1000, 10,
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Turbolasers"),
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Turbolasers-Hover"),
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Turbolasers-Pressed"),
            //    new Vector2(1700, -710), ResearchStates.NotResearched));
            //ResearchTree.Add("Heavy Turbolasers", new ResearchItem(new List<string> { "Turbolasers" }, 
            //     "Heavy Turbolasers", 1500, 15,
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Heavy Turbolasers"),
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Heavy Turbolasers-Hover"),
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Heavy Turbolasers-Pressed"),
            //    new Vector2(2100, -710), ResearchStates.NotResearched));
            //ResearchTree.Add("SEAL Shielding", new ResearchItem(new List<string> { "Heavy Turbolasers" },
            //     "SEAL Shielding", 1200, 12,
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/SEAL Shielding"),
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/SEAL Shielding-Hover"),
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/SEAL Shielding-Pressed"),
            //    new Vector2(2500, -710), ResearchStates.NotResearched));
            //ResearchTree.Add("Proton Weapons", new ResearchItem(new List<string> { "Ion Weapons" },
            //     "Proton Weapons", 500, 7,
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Proton Weapons"),
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Proton Weapons-Hover"),
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Proton Weapons-Pressed"),
            //    new Vector2(1300, -475), ResearchStates.NotResearched));
            //ResearchTree.Add("Heavy Ion Weapons", new ResearchItem(new List<string> { "Ion Weapons" },
            //     "Heavy Ion Weapons", 1000, 10,
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Heavy Ion Weapons"),
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Heavy Ion Weapons-Hover"),
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Heavy Ion Weapons-Pressed"),
            //    new Vector2(1300, -240), ResearchStates.NotResearched));
            //ResearchTree.Add("Naquadah-Trinium-Carbon Alloys", new ResearchItem(new List<string> { "Ion Weapons" },
            //     "Naquadah-Trinium-Carbon Alloys", 1500, 15,
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Naquadah-Trinium-Carbon Alloys"),
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Naquadah-Trinium-Carbon Alloys-Hover"),
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Naquadah-Trinium-Carbon Alloys-Pressed"),
            //    new Vector2(1275, 5), ResearchStates.NotResearched));
            //ResearchTree.Add("Mass Drivers", new ResearchItem(new List<string> { "Hyperdrive" },
            //     "Mass Drivers", 600, 6,
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Mass Drivers"),
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Mass Drivers-Hover"),
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Mass Drivers-Pressed"),
            //    new Vector2(900, 1640), ResearchStates.NotResearched));
            //ResearchTree.Add("Magnetic Accelerator Cannons", new ResearchItem(new List<string> { "Mass Driver" },
            //     "Magnetic Accelerator Cannons", 1500, 15,
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Magnetic Accelerator Cannons"),
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Magnetic Accelerator Cannons-Hover"),
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Magnetic Accelerator Cannons-Pressed"),
            //    new Vector2(1300, 1640), ResearchStates.NotResearched));
            //ResearchTree.Add("Nuclear Missiles", new ResearchItem(new List<string> { "Magnetic Accelerator Cannons" },
            //     "Nuclear Missiles", 1700, 17,
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Nuclear Missiles"),
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Nuclear Missiles-Hover"),
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Nuclear Missiles-Pressed"),
            //    new Vector2(1700, 1640), ResearchStates.NotResearched));
            //ResearchTree.Add("Nuclear Mines", new ResearchItem(new List<string> { "Magnetic Accelerator Cannons" },
            //     "Nuclear Mines", 2000, 20,
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Nuclear Mines"),
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Nuclear Mines-Hover"),
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Nuclear Mines-Pressed"),
            //    new Vector2(1700, 1875), ResearchStates.NotResearched));
            //ResearchTree.Add("Organic Hulls", new ResearchItem(new List<string> { "Hyperdrive" },
            //     "researchitem", 700, 7,
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Organic Hulls"),
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Organic Hulls-Hover"),
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Organic Hulls-Pressed"),
            //    new Vector2(895, 925), ResearchStates.NotResearched));
            //ResearchTree.Add("Trinium-Organic Hulls", new ResearchItem(new List<string> { "Organic Hulls" },
            //     "Trinium-Organic Hulls", 1000, 10,
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Trinium-Organic Hulls"),
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Trinium-Organic Hulls-Hover"),
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Trinium-Organic Hulls-Pressed"),
            //    new Vector2(1255, 925), ResearchStates.NotResearched));
            //ResearchTree.Add("Regenerative Hulls", new ResearchItem(new List<string> { "Trinium-Organic Hulls" },
            //     "Regenerative Hulls", 1500, 15,
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Regenerative Hulls"),
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Regenerative Hulls-Hover"),
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Regenerative Hulls-Pressed"),
            //    new Vector2(1615, 925), ResearchStates.NotResearched));
            //ResearchTree.Add("Religious Indoctrination", new ResearchItem(new List<string> { "Hyperdrive" }, 
            //     "Religious Indoctrination", 500, 7,
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Religious Indoctrination"),
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Religious Indoctrination-Hover"),
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Religious Indoctrination-Pressed"),
            //    new Vector2(875, 1000), ResearchStates.NotResearched));
            //ResearchTree.Add("Naquadah Power Source", new ResearchItem(new List<string> { "Hyperdrive" },
            //     "Naquadah Power Source", 700, 7,
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Naquadah Power Source"),
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Naquadah Power Source-Hover"),
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Naquadah Power Source-Pressed"),
            //    new Vector2(900, 1405), ResearchStates.NotResearched));
            //ResearchTree.Add("Intergalactic Hyperdrive", new ResearchItem(new List<string> { "Hyperdrive" },
            //     "Intergalactic Hyperdrive", 3000, 30,
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Intergalactic Hyperdrive"),
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Intergalactic Hyperdrive-Hover"),
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Intergalactic Hyperdrive-Pressed"),
            //    new Vector2(897, 300), ResearchStates.NotResearched));
            //ResearchTree.Add("Alien Diplomacy", new ResearchItem(new List<string> { "Intergalactic Hyperdrive" },
            //     "Alien Diplomacy", 6000, 60,
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Alien Diplomacy"),
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Alien Diplomacy-Hover"),
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Alien Diplomacy-Pressed"),
            //    new Vector2(1290, 690), ResearchStates.NotResearched));
            //ResearchTree.Add("FTL Engines", new ResearchItem(new List<string> { "Intergalactic Hyperdrive" },
            //     "FTL Engines", 1400, 14,
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/FTL Engines"),
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/FTL Engines-Hover"),
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/FTL Engines-Pressed"),
            //    new Vector2(1290, 460), ResearchStates.NotResearched));
            //ResearchTree.Add("Ring Transporters", new ResearchItem(
            //    new List<string> { "Religious Indoctrination", "Naquadah Power Source" }, "Ring Transporters", 1200, 12,
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Ring Transporters"),
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Ring Transporters-Hover"),
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Ring Transporters-Pressed"),
            //    new Vector2(1300, 1405), ResearchStates.NotResearched));
            //ResearchTree.Add("Mobile Ring Transporters", new ResearchItem(new List<string> { "Ring Transporters" },
            //     "Mobile Ring Transporters", 3500, 35,
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Mobile Ring Transporters"),
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Mobile Ring Transporters-Hover"),
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Mobile Ring Transporters-Pressed"),
            //    new Vector2(1700, 1005), ResearchStates.NotResearched));
            //ResearchTree.Add("Plasma Weapons", new ResearchItem(new List<string> { "Intergalactic Hyperdrive" },
            //     "Plasma Weapons", 1700, 17,
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Plasma Weapons"),
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Plasma Weapons-Hover"),
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Plasma Weapons-Pressed"),
            //    new Vector2(1290, 65), ResearchStates.NotResearched));
            //ResearchTree.Add("Linked Weapons Systems", new ResearchItem(                
            //    new List<string> { "Heavy Ion Weapons", "Planetary Production Management", "Plasma Weapons" },  "Linked Weapons Systems", 3600, 40,
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Linked Weapons Systems"),
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Linked Weapons Systems-Hover"),
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Linked Weapons Systems-Pressed"),
            //    new Vector2(3300, -5), ResearchStates.NotResearched));
            //ResearchTree.Add("Pulse Cannons", new ResearchItem(new List<string> { "Plasma Weapons" }, 
            //     "Pulse Cannons", 2000, 20,
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Pulse Cannons"),
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Pulse Cannons-Hover"),
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Pulse Cannons-Pressed"),
            //    new Vector2(1675, 465), ResearchStates.NotResearched));
            //ResearchTree.Add("Energy Projectors", new ResearchItem(
            //    new List<string> { "Pulse Cannons", "Religious Indoctrination" },  "Energy Projectors", 2800, 28,
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Energy Projectors"),
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Energy Projectors-Hover"),
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Energy Projectors-Pressed"),
            //    new Vector2(2025, 925), ResearchStates.NotResearched));
            //ResearchTree.Add("Plasma Torpedoes", new ResearchItem(new List<string> { "Energy Projectors" },
            //     "Plasma Torpedoes", 3000, 30,
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Plasma Torpedoes"),
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Plasma Torpedoes-Hover"),
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Plasma Torpedoes-Pressed"),
            //    new Vector2(2375, 925), ResearchStates.NotResearched));
            //ResearchTree.Add("Cloaking Technology", new ResearchItem(new List<string> { "Plasma Weapons" }, 
            //     "Cloaking Technology", 2200, 25,
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Cloaking Technology"),
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Cloaking Technology-Hover"),
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Cloaking Technology-Pressed"),
            //    new Vector2(1675, 230), ResearchStates.NotResearched));
            //ResearchTree.Add("Drone Weapons", new ResearchItem(new List<string> { "Cloaking Technology" },
            //     "Drone Weapons", 3000, 35,
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Drone Weapons"),
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Drone Weapons-Hover"),
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Drone Weapons-Pressed"),
            //    new Vector2(2050, 230), ResearchStates.NotResearched));
            //ResearchTree.Add("Zero-Point Energy", new ResearchItem(new List<string> { "Cloaking Technology" },
            //     "Zero-Point Energy", 2500, 30,
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Zero-Point Energy"),
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Zero-Point Energy-Hover"),
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Zero-Point Energy-Pressed"),
            //    new Vector2(2050, 465), ResearchStates.NotResearched));
            //ResearchTree.Add("Direct Neural Interfaces", new ResearchItem(new List<string> { "Drone Weapons" }, 
            //     "Direct Neural Interfaces", 4000, 45,
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Direct Neural Interfaces"),
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Direct Neural Interfaces-Hover"),
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Direct Neural Interfaces-Pressed"),
            //    new Vector2(2440, 465), ResearchStates.NotResearched));
            //ResearchTree.Add("Control Chairs", new ResearchItem(new List<string> { "Direct Neural Interfaces" },
            //     "Control Chairs", 5000, 50,
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Control Chairs"),
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Control Chairs-Hover"),
            //    ((Game1)Game).Content.Load<Texture2D>("Graphics/ResearchMenu/Control Chairs-Pressed"),
            //    new Vector2(2825, 915), ResearchStates.NotResearched));
            #endregion

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            MouseState state = Mouse.GetState();
            camera.Update(Keyboard.GetState());

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                ((Game1)Game).Exit();

            foreach (ResearchItem item in ResearchTree.Values)
                item.Update(state, camera);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, null, null, null, null, camera.Translation);

            foreach (ResearchItem item in ResearchTree.Values)
                item.Draw(spriteBatch, font);

            ResearchMap.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
