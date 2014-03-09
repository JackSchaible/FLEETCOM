using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FleetCom.Classes.Ships.Weapons
{
    public class Railguns_Ref : IWeapon
    {
        public string Name { get { return "Railgun"; } }
        public Behaviors Behavior { get { return Behaviors.Unguided; } }
        public int Damage { get { return 10; } }
        public int Cooldown { get { return 7; } }
        public int Ammo { get { return 10000; } }
        public int Range { get { return 750; } }
        public int ProjectileSpeed { get { return 13; } }
        public Dictionary<ShipSizes, Accuracies> WeaponAccuracies { get; set; }

        public Texture2D AmmoTexture { get; set; }

        public Railguns_Ref(Texture2D ammoTexture)
        {
            AmmoTexture = ammoTexture;

            WeaponAccuracies = new Dictionary<ShipSizes, Accuracies>();
            WeaponAccuracies.Add(ShipSizes.Fine, Accuracies.Good);
            WeaponAccuracies.Add(ShipSizes.Diminutive, Accuracies.Good);
            WeaponAccuracies.Add(ShipSizes.Tiny, Accuracies.VeryGood);
            WeaponAccuracies.Add(ShipSizes.Small, Accuracies.VeryGood);
            WeaponAccuracies.Add(ShipSizes.Medium, Accuracies.Perfect);
            WeaponAccuracies.Add(ShipSizes.Large, Accuracies.Perfect);
            WeaponAccuracies.Add(ShipSizes.Huge, Accuracies.Perfect);
            WeaponAccuracies.Add(ShipSizes.Gargantuan, Accuracies.Perfect);
            WeaponAccuracies.Add(ShipSizes.Colossal, Accuracies.Perfect);
            WeaponAccuracies.Add(ShipSizes.ColossalPlus, Accuracies.Perfect);
        }
    }
}
