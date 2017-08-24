using Hivemind.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hivemind.Factories
{
    public interface ISkillFactory
    {
        Skill GetSkill(int skillId);
    }
}
