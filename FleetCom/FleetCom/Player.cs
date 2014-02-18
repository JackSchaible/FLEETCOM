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
        Finished
    }

    public class Player
    {
        public Characters Character { get; set; }
        public Galaxy Map { get; set; }
        public TutortialSteps TutorialStep { get; set; }

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
                Player player = LoadCharacter(character.ToString());

                Character = player.Character;
                Map = player.Map;
                TutorialStep = player.TutorialStep;
            }
            else
            {
                Character = character;
                TutorialStep = TutortialSteps.GalaxyMap1;
                Map = new Galaxy(systemNames, starClusterNormalTexture,
                    starClusterUnderAttackTexture, starClusterUnownedTexture, 
                    clusterStatusTexture, MH45, MH75);

                SaveCharacter();
            }
        }

        public static Player LoadCharacter(string name)
        {
            Player result = null;

            string filename = "Players/" + name + ".bin";

            Stream stream = File.Open(filename, FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();
            result = (Player)bf.Deserialize(stream);
            stream.Close();

            return result;
        }

        public void SaveCharacter()
        {
            if (!Directory.Exists("Players"))
                Directory.CreateDirectory("Players");

            XDocument xd = new XDocument();
            XElement root = new XElement("Player",
                new XAttribute("Character", Character)
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
