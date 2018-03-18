using Hivemind.Entities;
using Hivemind.Enums;
using Hivemind.Providers;
using Hivemind.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hivemind.Managers.Implementation
{
    public class SkillManager : ISkillManager
    {
        private SkillProvider _skillProvider;

        public SkillManager(SkillProvider skillProvider)
        {
            _skillProvider = skillProvider ?? throw new ArgumentNullException(nameof(skillProvider));
        }

        public Skill GetSkill(int skillId)
        {
            throw new NotImplementedException();
        }

        public Skill GetRandomSkillByType(SkillType type)
        {
            var skills = _skillProvider.GetSkillsByType(type).ToArray();

            return skills[DiceRoller.RollDie() - 1];
        }
    }
}
