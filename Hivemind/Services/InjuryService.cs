using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hivemind.Contracts;
using Hivemind.Providers;
using Hivemind.Utilities;
using Hivemind.Entities;
using Hivemind.Factories;
using Hivemind.Enums;
using Hivemind.Exceptions;

namespace Hivemind.Services
{
    public class InjuryService : IInjuryService
    {
        public InjuryReport ProcessInjuries(BattleReport battleReport)
        {
            //TODO: use injection
            var gangerFactory = new GangerFactory();

            // If a ganger is down, a roll less than three means they're injured.
            var injuries = battleReport.GangBattleStats
                .Where(stats => stats.OutOfAction || (stats.Down && DiceRoller.RollDie() <= 3))
                .Select(ganger => gangerFactory.GetGanger(ganger.GangerId))
                .Select(ganger => new GangerInjuryReport() { TheGanger = ganger, Injuries = DetermineInjury(null) });

            // apply injuries to Gangers
            var injuredGangers = new List<GangerInjuryReport>();
            for (int i = 0; i < injuries.Count(); i++)
            {
                var ganger = injuries.ElementAt(i).TheGanger;
                foreach (var injury in injuries.ElementAt(i).Injuries)
                {
                    ganger = injury.InjuryEffect(ganger);
                }
                injuredGangers.Add(new GangerInjuryReport()
                {
                    Injuries = injuries.ElementAt(i).Injuries,
                    TheGanger = ganger
                });
            }

            // update gangers in db
            foreach (var report in injuredGangers)
            {
                gangerFactory.UpdateGanger(report.TheGanger);
            }

            return new InjuryReport()
            {
                Injuries = injuries
            };
        }

        public IEnumerable<Injury> DetermineInjury(int? roll)
        {
            //TODO: use injection
            var factory = new InjuryFactory();
            if (!roll.HasValue)
            {
                roll = DiceRoller.RollD66();
            }

            switch (roll.Value)
            {
                case 11:
                case 12:
                case 13:
                case 14:
                case 15:
                case 16:
                    return new[] { factory.GetInjury((int)InjuryEnum.DEAD) };
                case 21:
                    var injuries = new List<Injury>();
                    injuries.Add(factory.GetInjury((int)InjuryEnum.MULTIPLE_INJURIES));
                    var extraInjuries = DiceRoller.RollDie();
                    for (int i = 0; i < extraInjuries; i++)
                    {
                        injuries.Add(DetermineInjury(DiceRoller.MultipleInjuriesRoll()).FirstOrDefault());
                    }
                    return injuries;
                case 22:
                    return new[] { factory.GetInjury((int)InjuryEnum.CHEST_WOUND) };
                case 23:
                    return new[] { factory.GetInjury((int)InjuryEnum.LEG_WOUND) };
                case 24:
                    return new[] { factory.GetInjury((int)InjuryEnum.ARM_WOUND) };
                case 25:
                    return new[] { factory.GetInjury((int)InjuryEnum.HEAD_WOUND) };
                case 26:
                    return new[] { factory.GetInjury((int)InjuryEnum.BLINDED_IN_ONE_EYE) };
                case 31:
                    return new[] { factory.GetInjury((int)InjuryEnum.PARTIALLY_DEAFENED) };
                case 32:
                    return new[] { factory.GetInjury((int)InjuryEnum.SHELL_SHOCK) };
                case 33:
                    return new[] { factory.GetInjury((int)InjuryEnum.HAND_INJURY) };
                case 34:
                case 35:
                case 36:
                    return new[] { factory.GetInjury((int)InjuryEnum.OLD_BATTLE_WOUND) };
                case 41:
                case 42:
                case 43:
                case 44:
                case 45:
                case 46:
                case 51:
                case 52:
                case 53:
                case 54:
                case 55:
                    return new[] { factory.GetInjury((int)InjuryEnum.FULL_RECOVERY) };
                case 56:
                    return new[] { factory.GetInjury((int)InjuryEnum.BITTER_ENMITY) };
                case 61:
                case 62:
                case 63:
                    return new[] { factory.GetInjury((int)InjuryEnum.CAPTURED) };
                case 64:
                    return new[] { factory.GetInjury((int)InjuryEnum.HORRIBLE_SCARS) };
                case 65:
                    return new[] { factory.GetInjury((int)InjuryEnum.IMPRESSIVE_SCARS) };
                case 66:
                    return new[] { factory.GetInjury((int)InjuryEnum.SURVIVES_AGAINST_THE_ODDS) };
                default:
                    HivemindException.NoSuchInjuryException(roll.Value);
                    return new Injury[] { };
            }
        }
    }
}
