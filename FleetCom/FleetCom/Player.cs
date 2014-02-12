using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace FleetCom
{
    [Serializable]
    public enum Characters
    {
        Aggressive,
        Defensive,
        Fast,
        Tech
    }
    [Serializable]
    public enum TutortialSteps
    {
        GalaxyMap
    }

    [Serializable()]
    public class Player
    {
        public Characters Character { get; set; }
        public Galaxy Map { get; set; }
        public TutortialSteps TutorialStep { get; set; }

        private string FileName
        {
            get
            {
                return "Players/" + Character.ToString() + ".bin";
            }
        }

        public Player(Characters character, bool newPlayer, List<string> systemNames, 
            Texture2D starClusterNormalTexture, Texture2D starClusterHoverTexture, 
            Texture2D starClusterDownTexture)
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
                TutorialStep = TutortialSteps.GalaxyMap;
                Map = new Galaxy(systemNames, starClusterNormalTexture, 
                    starClusterHoverTexture, starClusterDownTexture);

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

            Stream stream = File.Create(FileName);
            BinaryFormatter bf = new BinaryFormatter();

            bf.Serialize(stream, this);
            stream.Close();
        }
    }
}
