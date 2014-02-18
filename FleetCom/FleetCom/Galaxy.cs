using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FleetCom
{
    public class Galaxy
    {
        public List<StarCluster> StarClusters { get; set; }

        public Galaxy(List<string> systemNames, Texture2D starClusterNormalTexture,
            Texture2D starClusterUnderAttackTexture, Texture2D starClusterUnownedTexture,
            Texture2D clusterStatusWindow, SpriteFont MH45, SpriteFont MH75)
        {
            //Available slots for star clusters to be placed
            bool[,] slots = new bool[9, 5];

            //Create and initialize a dictionary to hold the star system names
            Dictionary<string, bool> names = new Dictionary<string, bool>();
            foreach (string item in systemNames)
                names.Add(item, false);

            //Initialize all to false, except for row 0
            for (int i = 0; i < 9; i++)
                for (int i2 = 0; i2 < 5; i2++)
                    slots[i, i2] = false;

            for (int i = 0; i < 9; i++)
                slots[i, 0] = true;

            Random rnd = new Random();

            //Create the star clusters
            StarClusters = new List<StarCluster>();

            for (int i = 0; i < 5; i++)
            {
                //Get the position of the next star system
                int[] pos = GetPosition(rnd, slots);
                string name = GetName(names);
                names[name] = true;

                StarClusters.Add(new StarCluster(new Vector2(pos[0] * 200, pos[1] * 200), 
                    name, starClusterNormalTexture, starClusterUnderAttackTexture,
                    starClusterUnownedTexture, clusterStatusWindow, MH45, MH75, StarClusterStates.Unowned));
            }

            int[] position = GetPosition(rnd, slots);
            StarClusters.Add(new StarCluster(new Vector2(position[0] * 200, position[1] * 200),
                "Local Cluster", starClusterUnownedTexture, starClusterUnderAttackTexture,
                starClusterNormalTexture, clusterStatusWindow, MH45, MH75, StarClusterStates.UnderAttack));
        }

        private int[] GetPosition(Random rnd, bool[,] slots)
        {
            int[] result = new int[2];

            int x = rnd.Next(0, 9);
            int y = rnd.Next(0, 4);

            if (slots[x, y])
                result = GetPosition(rnd, slots);
            else
                result = new int[] { x, y };

            slots[result[0], result[1]] = true;

            return result;
        }

        public string GetName(Dictionary<string, bool> dict)
        {
            string result = "";

            Random rand = new Random();
            List<string> names = dict.Where(x => x.Value == false).Select(x => x.Key).ToList<string>();
            result = names[rand.Next(names.Count)];

            return result;
        }
    }
}
