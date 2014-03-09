using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FleetCom.Classes.Ships.Weapons
{
    public class IonGunR : IWeapon
    {
        public string Name { get { return "Ion Gun"; } }
        public Behaviors Behavior { get { return Behaviors.Unguided; } }
        public int Damage { get { return 200; } }
        public int Cooldown { get { return 15; } }
        public int Ammo { get { return 5000; } }
        public int Range { get { return 700; } }
        public int ProjectileSpeed { get { return 15; } }
        public Dictionary<ShipSizes, Accuracies> WeaponAccuracies { get; set; }

        public Texture2D AmmoTexture { get; set; }

        public IonGunR(Texture2D ammoTexture)
        {
            AmmoTexture = ammoTexture;

            WeaponAccuracies = new Dictionary<ShipSizes, Accuracies>();
            WeaponAccuracies.Add(ShipSizes.Fine, Accuracies.VeryGood);
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
