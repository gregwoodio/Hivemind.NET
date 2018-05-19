using Hivemind.Enums;

namespace Hivemind.Contracts
{
    public class GangerSkillUpRequest
    {
        public string GangerId { get; set; }
        public string AdvancementId { get; set; }
        public SkillType SkillCategory { get; set; }
    }
}
