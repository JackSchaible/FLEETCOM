using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FleetCom.Classes.Ships.Weapons
{
    /// <summary>
    /// Describes the update behavior for each weapons type
    /// </summary>
    public enum Behaviors
    {
        /// <summary>
        /// Heat-seeking ordnance will lazily follow a target,
        /// but still has a chance to miss
        /// </summary>
        HeatSeeking,
        /// <summary>
        /// Guided weapons must be guided by the player
        /// </summary>
        Guided,
        /// <summary>
        /// Homing weapons home in on their targets and never
        /// miss
        /// </summary>
        Homing,
        /// <summary>
        /// Unguided weapons go in a straight line
        /// </summary>
        Unguided,
        /// <summary>
        /// Hits a point on the enemy ship from a point on the source
        /// ship; never misses
        /// </summary>
        PointToPoint
    }

    /// <summary>
    /// Describes the different levels of accuracy a weapon may have
    /// </summary>
    public enum Accuracies
    {
        /// <summary>
        /// Corresponse to a 100% accuracy rate.
        /// </summary>
        Perfect,
        /// <summary>
        /// Corresponse to an 85% accuracy rate.
        /// </summary>
        VeryGood,
        /// <summary>
        /// Corresponse to a 70% accuracy rate.
        /// </summary>
        Good,
        /// <summary>
        /// Corresponse to a 55% accuracy rate.
        /// </summary>
        Average,
        /// <summary>
        /// Corresponse to a 30% accuracy rate.
        /// </summary>
        Poor,
        /// <summary>
        /// Corresponse to a 15% accuracy rate.
        /// </summary>
        VeryPoor,
        /// <summary>
        /// Corresponse to a 0% accuracy rate.
        /// </summary>
        Impossible
    }

    public interface IWeapon
    {
        string Name { get; }
        int Damage { get; }
        int Cooldown { get; }
        int Ammo { get; }
        int Range { get; }
        int ProjectileSpeed { get; }
        Texture2D AmmoTexture { get; set; }
        Behaviors Behavior { get; }
        Dictionary<ShipSizes, Accuracies> WeaponAccuracies { get; }
    }
}
