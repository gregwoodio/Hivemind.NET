using Hivemind.Entities;
using Hivemind.Enums;
using Hivemind.Managers;
using Hivemind.Managers.Implementation;
using Hivemind.Providers;
using Hivemind.Utilities;
using Microsoft.Practices.Unity;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Hivemind.Tests.Managers
{
    [TestFixture]
    class SkillManagerTest
    {
        private ISkillManager _skillManager;
        private Mock<ISkillProvider> _mockSkillProvider;
        private Mock<IDiceRoller> _mockDiceRoller;

        private IEnumerable<Skill> _skills = new List<Skill>
        {
            new Skill()
            {
                SkillType = SkillType.Agility,
                SkillId = 1,
            },
            new Skill()
            {
                SkillType = SkillType.Combat,
                SkillId = 2,
            },
            new Skill()
            {
                SkillType = SkillType.Ferocity,
                SkillId = 3,
            },
            new Skill()
            {
                SkillType = SkillType.Muscle,
                SkillId = 4,
            },
            new Skill()
            {
                SkillType = SkillType.Shooting,
                SkillId = 5,
            },
            new Skill()
            {
                SkillType = SkillType.Stealth,
                SkillId = 6,
            },
            new Skill()
            {
                SkillType = SkillType.Techno,
                SkillId = 7,
            }
        };
        
        [SetUp]
        public void Setup()
        {
            _mockSkillProvider = new Mock<ISkillProvider>();
            _mockDiceRoller = new Mock<IDiceRoller>();
            _skillManager = new SkillManager(_mockSkillProvider.Object, _mockDiceRoller.Object);
        }

        [TestCase(SkillType.Agility)]
        [TestCase(SkillType.Combat)]
        [TestCase(SkillType.Ferocity)]
        [TestCase(SkillType.Muscle)]
        [TestCase(SkillType.Shooting)]
        [TestCase(SkillType.Stealth)]
        [TestCase(SkillType.Techno)]
        public void GetRandomSkillByTypeTest(SkillType skillType)
        {
            _mockSkillProvider.Setup(m => m.GetSkillsByType(It.IsAny<SkillType>()))
                .Returns((SkillType type) => _skills.Where(s => s.SkillType == type));
            _mockDiceRoller.Setup(m => m.RollDie()).Returns(1);

            var skill = _skillManager.GetRandomSkillByType(skillType);
            Assert.AreEqual(skillType, skill.SkillType);
        }
    }
}
