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
        Finished
    }

    public class Player
    {
        public Characters Character { get; set; }
        public Galaxy Map { get; set; }
        public TutortialSteps TutorialStep { get; set; }

        public static Dictionary<string, string> Ranks = new Dictionary<string, string>()
        {
            { "ENS", "Ensign" },
            { "LTJG", "Lieutenant Jr. Grade" },
            { "LT" , "Lieutenant" },
            { "LTCDR", "Lieutenant Commander" },
            { "CDR",  "Commander" },
            { "CPT",  "Captain" },
            { "RDML",  "Rear Admiral (Lower Half)" },
            { "RADM",  "Rear Admiral" },
            { "VAMD",  "Vice Admiral" },
            { "ADM",  "Admiral" },
            { "FADM",  "Fleet Admiral" },
            { "AN", "Admiral of the Navy" }
        };

        public string Rank { get; set; }

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
            SpriteFont MH45, SpriteFont MH75)
        {
            if (!newPlayer)
            {
                LoadCharacter(character.ToString(), starClusterNormalTexture,
                    starClusterUnderAttackTexture, starClusterUnownedTexture,
                    clusterStatusTexture, MH45, MH75);
            }
            else
            {
                Character = character;
                TutorialStep = TutortialSteps.GalaxyMap1;
                Map = new Galaxy(systemNames, starClusterNormalTexture,
                    starClusterUnderAttackTexture, starClusterUnownedTexture, 
                    clusterStatusTexture, MH45, MH75);
                Rank = "ENS";

                SaveCharacter();
            }
        }

        public void LoadCharacter(string name, Texture2D starClusterNormalTexture, Texture2D starClusterUnderAttackTexture, 
            Texture2D starClusterUnownedTexture, Texture2D clusterStatusTexture,
            SpriteFont MH45, SpriteFont MH75)
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

            //Load rank
            Rank = doc.Element("Player").Attribute("Rank").Value;

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
        }

        public void SaveCharacter()
        {
            if (!Directory.Exists("Players"))
                Directory.CreateDirectory("Players");

            XDocument xd = new XDocument();
            XElement root = new XElement("Player",
                new XAttribute("Character", Character),
                new XAttribute("TutorialStep", TutorialStep),
                new XAttribute("Rank", Rank)
                );


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

            xd.Add(root);

            Stream stream = File.Create(FileName);
            xd.Save(stream);
            stream.Close();
        }
    }
}
