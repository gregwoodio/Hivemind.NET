using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hivemind.Entities
{
    public class Territory
    {
        public int TerritoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Income { get; set; }
        Func<string> SpecialRules;
    }
}
