using FleetCom.Classes.Ships.Weapons;
using Microsoft.Xna.Framework;
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
        public int HP { get { return 10; } }
        public int Shields { get { return 0; } }
        public int ShieldRechargeDelay { get { return 0; } }
        public float ShieldRechargeRate { get { return 0; } }
        public ShipSizes ShipSize { get { return ShipSizes.Diminutive; } }

        public Texture2D Texture { get; set; }
        public Texture2D IconTexture { get; set; }
        public Texture2D InfoTexture { get; set; }
        public List<ResearchItem> Prerequisites { get; set; }
        public List<WeaponGroup> WeaponGroups { get; set; }

        public SpawnModes SpawnMode { get; set; }
        public int SpawnRate { get; set; }
        public Vector2[] SpawnPoints { get; set; }
        public Dictionary<string, int> Fighters { get; set; }


        public F302_Ref(Texture2D texture, Texture2D icon, 
            Texture2D infoTexture, 
            Dictionary<string, ResearchItem> researchTree,
            Dictionary<string, IWeapon> weapons)
        {
            Texture = texture;
            IconTexture = icon;
            InfoTexture = infoTexture;
            Prerequisites = new List<ResearchItem>() { researchTree["Space Flight"] };
            WeaponGroups = new List<WeaponGroup>()
            {
                new WeaponGroup(new List<Weapon>()
                {
                    new Weapon(weapons["Railgun"], new Vector2(180, 8),
                        0, 0),
                    new Weapon(weapons["Railgun"], new Vector2(200, 8),
                        0, 0)
                }, "Railguns"),
                new WeaponGroup(new List<Weapon>()
                {
                    new Weapon(weapons["Missile"], new Vector2(75, 50),
                        -90, 90),
                    new Weapon(weapons["Missile"], new Vector2(307, 50),
                        -90, 90)
                }, "Missiles", 30)
            };

            Fighters = null;
            SpawnMode = SpawnModes.AllAtOnce;
            SpawnPoints = null;
            SpawnRate = -1;
        }
    }
}
