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

    [Serializable()]
    public class Player
    {
        public Characters Character { get; set; }
        public Galaxy Map { get; set; }

        private string FileName
        {
            get
            {
                return "Players/" + Character.ToString() + ".bin";
            }
        }

        public Player(Characters character, bool newPlayer)
        {
            if (!newPlayer)
            {
                Player player = LoadCharacter(character.ToString());

                Character = player.Character;
            }
            else
            {
                Character = character;
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
