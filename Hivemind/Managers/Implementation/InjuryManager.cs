// <copyright file="InjuryManager.cs" company="weirdvector">
// Copyright (c) weirdvector. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using Hivemind.Entities;
using Hivemind.Enums;
using Hivemind.Exceptions;
using Hivemind.Providers;
using Hivemind.Utilities;

namespace Hivemind.Managers.Implementation
{
    /// <summary>
    /// Injury Manager
    /// </summary>
    public class InjuryManager : IInjuryManager
    {
        private IInjuryProvider _injuryProvider;
        private IDiceRoller _diceRoller;

        /// <summary>
        /// Initializes a new instance of the <see cref="InjuryManager"/> class.
        /// </summary>
        /// <param name="injuryProvider">Injury provider</param>
        public InjuryManager(IInjuryProvider injuryProvider, IDiceRoller diceRoller)
        {
            _injuryProvider = injuryProvider ?? throw new ArgumentNullException(nameof(injuryProvider));
            _diceRoller = diceRoller ?? throw new ArgumentNullException(nameof(diceRoller));
        }

        /// <summary>
        /// Gets the hivmeind exception
        /// </summary>
        public object HiveMindException { get; private set; }

        /// <summary>
        /// Get Injury by ID
        /// </summary>
        /// <param name="injuryId">Injury ID</param>
        /// <returns>Injury</returns>
        public Injury GetInjury(int injuryId)
        {
            var injury = _injuryProvider.GetInjuryById(injuryId);
            injury.InjuryEffect = GetInjuryEffect(injuryId);

            return injury;
        }

        /// <summary>
        /// Get all injuries
        /// </summary>
        /// <returns>Injuries</returns>
        public IEnumerable<Injury> GetAllInjuries()
        {
            return _injuryProvider.GetAllInjuries();
        }

        /// <summary>
        /// Get all injuries for a given gang
        /// </summary>
        /// <param name="gangId">Gang Id</param>
        /// <returns>Injuries</returns>
        public IEnumerable<GangerInjury> GetInjuriesByGangId(string gangId)
        {
            return _injuryProvider.GetInjuriesByGangId(gangId);
        }

        /// <summary>
        /// Gets the effect of a given injury
        /// </summary>
        /// <param name="injuryId">Injury ID</param>
        /// <returns>A function that alters a ganger based on injury criteria</returns>
        public Func<Ganger, Ganger> GetInjuryEffect(int injuryId)
        {
            switch ((InjuryEnum)injuryId)
            {
                case InjuryEnum.Dead:
                    return HasDied;
                case InjuryEnum.MultipleInjuries:
                    return NoStatsEffect;
                case InjuryEnum.ChestWound:
                    return HasChestWound;
                case InjuryEnum.LegWound:
                    return HasLegWound;
                case InjuryEnum.ArmWound:
                    return HasArmWound;
                case InjuryEnum.HeadWound:
                    return HasHeadWound;
                case InjuryEnum.BlindedInOneEye:
                    return HasBlindedInOneEye;
                case InjuryEnum.PartiallyDeafened:
                    return HasPartiallyDeafened;
                case InjuryEnum.ShellShock:
                    return HasShellShock;
                case InjuryEnum.HandInjury:
                    return HasHandInjury;
                case InjuryEnum.OldBattleWound:
                    return HasOldBattleWound;
                case InjuryEnum.FullRecovery:
                    return NoStatsEffect;
                case InjuryEnum.BitterEnmity:
                    return HasBitterEnmity;
                case InjuryEnum.Captured:
                    return IsCaptured;
                case InjuryEnum.HorribleScars:
                    return HasHorribleScars;
                case InjuryEnum.ImpressiveScars:
                    return HasImpressiveScars;
                case InjuryEnum.SurvivesAgainstTheOdds:
                    return HasSurvivedAgainstTheOdds;
            }

            HivemindException.NoSuchInjuryException(injuryId);
            return null;
        }

        private Ganger NoStatsEffect(Ganger ganger)
        {
            return ganger;
        }

        private Ganger HasDied(Ganger ganger)
        {
            ganger.Active = false;
            return ganger;
        }

        private Ganger HasChestWound(Ganger ganger)
        {
            ganger.Toughness -= 1;
            return ganger;
        }

        private Ganger HasLegWound(Ganger ganger)
        {
            ganger.Move -= 1;
            return ganger;
        }

        private Ganger HasArmWound(Ganger ganger)
        {
            ganger.Strength -= 1;
            return ganger;
        }

        private Ganger HasHeadWound(Ganger ganger)
        {
            ganger.HasHeadWound = true;
            return ganger;
        }

        private Ganger HasBlindedInOneEye(Ganger ganger)
        {
            if (ganger.IsOneEyed)
            {
                ganger.Active = false;
                return ganger;
            }

            ganger.BallisticSkill -= 1;
            ganger.IsOneEyed = true;
            return ganger;
        }

        private Ganger HasPartiallyDeafened(Ganger ganger)
        {
            if (ganger.IsDeafened)
            {
                ganger.Leadership -= 1;
                return ganger;
            }

            ganger.IsDeafened = true;
            return ganger;
        }

        private Ganger HasShellShock(Ganger ganger)
        {
            ganger.Initiative -= 1;
            return ganger;
        }

        private Ganger HasHandInjury(Ganger ganger)
        {
            ganger.WeaponSkill -= 1;
            if (_diceRoller.RollDie() >= 3)
            {
                // right hand
                ganger.RightHandFingers -= _diceRoller.RollDice(3, 1);
                if (ganger.RightHandFingers <= 0)
                {
                    if (ganger.IsOneHanded)
                    {
                        ganger.Active = false;
                        return ganger;
                    }

                    ganger.IsOneHanded = true;
                }

                return ganger;
            }
            else
            {
                ganger.LeftHandFingers -= _diceRoller.RollDice(3, 1);
                if (ganger.LeftHandFingers <= 0)
                {
                    if (ganger.IsOneHanded)
                    {
                        ganger.Active = false;
                        return ganger;
                    }

                    ganger.IsOneHanded = true;
                }

                return ganger;
            }
        }

        private Ganger HasOldBattleWound(Ganger ganger)
        {
            ganger.HasOldBattleWound = true;
            return ganger;
        }

        private Ganger HasBitterEnmity(Ganger ganger)
        {
            ganger.HasBitterEnmity = true;
            return ganger;
        }

        private Ganger HasHorribleScars(Ganger ganger)
        {
            ganger.HasHorribleScars = true;
            return ganger;
        }

        private Ganger HasImpressiveScars(Ganger ganger)
        {
            // should only apply once
            if (!ganger.HasImpressiveScars)
            {
                ganger.Leadership += 1;
            }

            ganger.HasImpressiveScars = true;
            return ganger;
        }

        private Ganger IsCaptured(Ganger ganger)
        {
            ganger.IsCaptured = true;
            return ganger;
        }

        private Ganger HasSurvivedAgainstTheOdds(Ganger ganger)
        {
            // TODO: Revisit this once experience is done
            ganger.Experience += _diceRoller.RollDie();
            return ganger;
        }
    }
}
