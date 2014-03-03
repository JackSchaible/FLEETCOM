using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FleetCom.Classes.Ships
{
    public class Weapon
    {
        public int Ammo { get; set; }
        public int Used { get; set; }
        public string Name { get; set; }

        public Texture2D AmmoTexture { get; set; }
        public Vector2 PositionOffset { get; set; }

        public Weapon(int ammo, string name, Texture2D ammoTexture,
            Vector2 offset)
        {
            Ammo = ammo;
            Used = 0;
            Name = name;
            PositionOffset = offset;
            AmmoTexture = ammoTexture;
        }
    }
}
