using Hivemind.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hivemind.Entities
{
    public class Skill
    {
        public int SkillId;
        public string Name;
        public string Description;
        public SkillType SkillType;
        public GangerType[] RestrictedTypes { get; set; }
    }
}
