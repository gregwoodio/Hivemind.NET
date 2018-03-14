using Hivemind.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hivemind.Entities
{
    public class Injury
    {
        public InjuryEnum InjuryEnum { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Func<Ganger, Ganger> InjuryEffect { get; set; }
    }
}
