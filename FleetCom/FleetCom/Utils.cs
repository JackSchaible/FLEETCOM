using FleetCom.Graphics.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FleetCom
{
    static class Utils
    {
        public static Dictionary<string, Ship> InitializeShipsList(Game1 game)
        {
            Dictionary<string, Ship> result = new Dictionary<string, Ship>();

            //result.Add("X-302", new Ship(new List<ResearchItem> { game.ResearchMenu.ResearchTree["Space Flight"] }));
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

            return result;
        }
    }
}
