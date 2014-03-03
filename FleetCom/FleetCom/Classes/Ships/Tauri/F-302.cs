using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FleetCom.Classes.Ships.Tauri
{
    public class F302_Ref : IShip
    {
        public string Name { get { return "F-302"; } }
        public int Cost { get { return 200; } }

        public Texture2D Texture { get; set; }
        public Texture2D IconTexture { get; set; }
        public Texture2D InfoTexture { get; set; }
        public List<ResearchItem> Prerequisites { get; set; }
        public List<Weapon> Weapons { get; set; }
        public IShip[,] Fighters { get; set; }

        public F302_Ref(Texture2D texture, Texture2D icon, Texture2D infoTexture,
            List<ResearchItem> prereqs, List<Weapon> weapons, IShip[,] fighters)
        {
            Texture = texture;
            IconTexture = icon;
            InfoTexture = infoTexture;
            Prerequisites = prereqs;
            Weapons = weapons;
            Fighters = fighters;
        }
    }
}
