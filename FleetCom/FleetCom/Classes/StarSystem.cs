using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FleetCom
{
    public class StarSystem
    {
        public string Name { get; set; }
        public bool HasBase { get; set; }

        public StarSystem()
        {
            Name = "";
            HasBase = false;
        }
    }
}
