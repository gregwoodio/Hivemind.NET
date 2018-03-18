using Hivemind.Entities;
using Hivemind.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hivemind.Managers
{
    public interface ISkillManager
    {
        Skill GetSkill(int skillId);
        Skill GetRandomSkillByType(SkillType type);
    }
}
