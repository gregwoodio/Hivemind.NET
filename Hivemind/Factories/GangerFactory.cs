using Hivemind.Entities;
using Hivemind.Enums;
using Hivemind.Providers;
using Hivemind.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hivemind.Factories
{
    public class GangerFactory : IGangerFactory
    {
        public Ganger GetGanger(int id)
        {
            var provider = new GangerProvider();
            return provider.GetByGangerId(id);
        }

        public Ganger UpdateGanger(Ganger ganger)
        {
            var provider = new GangerProvider();
            return provider.UpdateGanger(ganger);
        }

        public Ganger CreateGanger(string name, GangerType type)
        {
            switch (type)
            {
                case GangerType.LEADER:
                    return CreateLeader(name);
                case GangerType.HEAVY:
                    return CreateHeavy(name);
                case GangerType.GANGER:
                    return CreateGanger(name);
                case GangerType.JUVE:
                    return CreateJuve(name);
                default:
                    return null;
            }
        }

        public Ganger CreateJuve(string name)
        {
            return new Ganger()
            {
                Name = name,
                Type = GangerType.JUVE,
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
                Title = GangerTitle.GREEN_JUVE,
                Active = true
            };
        }

        public Ganger CreateGanger(string name)
        {
            return new Ganger()
            {
                Name = name,
                Type = GangerType.GANGER,
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
                Title = GangerTitle.NEW_GANGER,
                Active = true
            };
        }

        public Ganger CreateHeavy(string name)
        {
            return new Ganger()
            {
                Name = name,
                Type = GangerType.HEAVY,
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
                Title = GangerTitle.GANG_CHAMPION,
                Active = true
            };
        }

        public Ganger CreateLeader(string name)
        {
            return new Ganger()
            {
                Name = name,
                Type = GangerType.LEADER,
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
                Title = GangerTitle.GANG_CHAMPION,
                Active = true
            };
        }
    }
}
