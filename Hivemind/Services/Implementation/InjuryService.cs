// <copyright file="InjuryService.cs" company="weirdvector">
// Copyright (c) weirdvector. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using Hivemind.Contracts;
using Hivemind.Entities;
using Hivemind.Enums;
using Hivemind.Exceptions;
using Hivemind.Managers;
using Hivemind.Utilities;

namespace Hivemind.Services.Implementation
{
    /// <summary>
    /// Injury service implementation
    /// </summary>
    public class InjuryService : IInjuryService
    {
        private IGangerManager _gangerManager;
        private IInjuryManager _injuryManager;
        private IDiceRoller _diceRoller;

        /// <summary>
        /// Initializes a new instance of the <see cref="InjuryService"/> class.
        /// </summary>
        /// <param name="gangerManager">Ganger manager</param>
        /// <param name="injuryManager">Injury manager</param>
        /// <param name="diceRoller">Dice roller</param>
        public InjuryService(IGangerManager gangerManager, IInjuryManager injuryManager, IDiceRoller diceRoller)
        {
            _gangerManager = gangerManager ?? throw new ArgumentNullException(nameof(_gangerManager));
            _injuryManager = injuryManager ?? throw new ArgumentNullException(nameof(injuryManager));
            _diceRoller = diceRoller ?? throw new ArgumentNullException(nameof(diceRoller));
        }

        /// <summary>
        /// Process injuries
        /// </summary>
        /// <param name="battleReport">Battle report</param>
        /// <returns>Injury report</returns>
        public InjuryReport ProcessInjuries(BattleReport battleReport)
        {
            // If a ganger is down, a roll less than three means they're injured.
            var injuries = new List<GangerInjuryReport>();
            foreach (var stats in battleReport.GangBattleStats)
            {
                if (stats.OutOfAction || (stats.Down && _diceRoller.RollDie() <= 3))
                {
                    injuries.Add(new GangerInjuryReport()
                    {
                        TheGanger = _gangerManager.GetGanger(stats.GangerId),
                        Injuries = DetermineInjury(null),
                    });
                }
            }

            // var injuries = battleReport.GangBattleStats
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
                    TheGanger = ganger,
                });
            }

            // update gangers in db
            foreach (var report in injuredGangers)
            {
                _gangerManager.UpdateGanger(report.TheGanger);
                foreach (var injury in report.Injuries)
                {
                    if (injury.InjuryId != InjuryEnum.FullRecovery)
                    {
                        _gangerManager.AddGangerInjury(report.TheGanger.GangerId, injury.InjuryId);
                    }
                }
            }

            return new InjuryReport()
            {
                Injuries = injuries,
            };
        }

        /// <summary>
        /// Determine injury
        /// </summary>
        /// <param name="roll">Dice roll</param>
        /// <returns>List of injuries</returns>
        public IEnumerable<Injury> DetermineInjury(int? roll)
        {
            if (!roll.HasValue)
            {
                roll = _diceRoller.RollD66();
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
                    var extraInjuries = _diceRoller.RollDie();
                    for (int i = 0; i < extraInjuries; i++)
                    {
                        injuries.Add(DetermineInjury(_diceRoller.MultipleInjuriesRoll()).FirstOrDefault());
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
