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
        public string Name { get { return "F-302"; } }
        public int Cost { get { return 200; } }
        public int HP { get { return 1000; } }
        public int Shields { get { return 2000; } }
        public int ShieldRechargeDelay { get { return 6000; } }
        public float ShieldRechargeRate { get { return 100; } }
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
                researchTree["Space Flight"],
                researchTree["Hyperdrive"]
            };

            if (researchTree["Nuclear Missiles"].Researched)
            {
                WeaponGroups = new List<WeaponGroup>()
                {
                    new WeaponGroup(new List<Weapon>() 
                    {
                        new Weapon(weapons["Railgun"], new Vector2(200, 5),
                            -45, 45),
                        new Weapon(weapons["Railgun"], new Vector2(249, 5),
                            -45, 45),
                        new Weapon(weapons["Railgun"], new Vector2(298, 5),
                            -45, 45),
                        new Weapon(weapons["Railgun"], new Vector2(345, 5),
                            -45, 45),
                        new Weapon(weapons["Railgun"], new Vector2(170, 85),
                            -225, 0),
                        new Weapon(weapons["Railgun"], new Vector2(177, 135),
                            0, 225),
                        new Weapon(weapons["Railgun"], new Vector2(465, 135),
                            0, 225),
                        new Weapon(weapons["Railgun"], new Vector2(373, 85),
                            -225, 0)
                    }, "Fore Railguns"),
                    new WeaponGroup(new List<Weapon>()
                    {
                        new Weapon(weapons["Railgun"], new Vector2(195, 185),
                            0, -180),
                        new Weapon(weapons["Railgun"], new Vector2(195, 256),
                            0, -180),
                        new Weapon(weapons["Railgun"], new Vector2(195, 327),
                            0, -180),
                        new Weapon(weapons["Railgun"], new Vector2(195, 398),
                            0, -180),
                        new Weapon(weapons["Railgun"], new Vector2(195, 469),
                            0, -180),
                        new Weapon(weapons["Railgun"], new Vector2(195, 540),
                            0, -180),
                        new Weapon(weapons["Railgun"], new Vector2(170, 85),
                            -225, 0),
                        new Weapon(weapons["Railgun"], new Vector2(177, 135),
                            0, 225)
                    }, "Port Railguns"),
                    new WeaponGroup(new List<Weapon>()
                    {
                        new Weapon(weapons["Railgun"], new Vector2(350, 185),
                            0, 180),
                        new Weapon(weapons["Railgun"], new Vector2(350, 256),
                            0, 180),
                        new Weapon(weapons["Railgun"], new Vector2(350, 327),
                            0, 180),
                        new Weapon(weapons["Railgun"], new Vector2(350, 398),
                            0, 180),
                        new Weapon(weapons["Railgun"], new Vector2(350, 469),
                            0, 180),
                        new Weapon(weapons["Railgun"], new Vector2(350, 540),
                            0, 180),
                        new Weapon(weapons["Railgun"], new Vector2(465, 135),
                            0, 225),
                        new Weapon(weapons["Railgun"], new Vector2(373, 85),
                            0, 225)
                    }, "Starboard Railguns"),
                    new WeaponGroup(new List<Weapon>()
                    {
                        new Weapon(weapons["Railgun"], new Vector2(449, 672),
                            0, 360),
                        new Weapon(weapons["Railgun"], new Vector2(471, 672),
                            0, 360),
                        new Weapon(weapons["Railgun"], new Vector2(70, 673),
                            0, 360),
                        new Weapon(weapons["Railgun"], new Vector2(93, 673),
                            0, 360)
                    }, "Aft Railguns"),
                    new WeaponGroup(new List<Weapon>()
                    {
                        new Weapon(weapons["Missile"], new Vector2(236, 200),
                            0, 360, 2, 1, 1, 1, 1),
                        new Weapon(weapons["Missile"], new Vector2(236, 240),
                            0, 360, 2, 1, 1, 1, 1),
                        new Weapon(weapons["Missile"], new Vector2(236, 280),
                            0, 360, 2, 1, 1, 1, 1),
                        new Weapon(weapons["Missile"], new Vector2(236, 320),
                            0, 360, 2, 1, 1, 1, 1),
                        new Weapon(weapons["Missile"], new Vector2(236, 360),
                            0, 360, 2, 1, 1, 1, 1),
                        new Weapon(weapons["Missile"], new Vector2(236, 400),
                            0, 360, 3, 1, 1, 1, 1)
                    }, "Port Missile Launchers", 15),
                    new WeaponGroup(new List<Weapon>()
                    {
                        new Weapon(weapons["Missile"], new Vector2(305, 200),
                            0, 360, 2, 1, 1, 1, 1),
                        new Weapon(weapons["Missile"], new Vector2(305, 240),
                            0, 360, 2, 1, 1, 1, 1),
                        new Weapon(weapons["Missile"], new Vector2(305, 280),
                            0, 360, 2, 1, 1, 1, 1),
                        new Weapon(weapons["Missile"], new Vector2(305, 320),
                            0, 360, 2, 1, 1, 1, 1),
                        new Weapon(weapons["Missile"], new Vector2(305, 360),
                            0, 360, 2, 1, 1, 1, 1),
                        new Weapon(weapons["Missile"], new Vector2(305, 400),
                            0, 360, 3, 1, 1, 1, 1)
                    }, "Starboard Missile Launchers", 15)
                };
            }
            else
            {
                WeaponGroups = new List<WeaponGroup>()
                {
                    new WeaponGroup(new List<Weapon>() 
                    {
                        new Weapon(weapons["Railgun"], new Vector2(200, 5),
                            -45, 45),
                        new Weapon(weapons["Railgun"], new Vector2(249, 5),
                            -45, 45),
                        new Weapon(weapons["Railgun"], new Vector2(298, 5),
                            -45, 45),
                        new Weapon(weapons["Railgun"], new Vector2(345, 5),
                            -45, 45),
                        new Weapon(weapons["Railgun"], new Vector2(170, 85),
                            -225, 0),
                        new Weapon(weapons["Railgun"], new Vector2(177, 135),
                            0, 225),
                        new Weapon(weapons["Railgun"], new Vector2(465, 135),
                            0, 225),
                        new Weapon(weapons["Railgun"], new Vector2(373, 85),
                            -225, 0)
                    }, "Fore Railguns"),
                    new WeaponGroup(new List<Weapon>()
                    {
                        new Weapon(weapons["Railgun"], new Vector2(195, 185),
                            0, -180),
                        new Weapon(weapons["Railgun"], new Vector2(195, 256),
                            0, -180),
                        new Weapon(weapons["Railgun"], new Vector2(195, 327),
                            0, -180),
                        new Weapon(weapons["Railgun"], new Vector2(195, 398),
                            0, -180),
                        new Weapon(weapons["Railgun"], new Vector2(195, 469),
                            0, -180),
                        new Weapon(weapons["Railgun"], new Vector2(195, 540),
                            0, -180),
                        new Weapon(weapons["Railgun"], new Vector2(170, 85),
                            -225, 0),
                        new Weapon(weapons["Railgun"], new Vector2(177, 135),
                            0, 225)
                    }, "Port Railguns"),
                    new WeaponGroup(new List<Weapon>()
                    {
                        new Weapon(weapons["Railgun"], new Vector2(350, 185),
                            0, 180),
                        new Weapon(weapons["Railgun"], new Vector2(350, 256),
                            0, 180),
                        new Weapon(weapons["Railgun"], new Vector2(350, 327),
                            0, 180),
                        new Weapon(weapons["Railgun"], new Vector2(350, 398),
                            0, 180),
                        new Weapon(weapons["Railgun"], new Vector2(350, 469),
                            0, 180),
                        new Weapon(weapons["Railgun"], new Vector2(350, 540),
                            0, 180),
                        new Weapon(weapons["Railgun"], new Vector2(465, 135),
                            0, 225),
                        new Weapon(weapons["Railgun"], new Vector2(373, 85),
                            0, 225)
                    }, "Starboard Railguns"),
                    new WeaponGroup(new List<Weapon>()
                    {
                        new Weapon(weapons["Railgun"], new Vector2(449, 672),
                            0, 360),
                        new Weapon(weapons["Railgun"], new Vector2(471, 672),
                            0, 360),
                        new Weapon(weapons["Railgun"], new Vector2(70, 673),
                            0, 360),
                        new Weapon(weapons["Railgun"], new Vector2(93, 673),
                            0, 360)
                    }, "Aft Railguns"),
                    new WeaponGroup(new List<Weapon>()
                    {
                        new Weapon(weapons["Missile"], new Vector2(236, 200),
                            0, 360),
                        new Weapon(weapons["Missile"], new Vector2(236, 240),
                            0, 360),
                        new Weapon(weapons["Missile"], new Vector2(236, 280),
                            0, 360),
                        new Weapon(weapons["Missile"], new Vector2(236, 320),
                            0, 360),
                        new Weapon(weapons["Missile"], new Vector2(236, 360),
                            0, 360),
                        new Weapon(weapons["Missile"], new Vector2(236, 400),
                            0, 360)
                    }, "Port Missile Launchers", 15),
                    new WeaponGroup(new List<Weapon>()
                    {
                        new Weapon(weapons["Missile"], new Vector2(305, 200),
                            0, 360),
                        new Weapon(weapons["Missile"], new Vector2(305, 240),
                            0, 360),
                        new Weapon(weapons["Missile"], new Vector2(305, 280),
                            0, 360),
                        new Weapon(weapons["Missile"], new Vector2(305, 320),
                            0, 360),
                        new Weapon(weapons["Missile"], new Vector2(305, 360),
                            0, 360),
                        new Weapon(weapons["Missile"], new Vector2(305, 400),
                            0, 360)
                    }, "Starboard Missile Launchers", 15)
                };
            }

            SpawnMode = SpawnModes.Simultaneous;
            SpawnRate = 60;
            Fighters = new Dictionary<string,int>() { { "F-302", 8 } };
            SpawnPoints = new Vector2[]
            {
                new Vector2(105, 520),
                new Vector2(440, 520)
            };
        }
    }
}
