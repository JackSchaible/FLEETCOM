using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FleetCom.Classes.Ships.Weapons
{
    enum GroupBehaviors
    {
        FireAllAtOnce,
        Alternate
    }

    public class WeaponGroup
    {
        public List<Weapon> Weapons { get; set; }
        GroupBehaviors GroupBehavior { get; set; }
        public string GroupName { get; set; }
        public int Delay { get; set; }

        public WeaponGroup(List<Weapon> weapons, string name)
        {
            Weapons = weapons;
            GroupBehavior = GroupBehaviors.FireAllAtOnce;
            GroupName = name;
            Delay = 0;
        }

        public WeaponGroup(List<Weapon> weapons, string name, int delay)
        {
            Weapons = weapons;
            GroupBehavior = GroupBehaviors.Alternate;
            GroupName = name;
            Delay = delay;
        }
    }
}
