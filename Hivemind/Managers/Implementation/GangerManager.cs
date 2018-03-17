using Hivemind.Entities;
using Hivemind.Enums;
using Hivemind.Exceptions;
using Hivemind.Providers;
using Hivemind.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hivemind.Managers.Implementation
{
    public class GangerManager : IGangerManager
    {
        private GangerProvider _gangerProvider;

        public GangerManager(GangerProvider gangerProvider)
        {
            _gangerProvider = gangerProvider ?? throw new ArgumentNullException(nameof(gangerProvider));
        }

        public Ganger GetGanger(string id)
        {
            return _gangerProvider.GetByGangerId(id);
        }

        public Ganger UpdateGanger(Ganger ganger)
        {
            return _gangerProvider.UpdateGanger(ganger);
        }

        public Ganger CreateGanger(string name, GangerType type)
        {
            switch (type)
            {
                case GangerType.Leader:
                    return CreateLeader(name);
                case GangerType.Heavy:
                    return CreateHeavy(name);
                case GangerType.Ganger:
                    return CreateGanger(name);
                case GangerType.Juve:
                    return CreateJuve(name);
                default:
                    throw new HivemindException($"Invalid GangerType provided: ${type}");
            }
        }

        public Ganger CreateJuve(string name)
        {
            return new Ganger()
            {
                Name = name,
                GangerType = GangerType.Juve,
                Move = 4,
                WeaponSkill = 2,
                BallisticSkill = 2,
                Strength = 3,
                Toughness = 3,
                Wounds = 1,
                Initiative = 3,
                Attack = 1,
                Leadership = 6,
                Cost = 25,
                Experience = 0,
                Title = GangerTitle.GreenJuve,
                Active = true
            };
        }

        public Ganger CreateGanger(string name)
        {
            return new Ganger()
            {
                Name = name,
                GangerType = GangerType.Ganger,
                Move = 4,
                WeaponSkill = 3,
                BallisticSkill = 3,
                Strength = 3,
                Toughness = 3,
                Wounds = 1,
                Initiative = 3,
                Attack = 1,
                Leadership = 7,
                Cost = 50,
                Experience = 20 + DiceRoller.RollDie(),
                Title = GangerTitle.NewGanger,
                Active = true
            };
        }

        public Ganger CreateHeavy(string name)
        {
            return new Ganger()
            {
                Name = name,
                GangerType = GangerType.Heavy,
                Move = 4,
                WeaponSkill = 3,
                BallisticSkill = 3,
                Strength = 3,
                Toughness = 3,
                Wounds = 1,
                Initiative = 3,
                Attack = 1,
                Leadership = 7,
                Cost = 60,
                Experience = 60 + DiceRoller.RollDie(),
                Title = GangerTitle.GangChampion,
                Active = true
            };
        }

        public Ganger CreateLeader(string name)
        {
            return new Ganger()
            {
                Name = name,
                GangerType = GangerType.Leader,
                Move = 4,
                WeaponSkill = 4,
                BallisticSkill = 4,
                Strength = 3,
                Toughness = 3,
                Wounds = 1,
                Initiative = 4,
                Attack = 1,
                Leadership = 8,
                Cost = 120,
                Experience = 60 + DiceRoller.RollDie(),
                Title = GangerTitle.GangChampion,
                Active = true
            };
        }

        public Ganger IncreaseStat(Ganger ganger, GangerStatistics stat, int? interval)
        {
            if (!interval.HasValue)
            {
                interval = 1;
            }

            switch (stat)
            {
                case GangerStatistics.Move:
                    ganger.Move += interval.Value;
                    break;
                case GangerStatistics.WeaponSkill:
                    ganger.WeaponSkill += interval.Value;
                    break;
                case GangerStatistics.BallisticSkill:
                    ganger.BallisticSkill += interval.Value;
                    break;
                case GangerStatistics.Strength:
                    ganger.Strength += interval.Value;
                    break;
                case GangerStatistics.Toughness:
                    ganger.Toughness += interval.Value;
                    break;
                case GangerStatistics.Attack:
                    ganger.Attack += interval.Value;
                    break;
                case GangerStatistics.Wounds:
                    ganger.Wounds += interval.Value;
                    break;
                case GangerStatistics.Initiative:
                    ganger.Initiative += interval.Value;
                    break;
                case GangerStatistics.Leadership:
                    ganger.Leadership += interval.Value;
                    break;
                default:
                    break;
            }
            UpdateGanger(ganger);
            return ganger;
        }

        public Ganger AddGanger(Ganger ganger)
        {
            var gangerWithStats = CreateGanger(ganger.Name, ganger.GangerType);
            gangerWithStats.GangId = ganger.GangId;
            return _gangerProvider.AddGanger(gangerWithStats);
        }

        public void AddGangerInjury(string gangerId, InjuryEnum injury)
        {
            _gangerProvider.AddGangerInjury(gangerId, injury);
        }
    }
}
