using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hivemind.Entities
{
    public class GangTerritory
    {
        public string GangTerritoryId { get; set; }
        public string GangId { get; set; }
        public Territory Territory { get; set; }

        public GangTerritory()
        {
            Territory = new Territory();
        }
    }
}
