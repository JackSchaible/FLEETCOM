using FleetCom.Classes.Ships.Weapons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FleetCom.Classes.Ships.Tauri
{
    public class BC303_Ref : IShip
    {
        public string Name { get { return "BC-303"; } }
        public int Cost { get { return 2000; } }
        public int HP { get { return 1000; } }
        public int Shields { get { return 3000; } }
        public int ShieldRechargeDelay { get { return 6000; } }
        public float ShieldRechargeRate { get { return 100; } }
        public float Speed { get { return 3.3f; } }
        public float TurnRate { get { return 1; } }
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

        public BC303_Ref(Texture2D texture, Texture2D icon, 
            Texture2D infoTexture, 
            Dictionary<string, ResearchItem> researchTree,
            Dictionary<string, IWeapon> weapons)
        {
            Texture = texture;
            IconTexture = icon;
            InfoTexture = infoTexture;
            Prerequisites = new List<ResearchItem>() 
            { 
                researchTree["Hyperdrive"]
            };

            if (researchTree["Nuclear Missiles"].Researched)
            {
                WeaponGroups = new List<WeaponGroup>()
                {
                    new WeaponGroup(new List<Weapon>() 
                    {
                        new Weapon(weapons["Railgun"], new Vector2(160, 3),
                            -45, 45),
                        new Weapon(weapons["Railgun"], new Vector2(198, 3),
                            -45, 45),
                        new Weapon(weapons["Railgun"], new Vector2(237, 3),
                            -45, 45),
                        new Weapon(weapons["Railgun"], new Vector2(275, 3),
                            -45, 45),
                        new Weapon(weapons["Railgun"], new Vector2(137, 66),
                            -225, 0),
                        new Weapon(weapons["Railgun"], new Vector2(299, 66),
                            0, 225),
                        new Weapon(weapons["Railgun"], new Vector2(294, 106),
                            0, 225),
                        new Weapon(weapons["Railgun"], new Vector2(142, 106),
                            -225, 0)
                    }, "Fore Railguns"),
                    new WeaponGroup(new List<Weapon>()
                    {
                        new Weapon(weapons["Railgun"], new Vector2(162, 145),
                            0, -180),
                        new Weapon(weapons["Railgun"], new Vector2(162, 203),
                            0, -180),
                        new Weapon(weapons["Railgun"], new Vector2(162, 261),
                            0, -180),
                        new Weapon(weapons["Railgun"], new Vector2(162, 318),
                            0, -180),
                        new Weapon(weapons["Railgun"], new Vector2(162, 376),
                            0, -180),
                        new Weapon(weapons["Railgun"], new Vector2(162, 434),
                            0, -180),
                        new Weapon(weapons["Railgun"], new Vector2(137, 66),
                            -225, 0),
                        new Weapon(weapons["Railgun"], new Vector2(142, 106),
                            0, 225)
                    }, "Port Railguns"),
                    new WeaponGroup(new List<Weapon>()
                    {
                        new Weapon(weapons["Railgun"], new Vector2(274, 145),
                            0, -180),
                        new Weapon(weapons["Railgun"], new Vector2(274, 203),
                            0, -180),
                        new Weapon(weapons["Railgun"], new Vector2(274, 261),
                            0, -180),
                        new Weapon(weapons["Railgun"], new Vector2(274, 318),
                            0, -180),
                        new Weapon(weapons["Railgun"], new Vector2(274, 376),
                            0, -180),
                        new Weapon(weapons["Railgun"], new Vector2(274, 434),
                            0, 180),
                        new Weapon(weapons["Railgun"], new Vector2(299, 66),
                            0, 225),
                        new Weapon(weapons["Railgun"], new Vector2(294, 106),
                            0, 225)
                    }, "Starboard Railguns"),
                    new WeaponGroup(new List<Weapon>()
                    {
                        new Weapon(weapons["Railgun"], new Vector2(57, 357),
                            0, 360),
                        new Weapon(weapons["Railgun"], new Vector2(75, 537),
                            0, 360),
                        new Weapon(weapons["Railgun"], new Vector2(359, 536),
                            0, 360),
                        new Weapon(weapons["Railgun"], new Vector2(377, 536),
                            0, 360)
                    }, "Aft Railguns"),
                    new WeaponGroup(new List<Weapon>()
                    {
                        new Weapon(weapons["Missile"], new Vector2(195, 6),
                            0, 360, 2, 1, 1, 1, 1),
                        new Weapon(weapons["Missile"], new Vector2(237, 6),
                            0, 360, 2, 1, 1, 1, 1),
                        new Weapon(weapons["Missile"], new Vector2(168, 99),
                            0, 360, 2, 1, 1, 1, 1),
                        new Weapon(weapons["Missile"], new Vector2(266, 99),
                            0, 360, 2, 1, 1, 1, 1),
                        new Weapon(weapons["Missile"], new Vector2(202, 379),
                            0, 360, 2, 1, 1, 1, 1),
                        new Weapon(weapons["Missile"], new Vector2(202, 397),
                            0, 360, 3, 1, 1, 1, 1),
                        new Weapon(weapons["Missile"], new Vector2(235, 379),
                            0, 360, 2, 1, 1, 1, 1),
                        new Weapon(weapons["Missile"], new Vector2(235, 397),
                            0, 360, 2, 1, 1, 1, 1),
                        new Weapon(weapons["Missile"], new Vector2(190, 190),
                            0, 360, 2, 1, 1, 1, 1),
                        new Weapon(weapons["Missile"], new Vector2(190, 290),
                            0, 360, 2, 1, 1, 1, 1),
                        new Weapon(weapons["Missile"], new Vector2(244, 190),
                            0, 360, 2, 1, 1, 1, 1),
                        new Weapon(weapons["Missile"], new Vector2(244, 290),
                            0, 360, 3, 1, 1, 1, 1)
                    }, "Missile Launchers", 15)
                };
            }
            else
            {
                WeaponGroups = new List<WeaponGroup>()
                {
                    new WeaponGroup(new List<Weapon>() 
                    {
                        new Weapon(weapons["Railgun"], new Vector2(160, 3),
                            -45, 45),
                        new Weapon(weapons["Railgun"], new Vector2(198, 3),
                            -45, 45),
                        new Weapon(weapons["Railgun"], new Vector2(237, 3),
                            -45, 45),
                        new Weapon(weapons["Railgun"], new Vector2(275, 3),
                            -45, 45),
                        new Weapon(weapons["Railgun"], new Vector2(137, 66),
                            -225, 0),
                        new Weapon(weapons["Railgun"], new Vector2(299, 66),
                            0, 225),
                        new Weapon(weapons["Railgun"], new Vector2(294, 106),
                            0, 225),
                        new Weapon(weapons["Railgun"], new Vector2(142, 106),
                            -225, 0)
                    }, "Fore Railguns"),
                    new WeaponGroup(new List<Weapon>()
                    {
                        new Weapon(weapons["Railgun"], new Vector2(162, 145),
                            0, -180),
                        new Weapon(weapons["Railgun"], new Vector2(162, 203),
                            0, -180),
                        new Weapon(weapons["Railgun"], new Vector2(162, 261),
                            0, -180),
                        new Weapon(weapons["Railgun"], new Vector2(162, 318),
                            0, -180),
                        new Weapon(weapons["Railgun"], new Vector2(162, 376),
                            0, -180),
                        new Weapon(weapons["Railgun"], new Vector2(162, 434),
                            0, -180),
                        new Weapon(weapons["Railgun"], new Vector2(137, 66),
                            -225, 0),
                        new Weapon(weapons["Railgun"], new Vector2(142, 106),
                            0, 225)
                    }, "Port Railguns"),
                    new WeaponGroup(new List<Weapon>()
                    {
                        new Weapon(weapons["Railgun"], new Vector2(274, 145),
                            0, -180),
                        new Weapon(weapons["Railgun"], new Vector2(274, 203),
                            0, -180),
                        new Weapon(weapons["Railgun"], new Vector2(274, 261),
                            0, -180),
                        new Weapon(weapons["Railgun"], new Vector2(274, 318),
                            0, -180),
                        new Weapon(weapons["Railgun"], new Vector2(274, 376),
                            0, -180),
                        new Weapon(weapons["Railgun"], new Vector2(274, 434),
                            0, 180),
                        new Weapon(weapons["Railgun"], new Vector2(299, 66),
                            0, 225),
                        new Weapon(weapons["Railgun"], new Vector2(294, 106),
                            0, 225)
                    }, "Starboard Railguns"),
                    new WeaponGroup(new List<Weapon>()
                    {
                        new Weapon(weapons["Railgun"], new Vector2(57, 357),
                            0, 360),
                        new Weapon(weapons["Railgun"], new Vector2(75, 537),
                            0, 360),
                        new Weapon(weapons["Railgun"], new Vector2(359, 536),
                            0, 360),
                        new Weapon(weapons["Railgun"], new Vector2(377, 536),
                            0, 360)
                    }, "Aft Railguns"),
                    new WeaponGroup(new List<Weapon>()
                    {
                        new Weapon(weapons["Missile"], new Vector2(195, 6),
                            0, 360),
                        new Weapon(weapons["Missile"], new Vector2(237, 6),
                            0, 360),
                        new Weapon(weapons["Missile"], new Vector2(168, 99),
                            0, 360),
                        new Weapon(weapons["Missile"], new Vector2(266, 99),
                            0, 360),
                        new Weapon(weapons["Missile"], new Vector2(202, 379),
                            0, 360),
                        new Weapon(weapons["Missile"], new Vector2(202, 397),
                            0, 360),
                        new Weapon(weapons["Missile"], new Vector2(235, 379),
                            0, 360),
                        new Weapon(weapons["Missile"], new Vector2(235, 397),
                            0, 360),
                        new Weapon(weapons["Missile"], new Vector2(190, 190),
                            0, 360),
                        new Weapon(weapons["Missile"], new Vector2(190, 290),
                            0, 360),
                        new Weapon(weapons["Missile"], new Vector2(244, 190),
                            0, 360),
                        new Weapon(weapons["Missile"], new Vector2(244, 290),
                            0, 360)
                    }, "Missile Launchers", 15)
                };
            }

            SpawnMode = SpawnModes.Simultaneous;
            SpawnRate = 60;
            Fighters = new Dictionary<string,int>() { { "F-302", 8 } };
            SpawnPoints = new Vector2[]
            {
                new Vector2(85, 410),
                new Vector2(350, 410)
            };
        }
    }
}
