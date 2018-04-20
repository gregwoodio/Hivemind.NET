using Hivemind.Contracts;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hivemind.Tests.Contracts
{
    [TestFixture]
    public class GangSkillUpRequestTest
    {
        [Test]
        public void GangerSkillUpRequestsListsIsNotNullTest()
        {
            var request = new GangSkillUpRequest();

            Assert.NotNull(request.GangerSkillUpRequests);
        }
    }
}
