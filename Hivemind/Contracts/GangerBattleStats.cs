using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hivemind.Contracts
{
    public class GangerBattleStats
    {
        public string GangerId { get; set; }
        public int Kills { get; set; }
        public int Objectives { get; set; }
        public bool Down { get; set; }
        public bool OutOfAction { get; set; }
    }
}
