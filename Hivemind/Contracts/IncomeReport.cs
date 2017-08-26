using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hivemind.Contracts
{
    public class IncomeReport
    {
        public IEnumerable<TerritoryIncomeReport> Gross { get; set; }
        public int GiantKillerBonus { get; set; }
        public int Income { get; set; }
    }
}
