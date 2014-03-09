using FleetCom.Classes.Ships.Weapons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FleetCom.Classes.Ships.Tauri
{
    public class BC304_Ref : IShip
    {
        public string Name { get { return "BC-304"; } }
        public int Cost { get { return 6000; } }
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

        public BC304_Ref(Texture2D texture, Texture2D icon,
            Texture2D infoTexture,
            Dictionary<string, ResearchItem> researchTree,
            Dictionary<string, IWeapon> weapons)
        {
            Texture = texture;
            IconTexture = icon;
            InfoTexture = infoTexture;

            Prerequisites = new List<ResearchItem>() 
            { 
                researchTree["Intergalactic Hyperdrive"]
            };

            if (researchTree["Alien Diplomacy"].Researched)
            {
                Shields = 10000;
                ShieldRechargeDelay = 2000;
                ShieldRechargeRate = 400;
            }
            else
            {
                Shields = 5000;
                ShieldRechargeDelay = 4000;
                ShieldRechargeRate = 200;
            }

            HP = 3000;

            if (researchTree["Zero-Point Energy"].Researched)
            {
                Speed = 7;
                TurnRate = 2;
            }
            else
            {
                TurnRate = 1.5f;
                Speed = 4;
            }

            WeaponGroups = new List<WeaponGroup>()
            {
                new WeaponGroup(new List<Weapon>()
                {
                    new Weapon(weapons["Railgun"], new Vector2(218, 122),
                        -90, 90),
                    new Weapon(weapons["Railgun"], new Vector2(218, 122),
                        -90, 90),
                    new Weapon(weapons["Railgun"], new Vector2(248, 122),
                        -90, 90),
                    new Weapon(weapons["Railgun"], new Vector2(248, 122),
                        -90, 90),
                    new Weapon(weapons["Railgun"], new Vector2(278, 122),
                        -90, 90),
                    new Weapon(weapons["Railgun"], new Vector2(278, 122),
                        -90, 90),
                    new Weapon(weapons["Railgun"], new Vector2(308, 122),
                        -90, 90),
                    new Weapon(weapons["Railgun"], new Vector2(308, 122),
                        -90, 90),
                }, "Fore Railguns"),

                new WeaponGroup(new List<Weapon>()
                {
                    new Weapon(weapons["Railgun"], new Vector2(193, 275),
                        0, -180),
                    new Weapon(weapons["Railgun"], new Vector2(193, 303),
                        0, -180),
                    new Weapon(weapons["Railgun"], new Vector2(193, 331),
                        0, -180),
                    new Weapon(weapons["Railgun"], new Vector2(193, 359),
                        0, -180),
                    new Weapon(weapons["Railgun"], new Vector2(193, 387),
                        0, -180),
                    new Weapon(weapons["Railgun"], new Vector2(193, 415),
                        0, -180),
                }, "Port Bow Railguns"),

                new WeaponGroup(new List<Weapon>()
                {
                    new Weapon(weapons["Railgun"], new Vector2(333, 275),
                        0, 180),
                    new Weapon(weapons["Railgun"], new Vector2(333, 303),
                        0, 180),
                    new Weapon(weapons["Railgun"], new Vector2(333, 331),
                        0, 180),
                    new Weapon(weapons["Railgun"], new Vector2(333, 359),
                        0, 180),
                    new Weapon(weapons["Railgun"], new Vector2(333, 387),
                        0, 180),
                    new Weapon(weapons["Railgun"], new Vector2(333, 415),
                        0, 180),
                }, "Starboard Bow Railguns"),

                new WeaponGroup(new List<Weapon>()
                {
                    new Weapon(weapons["Railgun"], new Vector2(123, 655),
                        0, -270),
                    new Weapon(weapons["Railgun"], new Vector2(123, 697),
                        0, -270),
                    new Weapon(weapons["Railgun"], new Vector2(123, 739),
                        0, -270),
                    new Weapon(weapons["Railgun"], new Vector2(123, 781),
                        0, -270),
                    new Weapon(weapons["Railgun"], new Vector2(123, 823),
                        0, -270),
                    new Weapon(weapons["Railgun"], new Vector2(123, 865),
                        0, -270),
                }, "Port Stern Railguns"),

                new WeaponGroup(new List<Weapon>()
                {
                    new Weapon(weapons["Railgun"], new Vector2(637, 655),
                        0, 270),
                    new Weapon(weapons["Railgun"], new Vector2(637, 697),
                        0, 270),
                    new Weapon(weapons["Railgun"], new Vector2(637, 739),
                        0, 270),
                    new Weapon(weapons["Railgun"], new Vector2(637, 781),
                        0, 270),
                    new Weapon(weapons["Railgun"], new Vector2(637, 823),
                        0, 270),
                    new Weapon(weapons["Railgun"], new Vector2(637, 865),
                        0, 270),
                }, "Starboard Stern Railguns")
            };

            if (researchTree["Nuclear Missiles"].Researched)
            {
                WeaponGroups.Add(new WeaponGroup(new List<Weapon>()
                {
                    new Weapon(weapons["Missile"], new Vector2(249, 237),
                        0, 360, 2.5f, 1, 1, 1.5, 1.5),
                    new Weapon(weapons["Missile"], new Vector2(249, 261),
                        0, 360, 2.5f, 1, 1, 1.5, 1.5),
                    new Weapon(weapons["Missile"], new Vector2(249, 284),
                        0, 360, 2.5f, 1, 1, 1.5, 1.5),
                    new Weapon(weapons["Missile"], new Vector2(249, 308),
                        0, 360, 2.5f, 1, 1, 1.5, 1.5),
                    new Weapon(weapons["Missile"], new Vector2(249, 332),
                        0, 360, 2.5f, 1, 1, 1.5, 1.5),
                    new Weapon(weapons["Missile"], new Vector2(249, 256),
                        0, 360, 2.5f, 1, 1, 1.5, 1.5),
                    new Weapon(weapons["Missile"], new Vector2(249, 381),
                        0, 360, 2.5f, 1, 1, 1.5, 1.5),
                    new Weapon(weapons["Missile"], new Vector2(249, 405),
                        0, 360, 2.5f, 1, 1, 1.5, 1.5),
                    new Weapon(weapons["Missile"], new Vector2(278, 237),
                        0, 360, 2.5f, 1, 1, 1.5, 1.5),
                    new Weapon(weapons["Missile"], new Vector2(278, 261),
                        0, 360, 2.5f, 1, 1, 1.5, 1.5),
                    new Weapon(weapons["Missile"], new Vector2(278, 284),
                        0, 360, 2.5f, 1, 1, 1.5, 1.5),
                    new Weapon(weapons["Missile"], new Vector2(278, 308),
                        0, 360, 2.5f, 1, 1, 1.5, 1.5),
                    new Weapon(weapons["Missile"], new Vector2(278, 332),
                        0, 360, 2.5f, 1, 1, 1.5, 1.5),
                    new Weapon(weapons["Missile"], new Vector2(278, 256),
                        0, 360, 2.5f, 1, 1, 1.5, 1.5),
                    new Weapon(weapons["Missile"], new Vector2(278, 381),
                        0, 360, 2.5f, 1, 1, 1.5, 1.5),
                    new Weapon(weapons["Missile"], new Vector2(278, 405),
                        0, 360, 2.5f, 1, 1, 1.5, 1.5),
                }, "Missile Launchers"));
            }
            else
            {
                WeaponGroups.Add(new WeaponGroup(new List<Weapon>()
                {
                    new Weapon(weapons["Missile"], new Vector2(249, 237),
                        0, 360, 1.5f, 1, 1, 1.5, 1.5),
                    new Weapon(weapons["Missile"], new Vector2(249, 261),
                        0, 360, 1.5f, 1, 1, 1.5, 1.5),
                    new Weapon(weapons["Missile"], new Vector2(249, 284),
                        0, 360, 1.5f, 1, 1, 1.5, 1.5),
                    new Weapon(weapons["Missile"], new Vector2(249, 308),
                        0, 360, 1.5f, 1, 1, 1.5, 1.5),
                    new Weapon(weapons["Missile"], new Vector2(249, 332),
                        0, 360, 1.5f, 1, 1, 1.5, 1.5),
                    new Weapon(weapons["Missile"], new Vector2(249, 256),
                        0, 360, 1.5f, 1, 1, 1.5, 1.5),
                    new Weapon(weapons["Missile"], new Vector2(249, 381),
                        0, 360, 1.5f, 1, 1, 1.5, 1.5),
                    new Weapon(weapons["Missile"], new Vector2(249, 405),
                        0, 360, 1.5f, 1, 1, 1.5, 1.5),
                    new Weapon(weapons["Missile"], new Vector2(278, 237),
                        0, 360, 1.5f, 1, 1, 1.5, 1.5),
                    new Weapon(weapons["Missile"], new Vector2(278, 261),
                        0, 360, 1.5f, 1, 1, 1.5, 1.5),
                    new Weapon(weapons["Missile"], new Vector2(278, 284),
                        0, 360, 1.5f, 1, 1, 1.5, 1.5),
                    new Weapon(weapons["Missile"], new Vector2(278, 308),
                        0, 360, 1.5f, 1, 1, 1.5, 1.5),
                    new Weapon(weapons["Missile"], new Vector2(278, 332),
                        0, 360, 1.5f, 1, 1, 1.5, 1.5),
                    new Weapon(weapons["Missile"], new Vector2(278, 256),
                        0, 360, 1.5f, 1, 1, 1.5, 1.5),
                    new Weapon(weapons["Missile"], new Vector2(278, 381),
                        0, 360, 1.5f, 1, 1, 1.5, 1.5),
                    new Weapon(weapons["Missile"], new Vector2(278, 405),
                        0, 360, 1.5f, 1, 1, 1.5, 1.5),
                }, "Missile Launchers"));

                if (researchTree["Alien Diplomacy"].Researched)
                {
                    if (researchTree["Zero-Point Energy"].Researched)
                    {
                        WeaponGroups.Add(new WeaponGroup(new List<Weapon>()
                        {
                            new Weapon(weapons["Plamsa Beam"], new Vector2(208, 145),
                                30, -180, 1.5, 1.5, 1, 1, 1),
                            new Weapon(weapons["Plamsa Beam"], new Vector2(210, 217),
                                30, -180, 1.5, 1.5, 1, 1, 1),
                            new Weapon(weapons["Plamsa Beam"], new Vector2(308, 122),
                                30, -180, 1.5, 1.5, 1, 1, 1),
                            new Weapon(weapons["Plamsa Beam"], new Vector2(316, 217),
                                30, -180, 1.5, 1.5, 1, 1, 1),
                        }, "Plasma Weapons"));
                    }
                    else
                    {
                        WeaponGroups.Add(new WeaponGroup(new List<Weapon>()
                        {
                            new Weapon(weapons["Plamsa Beam"], new Vector2(208, 145),
                                30, -180),
                            new Weapon(weapons["Plamsa Beam"], new Vector2(210, 217),
                                30, -180),
                            new Weapon(weapons["Plamsa Beam"], new Vector2(308, 122),
                                30, -180),
                            new Weapon(weapons["Plamsa Beam"], new Vector2(316, 217),
                                30, -180),
                        }, "Plasma Weapons"));
                    }
                }

                SpawnMode = SpawnModes.Simultaneous;
                SpawnRate = 30;
                Fighters = new Dictionary<string, int>() { { "F-302", 16 } };
                SpawnPoints = new Vector2[]
                {
                    new Vector2(90, 585),
                    new Vector2(440, 585)
                };
            }
        }
    }
}
