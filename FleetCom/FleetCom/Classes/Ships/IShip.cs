using FleetCom.Classes.Ships.Weapons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FleetCom.Classes.Ships
{
    public enum ShipSizes
    {
        Fine,
        Diminutive,
        Tiny,
        Small,
        Medium,
        Large,
        Huge,
        Gargantuan,
        Colossal,
        ColossalPlus
    }
    public enum SpawnModes
    {
        AllAtOnce,
        Simultaneous,
        OneAtATime
    }

    public interface IShip
    {
        //Basic info
        string Name { get; }
        int Cost { get; }
        int HP { get; }
        int Shields { get; }
        int ShieldRechargeDelay { get; }
        float ShieldRechargeRate { get; }
        ShipSizes ShipSize { get; }

        //Physics
        float Speed { get; }
        float TurnRate { get; }

        //Graphics
        Texture2D Texture { get; set; }
        Texture2D IconTexture { get; set; }
        Texture2D InfoTexture { get; set; }

        //Other classes
        List<ResearchItem> Prerequisites { get; set; }
        List<WeaponGroup> WeaponGroups { get; set; }

        //Fighter spawning
        SpawnModes SpawnMode { get; set; }
        int SpawnRate { get; set; }
        Vector2[] SpawnPoints { get; set; }
        Dictionary<string, int> Fighters { get; set; }
    }
}
