using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FleetCom.Classes.Ships
{
    public interface IShip
    {
        //Basic info
        string Name { get; }
        int Cost { get; }

        //Graphics
        Texture2D Texture { get; set; }
        Texture2D IconTexture { get; set; }
        Texture2D InfoTexture { get; set; }

        //Other classes
        List<ResearchItem> Prerequisites { get; set; }
        List<Weapon> Weapons { get; set; }
        IShip[,] Fighters { get; set; }
    }
}
