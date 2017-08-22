using Hivemind.Entities;
using Hivemind.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hivemind.Factories
{
    interface IGangerFactory
    {
        Ganger GetGanger(int id);
        Ganger CreateGanger(string name, GangerType type);
        Ganger CreateJuve(string name);
        Ganger CreateGanger(string name);
        Ganger CreateHeavy(string name);
        Ganger CreateLeader(string name);
        Ganger IncreaseStat(Ganger ganger, GangerStatistics stat, int? interval);
    }
}
