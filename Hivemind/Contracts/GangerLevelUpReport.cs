using Hivemind.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hivemind.Contracts
{
    public class GangerLevelUpReport
    {
        public string GangerName { get; set; }
        public string Description { get; set; }
        public IEnumerable<SkillType> NewSkillFromCategory { get; set; }
    }
}
