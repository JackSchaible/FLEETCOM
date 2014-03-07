using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FleetCom.Classes.Ships.Weapons
{
    public class Missile_Ref : IWeapon
    {
        public string Name { get { return "Missile"; } }
        public Behaviors Behavior { get { return Behaviors.Homing; } }
        public int Damage { get { return 50; } }
        public int Cooldown { get { return 15; } }
        public int Ammo { get { return 2; } }
        public int Range { get { return 400; } }
        public int ProjectileSpeed { get { return 600; } }
        public Dictionary<ShipSizes, Accuracies> WeaponAccuracies { get; set; }

        public Texture2D AmmoTexture { get; set; }

        public Missile_Ref(Texture2D ammoTexture)
        {
            AmmoTexture = ammoTexture;

            WeaponAccuracies = new Dictionary<ShipSizes, Accuracies>();
            WeaponAccuracies.Add(ShipSizes.Fine, Accuracies.Good);
            WeaponAccuracies.Add(ShipSizes.Diminutive, Accuracies.VeryGood);
            WeaponAccuracies.Add(ShipSizes.Tiny, Accuracies.Perfect);
            WeaponAccuracies.Add(ShipSizes.Small, Accuracies.Perfect);
            WeaponAccuracies.Add(ShipSizes.Medium, Accuracies.Perfect);
            WeaponAccuracies.Add(ShipSizes.Large, Accuracies.Perfect);
            WeaponAccuracies.Add(ShipSizes.Huge, Accuracies.Perfect);
            WeaponAccuracies.Add(ShipSizes.Gargantuan, Accuracies.Perfect);
            WeaponAccuracies.Add(ShipSizes.Colossal, Accuracies.Perfect);
            WeaponAccuracies.Add(ShipSizes.ColossalPlus, Accuracies.Perfect);
        }
    }
}
