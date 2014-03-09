using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FleetCom.Classes.Ships.Weapons
{
    class PlasmaBeam_Ref : IWeapon
    {
        public string Name { get { return "Plasma Beam"; } }
        public Behaviors Behavior { get { return Behaviors.PointToPoint; } }
        public int Damage { get { return 5000; } }
        public int Cooldown { get { return 60; } }
        public int Ammo { get { return 10000; } }
        public int Range { get { return 800; } }
        public int ProjectileSpeed { get { return 300; } }
        public Dictionary<ShipSizes, Accuracies> WeaponAccuracies { get; set; }

        public Texture2D AmmoTexture { get; set; }

        public PlasmaBeam_Ref(Texture2D ammoTexture)
        {
            AmmoTexture = ammoTexture;

            WeaponAccuracies = new Dictionary<ShipSizes, Accuracies>();
            WeaponAccuracies.Add(ShipSizes.Fine, Accuracies.Perfect);
            WeaponAccuracies.Add(ShipSizes.Diminutive, Accuracies.Perfect);
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
