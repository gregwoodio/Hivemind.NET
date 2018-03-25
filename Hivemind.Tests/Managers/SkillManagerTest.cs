using Hivemind.Enums;
using Hivemind.Managers;
using Microsoft.Practices.Unity;
using NUnit.Framework;

namespace Hivemind.Tests.Managers
{
    [TestFixture]
    class SkillManagerTest
    {
        private UnityContainer _container;
        private ISkillManager _skillManager;

        public SkillManagerTest()
        {
            _container = Container.GetContainer();
        }

        [SetUp]
        public void Setup()
        {
            _skillManager = _container.Resolve<ISkillManager>();
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
            var skill = _skillManager.GetRandomSkillByType(skillType);
            Assert.AreEqual(skillType, skill.SkillType);
        }
    }
}
