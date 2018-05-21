using System.Collections.Generic;

namespace Hivemind.Contracts
{
    public class GangSkillUpRequest
    {
        public IEnumerable<GangerSkillUpRequest> GangerSkillUpRequests { get; set; }

        public GangSkillUpRequest()
        {
            GangerSkillUpRequests = new List<GangerSkillUpRequest>();
        }
    }
}
