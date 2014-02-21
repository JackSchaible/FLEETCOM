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

            result.Add("X-302", new Ship(new List<ResearchItem> { game.ResearchTree["Space Flight"] }));
            result.Add("BC-303", new Ship(new List<ResearchItem> { game.ResearchTree["Hyperdrive"] } ));
            result.Add("BC-304", new Ship(new List<ResearchItem> { game.ResearchTree["Intergalactic Hyperdrive"] }));
            result.Add("BC-304 Refit", new Ship(new List<ResearchItem> { game.ResearchTree["Alien Diplomacy"], game.ResearchTree["Cloaking Technology"] }));
            result.Add("Bilskirnir", new Ship(new List<ResearchItem> { game.ResearchTree["Ion Weapons"] }));
            result.Add("O'Neill", new Ship(new List<ResearchItem> { game.ResearchTree["Naquadah-Trinium-Carbon Alloys"] }));
            result.Add("Death Glider", new Ship(new List<ResearchItem> { game.ResearchTree["Naquadah Power Source"] }));
            result.Add("Ha'tak", new Ship(new List<ResearchItem> { game.ResearchTree["Religious Indoctrination"], game.ResearchTree["Ring Transporters"] }));
            result.Add("Dart", new Ship(new List<ResearchItem> { game.ResearchTree["Organic Hulls"] }));
            result.Add("Wraith Cruiser", new Ship(new List<ResearchItem> { game.ResearchTree["Trinium-Organic Hulls"] }));
            result.Add("Hive Ship", new Ship(new List<ResearchItem> { game.ResearchTree["Regenerative Hulls"] }));
            result.Add("Ori Fighter", new Ship(new List<ResearchItem> { game.ResearchTree["Religious Indoctrination"] }));
            result.Add("Ori Warship", new Ship(new List<ResearchItem> { game.ResearchTree["Religious Indoctrination"], game.ResearchTree["Control Chairs"] }));
            result.Add("Seed Ship", new Ship(new List<ResearchItem> { game.ResearchTree["FTL Engines"] }));
            result.Add("Gateship", new Ship(new List<ResearchItem> { game.ResearchTree["Drone Weapons"] }));
            result.Add("Aurora", new Ship(new List<ResearchItem> { game.ResearchTree["Drone Weapons"], game.ResearchTree["Zero-Point Energy"] }));
            result.Add("TIE Fighter", new Ship(new List<ResearchItem> { game.ResearchTree["Ion Engines"] }));
            result.Add("Acclamator", new Ship(new List<ResearchItem> { game.ResearchTree["Turbolasers"] }));
            result.Add("Victory", new Ship(new List<ResearchItem> { game.ResearchTree["Ion Engines"], game.ResearchTree["Tractor Beams"] }));
            result.Add("Imperial", new Ship(new List<ResearchItem> { game.ResearchTree["Heavy Turbolasers"], game.ResearchTree["Planetary Production Management"] }));
            result.Add("X-Wing", new Ship(new List<ResearchItem> { game.ResearchTree["Proton Weapons"] }));
            result.Add("Nebulon-B", new Ship(new List<ResearchItem> { game.ResearchTree["Turbolasers"] }));
            result.Add("Liberty", new Ship(new List<ResearchItem> { game.ResearchTree["Tractor Beams"], game.ResearchTree["Heavy Turbolasers"] }));
            result.Add("Home One", new Ship(new List<ResearchItem> { game.ResearchTree["SEAL Shielding"] }));
            result.Add("Sith Fighter", new Ship(new List<ResearchItem> { game.ResearchTree["Ion Engines"] }));
            result.Add("Leviathan", new Ship(new List<ResearchItem> { game.ResearchTree["Turbolasers"], game.ResearchTree["Tractor Beams"] }));
            result.Add("Aurek", new Ship(new List<ResearchItem> { game.ResearchTree["Proton Weapons"] }));
            result.Add("Hammerhead", new Ship(new List<ResearchItem> { game.ResearchTree["Tractor Beams"], game.ResearchTree["Proton Weapons"] }));
            result.Add("Vulture Droid", new Ship(new List<ResearchItem> { game.ResearchTree["Droid Control Systems"] }));
            result.Add("Lucrehulk", new Ship(new List<ResearchItem> { game.ResearchTree["Droid Control Systems"], game.ResearchTree["Heavy Turbolasers"] }));
            result.Add("Providence", new Ship(new List<ResearchItem> { game.ResearchTree["Proton Weapons"], game.ResearchTree["Heavy Turbolasers"] }));
            result.Add("Crusader", new Ship(new List<ResearchItem> { game.ResearchTree["Hyperdrive"] }));
            result.Add("Keldabe", new Ship(new List<ResearchItem> { game.ResearchTree["Turbolasers"], game.ResearchTree["Heavy Ion Weapons"] }));
            result.Add("Vengeance", new Ship(new List<ResearchItem> { game.ResearchTree["Mass Drivers"] }));
            result.Add("Aggressor", new Ship(new List<ResearchItem> { game.ResearchTree["Linked Weapons Systems"] }));
            result.Add("Longsword", new Ship(new List<ResearchItem> { game.ResearchTree["Mass Drivers"] }));
            result.Add("Charon", new Ship(new List<ResearchItem> { game.ResearchTree["Magnetic Accelerator Cannons"] }));
            result.Add("Halcyon", new Ship(new List<ResearchItem> { game.ResearchTree["Nuclear Missiles"] }));
            result.Add("Phoenix", new Ship(new List<ResearchItem> { game.ResearchTree["Nuclear Missiles"], game.ResearchTree["Nuclear Mines"] }));
            result.Add("Seraph", new Ship(new List<ResearchItem> { game.ResearchTree["Religious Indoctrination"], game.ResearchTree["Plasma Weapons"] }));
            result.Add("Heavy Corvette", new Ship(new List<ResearchItem> { game.ResearchTree["Religious Indoctrination"], game.ResearchTree["Plasma Weapons"] }));
            result.Add("CCS", new Ship(new List<ResearchItem> { game.ResearchTree["Energy Projectors"] }));
            result.Add("CAS", new Ship(new List<ResearchItem> { game.ResearchTree["Plasma Torpedoes"] }));

            return result;
        }
        
        public static Dictionary<string, ResearchItem> InitializeResearchTree(Game1 game)
        {
            Dictionary<string, ResearchItem> result = new Dictionary<string, ResearchItem>();

            result.Add("Space Flight", new ResearchItem(new List<string>(), true, "Space Flight", 0, 0));
            result.Add("Droid Control Systems", new ResearchItem(new List<string> { "Space Flight" }, false, "Droid Control Systems", 0, 0));
            result.Add("Hyperdrive", new ResearchItem(new List<string> { "Space Flight" }, false, "Hyperdrive", 0, 0));
            result.Add("Tractor Beams", new ResearchItem(new List<string> { "Space Flight" }, false, "Tractor Beams", 0, 0));
            result.Add("Planetary Production Management", new ResearchItem(new List<string> { "Tractor Beam" }, false, "Planetary Production Management", 0, 0));
            result.Add("Ion Weapons", new ResearchItem(new List<string> { "Hyperdrive" }, false, "Ion Weapons", 0, 0));
            result.Add("Ion Engines", new ResearchItem(new List<string> { "Ion Weapons" }, false, "Ion Engines", 0, 0));
            result.Add("Turbolasers", new ResearchItem(new List<string> { "Ion Engines" }, false, "Turbolasers", 0, 0));
            result.Add("Heavy Turbolasers", new ResearchItem(new List<string> { "Turbolasers" }, false, "Heavy Turbolasers", 0, 0));
            result.Add("SEAL Shielding", new ResearchItem(new List<string> { "Heavy Turbolasers" }, false, "SEAL Shielding", 0, 0));
            result.Add("Proton Weapons", new ResearchItem(new List<string> { "Ion Weapons" }, false, "Proton Weapons", 0, 0));
            result.Add("Heavy Ion Weapons", new ResearchItem(new List<string> { "Ion Weapons" }, false, "Heavy Ion Weapons", 0, 0));
            result.Add("Naquadah-Trinium-Carbon Alloys", new ResearchItem(new List<string> { "Ion Weapons" }, false, "Naquadah-Trinium-Carbon Alloys", 0, 0));
            result.Add("Mass Drivers", new ResearchItem(new List<string> { "Hyperdrive" }, false, "Mass Drivers", 0, 0));
            result.Add("Magnetic Accelerator Cannons", new ResearchItem(new List<string> { "Mass Driver" }, false, "Magnetic Accelerator Cannons", 0, 0));
            result.Add("Nuclear Missiles", new ResearchItem(new List<string> { "Magnetic Accelerator Cannons" }, false, "Nuclear Missiles", 0, 0));
            result.Add("Nuclear Mines", new ResearchItem(new List<string> { "Magnetic Accelerator Cannons" }, false, "Nuclear Mines", 0, 0));
            result.Add("Organic Hulls", new ResearchItem(new List<string> { "Hyperdrive" }, false, "researchitem", 0, 0));
            result.Add("Trinium-Organic Hulls", new ResearchItem(new List<string> { "Organic Hulls" }, false, "Trinium-Organic Hulls", 0, 0));
            result.Add("Regenerative Hulls", new ResearchItem(new List<string> { "Trinium-Organic Hulls" }, false, "Regenerative Hulls", 0, 0));
            result.Add("Religious Indoctrination", new ResearchItem(new List<string> { "Hyperdrive" }, false, "Religious Indoctrination", 0, 0));
            result.Add("Naquadah Power Source", new ResearchItem(new List<string> { "Hyperdrive" }, false, "Naquadah Power Source", 0, 0));
            result.Add("Intergalactic Hyperdrive", new ResearchItem(new List<string> { "Hyperdrive" }, false, "Intergalactic Hyperdrive", 0, 0));
            result.Add("Alien Diplomacy", new ResearchItem(new List<string> { "Intergalactic Hyperdrive" }, false, "Alien Diplomacy", 0, 0));
            result.Add("FTL Engines", new ResearchItem(new List<string> { "Intergalactic Hyperdrive" }, false, "FTL Engines", 0, 0));
            result.Add("Ring Transporters", new ResearchItem(new List<string> { "Religious Indoctrination", "Naquadah Power Source" }, false, "Ring Transporters", 0, 0));
            result.Add("Mobile Ring Transporters", new ResearchItem(new List<string> { "Ring Transporters" }, false, "Mobile Ring Transporters", 0, 0));
            result.Add("Plasma Weapons", new ResearchItem(new List<string> { "Intergalactic Hyperdrive" }, false, "Plasma Weapons", 0, 0));
            result.Add("Linked Weapons Systems", new ResearchItem(new List<string> { "Heavy Ion Weapons", "Planetary Production Management", "Plasma Weapons" }, false, "Linked Weapons Systems", 0, 0));
            result.Add("Pulse Cannons", new ResearchItem(new List<string> { "Plasma Weapons" }, false, "Pulse Cannons", 0, 0));
            result.Add("Energy Projectors", new ResearchItem(new List<string> { "Pulse Cannons", "Religious Indoctrination" }, false, "Energy Projectors", 0, 0));
            result.Add("Plasma Torpedoes", new ResearchItem(new List<string> { "Energy Projectors" }, false, "Plasma Torpedoes", 0, 0));
            result.Add("Cloaking Technology", new ResearchItem(new List<string> { "Plasma Weapons" }, false, "Cloaking Technology", 0, 0));
            result.Add("Drone Weapons", new ResearchItem(new List<string> { "Cloaking Technology" }, false, "Drone Weapons", 0, 0));
            result.Add("Zero-Point Energy", new ResearchItem(new List<string> { "Cloaking Technology" }, false, "Zero-Point Energy", 0, 0));
            result.Add("Direct Neural Interfaces", new ResearchItem(new List<string> { "Drone Weapons" }, false, "Direct Neural Interfaces", 0, 0));
            result.Add("Control Chairs", new ResearchItem(new List<string> { "Direct Neural Interfaces" }, false, "Control Chairs", 0, 0));

            return result;
        }
    }

    public class ResearchItem
    {
        public List<string> Prerequisites { get; set; }
        public bool Researched { get; set; }
        public string Name { get; set; }
        public int Cost { get; set; }
        public int TurnsToResearch { get; set; }

        public ResearchItem(List<string> prereqs, bool researched, 
            string name, int cost, int turnsToResearch)
        {
            Prerequisites = prereqs;
            Researched = researched;
            Name = name;
            Cost = cost;
            TurnsToResearch = turnsToResearch;
        }
    }
}
