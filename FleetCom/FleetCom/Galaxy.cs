using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FleetCom
{
    [Serializable]
    public class Galaxy
    {
        public List<StarCluster> StarClusters { get; set; }

        public Galaxy(List<string> systemNames, Texture2D starClusterNormalTexture,
            Texture2D starClusterUnderAttackTexture, Texture2D starClusterUnownedTexture)
        {
            //Available slots for star clusters to be placed
            bool[,] slots = new bool[9, 5];

            //Create and initialize a dictionary to hold the star system names
            Dictionary<string, bool> names = new Dictionary<string, bool>();
            foreach (string item in systemNames)
                names.Add(item, false);

            //Initialize all to false
            for (int i = 0; i < 9; i++)
                for (int i2 = 0; i2 < 5; i2++)
                    slots[i, i2] = false;

            //Create a random number generator citing the day of the month as the seed
            Random rnd = new Random(DateTime.Now.Day);

            //Create the star clusters
            for (int i = 0; i < 5; i++)
            {
                //Get the position of the next star system
                int[] pos = GetPosition(rnd, slots);

                StarClusters.Add(new StarCluster(new Vector2(pos[0] * 200, pos[1] * 200), 
                    RandomValues(names).Take(1).ToString(), 
                    starClusterNormalTexture, starClusterUnderAttackTexture,
                    starClusterUnownedTexture, StarClusterStates.Unowned));
            }
        }

        private int[] GetPosition(Random rnd, bool[,] slots)
        {
            int[] result = new int[2];

            int x = rnd.Next(0, 10);
            int y = rnd.Next(0, 6);

            if (slots[x, y])
                result = GetPosition(rnd, slots);

            slots[result[0], result[1]] = true;

            return result;
        }

        public IEnumerable<TValue> RandomValues<TKey, TValue>(IDictionary<TKey, TValue> dict)
        {
            Random rand = new Random();
            List<TValue> values = Enumerable.ToList(dict.Values);
            int size = dict.Count;
            while (true)
            {
                yield return values[rand.Next(size)];
            }
        }
    }
}
