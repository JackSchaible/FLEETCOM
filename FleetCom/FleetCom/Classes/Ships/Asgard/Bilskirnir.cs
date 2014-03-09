using FleetCom.Classes.Ships.Weapons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FleetCom.Classes.Ships.Asgard
{
    public class Bilskirnir_Ref : IShip
    {
        public string Name { get { return "Bilskirnir"; } }
        public int Cost { get { return 4000; } }
        public int HP { get; private set; }
        public int Shields { get; private set; }
        public int ShieldRechargeDelay { get; private set; }
        public float ShieldRechargeRate { get; private set; }
        public float Speed { get; private set; }
        public float TurnRate { get; private set; }
        public ShipSizes ShipSize { get { return ShipSizes.Medium; } }

        public Texture2D Texture { get; set; }
        public Texture2D IconTexture { get; set; }
        public Texture2D InfoTexture { get; set; }
        public List<ResearchItem> Prerequisites { get; set; }
        public List<WeaponGroup> WeaponGroups { get; set; }

        public SpawnModes SpawnMode { get; set; }
        public int SpawnRate { get; set; }
        public Vector2[] SpawnPoints { get; set; }
        public Dictionary<string, int> Fighters { get; set; }

        public Bilskirnir_Ref(Texture2D texture, Texture2D icon,
            Texture2D infoTexture,
            Dictionary<string, ResearchItem> researchTree,
            Dictionary<string, IWeapon> weapons)
        {
            Texture = texture;
            IconTexture = icon;
            InfoTexture = infoTexture;
            
            Prerequisites = new List<ResearchItem>()
            {
                researchTree["Ion Weapons"]
            };

            Shields = 7500;
            HP = 3500;
            Speed = 5;
            TurnRate = 2;

            WeaponGroups = new List<WeaponGroup>()
            {
                new WeaponGroup(new List<Weapon>()
                {
                    new Weapon(weapons["Ion Gun"], new Vector2(470, 25),
                        -45, 45),
                    new Weapon(weapons["Ion Gun"], new Vector2(505, 25),
                        -45, 45),
                    new Weapon(weapons["Ion Gun"], new Vector2(215, 300),
                        -45, 45),
                    new Weapon(weapons["Ion Gun"], new Vector2(760, 300),
                        -45, 45),
                    new Weapon(weapons["Ion Gun"], new Vector2(168, 900),
                        0, 0),
                    new Weapon(weapons["Ion Gun"], new Vector2(168, 900),
                        0, 0),
                    new Weapon(weapons["Ion Gun"], new Vector2(168, 900),
                        0, 0),
                    new Weapon(weapons["Ion Gun"], new Vector2(807, 900),
                        0, 0),
                    new Weapon(weapons["Ion Gun"], new Vector2(807, 900),
                        0, 0),
                    new Weapon(weapons["Ion Gun"], new Vector2(807, 900),
                        0, 0)
                }, "Forward Ion Guns")
            };

            SpawnMode = SpawnModes.AllAtOnce;
            SpawnRate = -1;
            Fighters = null;
            SpawnPoints = null;
        }
    }
}
