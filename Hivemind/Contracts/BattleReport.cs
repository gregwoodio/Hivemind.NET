using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hivemind.Contracts
{
    public class BattleReport
    {
        public int GangId { get; set; }
        public IEnumerable<GangerBattleStats> GangBattleStats { get; set; }
    }
}
