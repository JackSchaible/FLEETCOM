using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FleetCom.Classes.Ships.Weapons
{
    public class Weapon : IWeapon
    {
        IWeapon WeaponReference { get; set; }

        public Texture2D AmmoTexture { get; set; }
        public Vector2 Offset { get; set; }
        public string Name { get; set; }
        public int AmmoUsed { get; set; }
        public int Damage { get; private set; }
        public int Cooldown { get; private set; }
        public int Ammo { get; private set; }
        public int Range { get; private set; }
        public double FiringAngleMax { get; private set; }
        public double FiringAngleMin { get; private set; }
        public int ProjectileSpeed { get; private set; }
        public Behaviors Behavior { get; private set; }
        public Dictionary<ShipSizes, Accuracies> WeaponAccuracies { get; set; }

        public Weapon(IWeapon weaponReferenceClass, Vector2 positionOffset,
            double firingAngleMin, double firingAngleMax)
        {
            Instantiate(weaponReferenceClass, positionOffset,
                firingAngleMin, firingAngleMax);
        }

        public Weapon(IWeapon weaponReferenceClass, Vector2 positionOffset,
            double firingAngleMin, double firingAngleMax, 
            double damageModifier, double cooldownModifier, 
            double ammoModifier, double rangeModifier, 
            double speedModifier)
        {
            Instantiate(weaponReferenceClass, positionOffset, 
                firingAngleMin, firingAngleMax);

            if (damageModifier < 0)
                throw new ArgumentOutOfRangeException("damageModifier", "Damage modifier must be greater than 0.");
            else
                Damage = (int)(WeaponReference.Damage * damageModifier);

            if (cooldownModifier < 0)
                throw new ArgumentOutOfRangeException("cooldownModifier", "Cooldown modifier must be greater than 0.");
            else
                Cooldown = (int)(WeaponReference.Cooldown * cooldownModifier);

            if (ammoModifier < 0)
                throw new ArgumentOutOfRangeException("ammoModifier", "Ammo modifier must be greater than 0.");
            else
                Ammo = (int)(WeaponReference.Ammo * ammoModifier);

            if (rangeModifier < 0)
                throw new ArgumentOutOfRangeException("rangeModifier", "Range modifier must be greater than 0.");
            else
                Range = (int)(WeaponReference.Range * rangeModifier);

            if (speedModifier < 0)
                throw new ArgumentOutOfRangeException("speedModifier", "Projectile Speed modifier must be greater than 0.");
            else
                ProjectileSpeed = (int)(WeaponReference.ProjectileSpeed * speedModifier);
        }

        void Instantiate(IWeapon weaponReferenceClass, 
            Vector2 positionOffset, double firingAngleMin,
            double firingAngleMax)
        {
            AmmoTexture = weaponReferenceClass.AmmoTexture;
            Offset = positionOffset;
            WeaponReference = weaponReferenceClass;
            AmmoUsed = 0;
            Name = weaponReferenceClass.Name;
            Damage = WeaponReference.Damage;
            Cooldown = WeaponReference.Cooldown;
            Ammo = WeaponReference.Ammo;
            Range = WeaponReference.Range;
            ProjectileSpeed = WeaponReference.ProjectileSpeed;
            Behavior = WeaponReference.Behavior;
            FiringAngleMin = firingAngleMin;
            FiringAngleMax = firingAngleMax;
            WeaponAccuracies = WeaponReference.WeaponAccuracies;
        }
    }
}
