using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace FleetCom
{
    public enum Characters
    {
        Aggressive,
        Defensive,
        Fast,
        Tech
    }
    public enum TutortialSteps
    {
        GalaxyMap1,
        GalaxyMap2,
        GalaxyMap3,
        GalaxyMap4,
        Finished
    }

    public class Player
    {
        public Characters Character { get; set; }
        public Galaxy Map { get; set; }
        public string[] Fleet { get; set; }
        public int XP { get; set; }
        public int GenericGalacticCredits { get; set; }

        public static Dictionary<string, string> Ranks = new Dictionary<string, string>()
        {
            { "ENS", "Ensign" },
            { "LTJG", "Lieutenant Jr. Grade" },
            { "LT" , "Lieutenant" },
            { "LTCDR", "Lieutenant Commander" },
            { "CDR",  "Commander" },
            { "CPT",  "Captain" },
            { "RADM",  "Rear Admiral" },
            { "VADM",  "Vice Admiral" },
            { "ADM",  "Admiral" },
            { "FADM", "Fleet Admiral"}
        };

        public string Rank { get; set; }

        public TutortialSteps TutorialStep { get; set; }
        public bool ResearchTutorial { get; set; }
        public bool FleetTutorial { get; set; }

        private string FileName
        {
            get
            {
                return "Players/" + Character.ToString() + ".xml";
            }
        }

        public Player(Characters character, bool newPlayer, List<string> systemNames, 
            Texture2D starClusterNormalTexture, Texture2D starClusterUnderAttackTexture, 
            Texture2D starClusterUnownedTexture, Texture2D clusterStatusTexture,
            SpriteFont MH45, SpriteFont MH75, Game1 game)
        {
            if (!newPlayer)
            {
                LoadCharacter(character.ToString(), starClusterNormalTexture,
                    starClusterUnderAttackTexture, starClusterUnownedTexture,
                    clusterStatusTexture, MH45, MH75, game);
            }
            else
            {
                Character = character;
                TutorialStep = TutortialSteps.GalaxyMap1;
                Map = new Galaxy(systemNames, starClusterNormalTexture,
                    starClusterUnderAttackTexture, starClusterUnownedTexture, 
                    clusterStatusTexture, MH45, MH75);
                Rank = "ENS";
                ResearchTutorial = false;
                Fleet = new string[10];
                Fleet[0] = "F-302";
                XP = 0;
                GenericGalacticCredits = 2000;

                SaveCharacter(game);
            }
        }

        public void LoadCharacter(string name, Texture2D starClusterNormalTexture, Texture2D starClusterUnderAttackTexture, 
            Texture2D starClusterUnownedTexture, Texture2D clusterStatusTexture,
            SpriteFont MH45, SpriteFont MH75, Game1 game)
        {
            string filename = "Players/" + name + ".xml";

            XDocument doc = XDocument.Load(filename);

            //Get name & character type
            switch (name)
            { 
                case "Aggressive":
                    Character = Characters.Aggressive;
                    break;

                case "Defensive":
                    Character = Characters.Defensive;
                    break;

                case "Fast":
                    Character = Characters.Fast;
                    break;

                case "Tech":
                    Character = Characters.Tech;
                    break;
            }

            //Get Tutorial Progress
            switch (doc.Element("Player").Attribute("TutorialStep").Value)
            {
                case "GalaxyMap1":
                    TutorialStep = TutortialSteps.GalaxyMap1;
                    break;

                case "GalaxyMap2":
                    TutorialStep = TutortialSteps.GalaxyMap2;
                    break;

                case "GalaxyMap3":
                    TutorialStep = TutortialSteps.GalaxyMap3;
                    break;

                case "Finished":
                    TutorialStep = TutortialSteps.Finished;
                    break;
            }

            if (doc.Elements("Player").Elements("ResearchTutorial") != null)
                ResearchTutorial = true;

            //Load rank
            Rank = doc.Element("Player").Attribute("Rank").Value;
            XP = int.Parse(doc.Element("Player").Attribute("XP").Value);
            GenericGalacticCredits = int.Parse(doc.Element("Player").Attribute("GGC").Value);

            //Load map
            foreach (XElement item in doc.Elements("StarCluster"))
            {
                StarClusterStates state = StarClusterStates.Unowned;

                switch(item.Attribute("State").Value)
                {
                    case "Unowned":
                        state = StarClusterStates.Unowned;
                        break;

                    case "UnderAttack":
                        state = StarClusterStates.UnderAttack;
                        break;

                    case "Owned":
                        state = StarClusterStates.Owned;
                        break;
                }

                StarCluster cluster = new StarCluster(
                    new Vector2(int.Parse(item.Attribute("X").Value), int.Parse(item.Attribute("Y").Value)),
                    item.Attribute("Name").Value, starClusterUnownedTexture, starClusterUnderAttackTexture, starClusterNormalTexture,
                    clusterStatusTexture, MH45, MH75, state);

                foreach (XElement system in item.Elements("StarSystem"))
                {
                    //add systems to the cluster
                }

                Map.StarClusters.Add(cluster);
            }

            //Load research
            foreach (XElement item in doc.Element("Research").Elements().ToList<XElement>())
            {
                //Determine if there's a currently researching item
                if (item.Attribute("TimeLeft") != null)
                {
                    game.ResearchMenu.CurrentlyResearching = item.Attributes("Key").FirstOrDefault<XAttribute>().Value;
                    game.ResearchMenu.ResearchCounter = int.Parse(item.Attributes("TimeLeft").FirstOrDefault<XAttribute>().Value);
                }
                else
                {
                    game.ResearchMenu.ResearchTree[item.Attribute("Key").Value].Researched = true;
                    game.ResearchMenu.ResearchTree[item.Attribute("Key").Value].ResearchState = ResearchStates.Researched;
                }
            }

            //Load fleet
            Fleet = new string[10];
            List<XElement> shipElements = doc.Element("Fleet").Elements().ToList<XElement>();
            for (int i = 0; i < shipElements.Count(); i++)
                Fleet[i] = shipElements[i].Value;
        }

        public void SaveCharacter(Game1 game)
        {
            if (!Directory.Exists("Players"))
                Directory.CreateDirectory("Players");

            XDocument xd = new XDocument();
            XElement root = new XElement("Player",
                new XAttribute("Character", Character),
                new XAttribute("TutorialStep", TutorialStep),
                new XAttribute("Rank", Rank),
                new XAttribute("XP", XP),
                new XAttribute("GGC", GenericGalacticCredits)
                );

            if (ResearchTutorial)
                root.Add(new XAttribute("ResearchTutorial", "True"));

            //Create an element for each Star Cluster, and add each system
            foreach (StarCluster item in Map.StarClusters)
            {
                XElement clusterElement = new XElement("StarCluster",
                        new XAttribute("Name", item.Name),
                        new XAttribute("X", item.Position.X),
                        new XAttribute("Y", item.Position.Y),
                        new XAttribute("State", item.State)
                    );


                foreach(StarSystem item2 in item.StarSystems)
                {
                    XElement systemElement = new XElement("StarSystem",
                            new XAttribute("Name", item.Name),
                            new XAttribute("X", item.Position.X),
                            new XAttribute("Y", item.Position.Y),
                            new XAttribute("State", item.State)
                        );

                    clusterElement.Add(systemElement);
                }

                root.Add(clusterElement);
            }

            XElement researchElement = new XElement("Research");

            if (((Game1)game).ResearchMenu.CurrentlyResearching != null)
                researchElement.Add(new XElement("ResearchElement",
                    new XAttribute("Key", ((Game1)game).ResearchMenu.CurrentlyResearching),
                    new XAttribute("TimeLeft", ((Game1)game).ResearchMenu.ResearchCounter)));

            foreach (ResearchItem item in (game.ResearchMenu.ResearchTree.Where(x => x.Value.ResearchState == ResearchStates.Researched).Select(x => x.Value)))
                researchElement.Add(new XElement("ResearchElement",
                    new XAttribute("Key", item.Name)
                    ));

            root.Add(researchElement);

            XElement fleetElement = new XElement("Fleet");
            foreach (string item in Fleet)
                if (item != null)
                    fleetElement.Add(new XElement(item));
            root.Add(fleetElement);

            xd.Add(root);

            Stream stream = File.Create(FileName);
            xd.Save(stream);
            stream.Close();
        }

        public bool HasAchievedRank(string RankKey)
        {
            bool result = true;

            string[] dictionary = Ranks.Select(x => x.Key).ToArray<string>();
            int pos = Array.IndexOf(dictionary, RankKey);
            int current = Array.IndexOf(dictionary, Rank);

            if (pos > current)
                result = false;

            return result;
        }
    }
}
