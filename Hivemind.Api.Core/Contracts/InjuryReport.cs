using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hivemind.Contracts
{
    public class InjuryReport
    {
        public IEnumerable<GangerInjuryReport> Injuries { get; set; }
    }
}
