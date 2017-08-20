using Hivemind.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hivemind.Contracts
{
    public class GangerInjuryReport
    {
        public Ganger TheGanger { get; set; }
        public IEnumerable<Injury> Injuries { get; set; }

        public string GetTitle()
        {
            var title = $"{TheGanger.Name} has been injured: ";
            foreach (var injury in Injuries)
            {
                title += $"\n{injury.Name}";
            }
            return title;
        }

        public string GetDescription()
        {
            var description = "";
            foreach (var injury in Injuries)
            {
                description += $"{injury.Description}\n";
            }
            return description;
        }
    }
}
