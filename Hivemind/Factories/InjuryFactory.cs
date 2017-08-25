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

namespace Hivemind.Factories
{
    public class InjuryFactory : IInjuryFactory
    {
        public object HiveMindException { get; private set; }
        private InjuryProvider _injuryProvider;

        public InjuryFactory(InjuryProvider injuryProvider)
        {
            _injuryProvider = injuryProvider ?? throw new ArgumentNullException(nameof(injuryProvider));
        }

        public Injury GetInjury(int injuryId)
        {
            var injury = _injuryProvider.GetInjuryById(injuryId);
            injury.InjuryEffect = GetInjuryEffect(injuryId);

            return injury;
        }

        public IEnumerable<Injury> GetAllInjuries()
        {
            return _injuryProvider.GetAllInjuries();
        }

        public IEnumerable<Injury> GetInjuriesByGangId(int gangId)
        {
            return _injuryProvider.GetInjuriesByGangId(gangId);
        }

        public Effect GetInjuryEffect(int injuryId)
        {
            switch ((InjuryEnum)injuryId)
            {
                case InjuryEnum.DEAD:
                    return HasDied;
                case InjuryEnum.MULTIPLE_INJURIES:
                    return NoStatsEffect;
                case InjuryEnum.CHEST_WOUND:
                    return HasChestWound;
                case InjuryEnum.LEG_WOUND:
                    return HasLegWound;
                case InjuryEnum.ARM_WOUND:
                    return HasArmWound;
                case InjuryEnum.HEAD_WOUND:
                    return HasHeadWound;
                case InjuryEnum.BLINDED_IN_ONE_EYE:
                    return HasBlindedInOneEye;
                case InjuryEnum.PARTIALLY_DEAFENED:
                    return HasPartiallyDeafened;
                case InjuryEnum.SHELL_SHOCK:
                    return HasShellShock;
                case InjuryEnum.HAND_INJURY:
                    return HasHandInjury;
                case InjuryEnum.OLD_BATTLE_WOUND:
                    return HasOldBattleWound;
                case InjuryEnum.FULL_RECOVERY:
                    return NoStatsEffect;
                case InjuryEnum.BITTER_ENMITY:
                    return HasBitterEnmity;
                case InjuryEnum.CAPTURED:
                    return IsCaptured;
                case InjuryEnum.HORRIBLE_SCARS:
                    return HasHorribleScars;
                case InjuryEnum.IMPRESSIVE_SCARS:
                    return HasImpressiveScars;
                case InjuryEnum.SURVIVES_AGAINST_THE_ODDS:
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
            if (DiceRoller.RollDie() >= 3)
            {
                // right hand
                ganger.RightHandFingers -= DiceRoller.RollDice(3, 1);
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
                ganger.LeftHandFingers -= DiceRoller.RollDice(3, 1);
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
            ganger.Experience += DiceRoller.RollDie();
            return ganger;
        }
    }
}
