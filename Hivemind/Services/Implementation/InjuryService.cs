using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hivemind.Contracts;
using Hivemind.Providers;
using Hivemind.Utilities;
using Hivemind.Entities;
using Hivemind.Managers;
using Hivemind.Enums;
using Hivemind.Exceptions;

namespace Hivemind.Services.Implementation
{
    public class InjuryService : IInjuryService
    {
        private IGangerManager _gangerManager;
        private IInjuryManager _injuryManager;

        public InjuryService(IGangerManager gangerManager, IInjuryManager injuryManager)
        {
            _gangerManager = gangerManager ?? throw new ArgumentNullException(nameof(_gangerManager));
            _injuryManager = injuryManager ?? throw new ArgumentNullException(nameof(injuryManager));
        }

        public InjuryReport ProcessInjuries(BattleReport battleReport)
        {
            // If a ganger is down, a roll less than three means they're injured.
            var injuries = new List<GangerInjuryReport>();
            foreach (var stats in battleReport.GangBattleStats)
            {
                if (stats.OutOfAction || (stats.Down && DiceRoller.RollDie() <= 3))
                {
                    injuries.Add(new GangerInjuryReport()
                    {
                        TheGanger = _gangerManager.GetGanger(stats.GangerId),
                        Injuries = DetermineInjury(null)
                    });
                }
            }
            //var injuries = battleReport.GangBattleStats
            //    .Where(stats => stats.OutOfAction || (stats.Down && DiceRoller.RollDie() <= 3))
            //    .Select(ganger => _gangerManager.GetGanger(ganger.GangerId))
            //    .Select(ganger => new GangerInjuryReport() { TheGanger = ganger, Injuries = DetermineInjury(null) })
            //    .ToList();

            // apply injuries to Gangers
            var injuredGangers = new List<GangerInjuryReport>();
            for (int i = 0; i < injuries.Count(); i++)
            {
                var ganger = injuries[i].TheGanger;
                foreach (var injury in injuries[i].Injuries)
                {
                    ganger = injury.InjuryEffect(ganger);
                }
                injuredGangers.Add(new GangerInjuryReport()
                {
                    Injuries = injuries[i].Injuries,
                    TheGanger = ganger
                });
            }

            // update gangers in db
            foreach (var report in injuredGangers)
            {
                _gangerManager.UpdateGanger(report.TheGanger);
                foreach (var injury in report.Injuries)
                {
                    if (injury.InjuryEnum != InjuryEnum.FullRecovery)
                    {
                        _gangerManager.AddGangerInjury(report.TheGanger.GangerId, injury.InjuryEnum);
                    }
                }
            }

            return new InjuryReport()
            {
                Injuries = injuries
            };
        }

        public IEnumerable<Injury> DetermineInjury(int? roll)
        {
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
                    return new[] { _injuryManager.GetInjury((int)InjuryEnum.Dead) };
                case 21:
                    var injuries = new List<Injury>();
                    injuries.Add(_injuryManager.GetInjury((int)InjuryEnum.MultipleInjuries));
                    var extraInjuries = DiceRoller.RollDie();
                    for (int i = 0; i < extraInjuries; i++)
                    {
                        injuries.Add(DetermineInjury(DiceRoller.MultipleInjuriesRoll()).FirstOrDefault());
                    }
                    return injuries;
                case 22:
                    return new[] { _injuryManager.GetInjury((int)InjuryEnum.ChestWound) };
                case 23:
                    return new[] { _injuryManager.GetInjury((int)InjuryEnum.LegWound) };
                case 24:
                    return new[] { _injuryManager.GetInjury((int)InjuryEnum.ArmWound) };
                case 25:
                    return new[] { _injuryManager.GetInjury((int)InjuryEnum.HeadWound) };
                case 26:
                    return new[] { _injuryManager.GetInjury((int)InjuryEnum.BlindedInOneEye) };
                case 31:
                    return new[] { _injuryManager.GetInjury((int)InjuryEnum.PartiallyDeafened) };
                case 32:
                    return new[] { _injuryManager.GetInjury((int)InjuryEnum.ShellShock) };
                case 33:
                    return new[] { _injuryManager.GetInjury((int)InjuryEnum.HandInjury) };
                case 34:
                case 35:
                case 36:
                    return new[] { _injuryManager.GetInjury((int)InjuryEnum.OldBattleWound) };
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
                    return new[] { _injuryManager.GetInjury((int)InjuryEnum.FullRecovery) };
                case 56:
                    return new[] { _injuryManager.GetInjury((int)InjuryEnum.BitterEnmity) };
                case 61:
                case 62:
                case 63:
                    return new[] { _injuryManager.GetInjury((int)InjuryEnum.Captured) };
                case 64:
                    return new[] { _injuryManager.GetInjury((int)InjuryEnum.HorribleScars) };
                case 65:
                    return new[] { _injuryManager.GetInjury((int)InjuryEnum.ImpressiveScars) };
                case 66:
                    return new[] { _injuryManager.GetInjury((int)InjuryEnum.SurvivesAgainstTheOdds) };
                default:
                    HivemindException.NoSuchInjuryException(roll.Value);
                    return new Injury[] { };
            }
        }
    }
}
