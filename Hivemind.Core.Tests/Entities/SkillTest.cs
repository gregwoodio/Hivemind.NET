using Hivemind.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hivemind.Tests.Entities
{
    [TestFixture]
    public class SkillTest
    {
        [TestCase]
        public void EqualsTest()
        {
            var skill1 = new Skill()
            {
                SkillId = 1
            };

            var skill2 = new Skill()
            {
                SkillId = 1
            };

            Assert.IsTrue(skill1.Equals(skill2));

            Assert.IsFalse(skill1.Equals(new object()));
        }
    }
}
