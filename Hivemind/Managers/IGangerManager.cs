using Hivemind.Entities;
using Hivemind.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hivemind.Managers
{
    public interface IGangerManager
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
        void AddGangerInjury(string gangerId, InjuryEnum injuryEnum);
        Ganger LearnSkill(Ganger ganger, string advancementId, SkillType type);
        string RegisterGangerAdvancement(string gangerId);
    }
}
