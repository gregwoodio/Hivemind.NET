using Hivemind.Entities;
using Hivemind.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hivemind.Factories
{
    public interface IGangerFactory
    {
        Ganger GetGanger(string id);
        Ganger CreateGanger(string name, GangerType type);
        Ganger CreateJuve(string name);
        Ganger CreateGanger(string name);
        Ganger CreateHeavy(string name);
        Ganger CreateLeader(string name);
        Ganger UpdateGanger(Ganger ganger);
        Ganger IncreaseStat(Ganger ganger, GangerStatistics stat, int? interval);
        Ganger AddGanger(Ganger ganger);
    }
}
