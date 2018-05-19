using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hivemind.Contracts
{
    public class PostGameReport
    {
        public InjuryReport Injuries { get; set; }
        public GangLevelUpReport Experience { get; set; }
        public IncomeReport Income { get; set; }
    }
}
