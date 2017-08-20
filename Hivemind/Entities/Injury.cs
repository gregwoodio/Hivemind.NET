using Hivemind.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hivemind.Entities
{
    public delegate Ganger Effect(Ganger ganger);

    public class Injury
    {
        public InjuryEnum InjuryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Effect InjuryEffect { get; set; }
    }
}
