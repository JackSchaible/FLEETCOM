using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace FleetCom
{
    public enum Characters
    {
        Aggressive,
        Defensive,
        Fast,
        Tech
    }
    public delegate void CharacterExist();

    [Serializable()]
    public class Player
    {
        public Characters Character { get; set; }

        public event CharacterExist CharacterExists;

        private string FileName
        {
            get
            {
                return "Players/" + Character.ToString() + ".bin";
            }
        }
        /// <summary>
        /// Load a previously saved character
        /// </summary>
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

                if (File.Exists(FileName))
                    CharacterExists();
                else
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
            Stream stream = File.Create(FileName);
            BinaryFormatter bf = new BinaryFormatter();

            bf.Serialize(stream, this);
            stream.Close();
        }
    }
}
