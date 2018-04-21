using Hivemind.Entities;
using Hivemind.Enums;
using Hivemind.Exceptions;
using Hivemind.Managers;
using Hivemind.Managers.Implementation;
using Hivemind.Providers;
using Hivemind.Utilities;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hivemind.Tests.Managers
{
    [TestFixture]
    public class GangerManagerTest
    {
        private IGangerManager _gangerManager;
        private Mock<IGangerProvider> _mockGangerProvider;
        private Mock<ISkillManager> _mockSkillManager;
        private Mock<IDiceRoller> _mockDiceRoller;

        [SetUp]
        public void SetUp()
        {
            _mockGangerProvider = new Mock<IGangerProvider>();
            _mockSkillManager = new Mock<ISkillManager>();
            _mockDiceRoller = new Mock<IDiceRoller>();

            _gangerManager = new GangerManager(_mockGangerProvider.Object, _mockSkillManager.Object, _mockDiceRoller.Object);
        }

        [Test]
        public void GetGangerTest()
        {
            var gangerId = "1";
            _gangerManager.GetGanger(gangerId);
            _mockGangerProvider.Verify(m => m.GetByGangerId(It.Is<string>(s => s == gangerId)), Times.Once);
        }

        [Test]
        public void UpdateGangerTest()
        {
            var ganger = new Ganger();
            _gangerManager.UpdateGanger(ganger);
            _mockGangerProvider.Verify(m => m.UpdateGanger(It.Is<Ganger>(g => g == ganger)), Times.Once);
        }

        [TestCase(GangerType.Juve, 4, 2, 2, 3, 3, 1, 3, 1, 6, 25, 0, GangerTitle.GreenJuve)]
        [TestCase(GangerType.Ganger, 4, 3, 3, 3, 3, 1, 3, 1, 7, 50, 20, GangerTitle.NewGanger)]
        [TestCase(GangerType.Heavy, 4, 3, 3, 3, 3, 1, 3, 1, 7, 60, 60, GangerTitle.GangChampion)]
        [TestCase(GangerType.Leader, 4, 4, 4, 3, 3, 1, 4, 1, 8, 120, 60, GangerTitle.GangChampion)]
        public void CreateGangerTest(GangerType type, int move, int ws, int bs, int strength, int toughness, int wounds, int initiative, int attack, int leadership, int cost, int startingExperience, GangerTitle title)
        {
            _mockDiceRoller.Setup(m => m.RollDie()).Returns(0);
            var ganger = _gangerManager.CreateGanger("Test Ganger", type);

            Assert.NotNull(ganger);
            Assert.AreEqual(type, ganger.GangerType);
            Assert.AreEqual(move, ganger.Move);
            Assert.AreEqual(ws, ganger.WeaponSkill);
            Assert.AreEqual(bs, ganger.BallisticSkill);
            Assert.AreEqual(strength, ganger.Strength);
            Assert.AreEqual(toughness, ganger.Toughness);
            Assert.AreEqual(wounds, ganger.Wounds);
            Assert.AreEqual(initiative, ganger.Initiative);
            Assert.AreEqual(attack, ganger.Attack);
            Assert.AreEqual(leadership, ganger.Leadership);
            Assert.AreEqual(cost, ganger.Cost);
            Assert.AreEqual(startingExperience, ganger.Experience);
            Assert.AreEqual(title, ganger.Title);
            Assert.IsTrue(ganger.Active);
        }

        [Test]
        public void InvalidCreateGangerTest()
        {
            Assert.Throws<HivemindException>(() => _gangerManager.CreateGanger("name", (GangerType)50));
        }

        [TestCase(GangerStatistics.Move)]
        [TestCase(GangerStatistics.WeaponSkill)]
        [TestCase(GangerStatistics.BallisticSkill)]
        [TestCase(GangerStatistics.Strength)]
        [TestCase(GangerStatistics.Toughness)]
        [TestCase(GangerStatistics.Wounds)]
        [TestCase(GangerStatistics.Attack)]
        [TestCase(GangerStatistics.Leadership)]
        [TestCase(GangerStatistics.Initiative)]
        public void IncreaseStatTest(GangerStatistics stat)
        {
            var ganger = new Ganger()
            {
                Move = 1,
                WeaponSkill = 1,
                BallisticSkill = 1,
                Strength = 1,
                Toughness = 1,
                Wounds = 1,
                Attack = 1,
                Leadership = 1,
                Initiative = 1,
            };

            var updated = _gangerManager.IncreaseStat(ganger, stat);

            var properties = updated.GetType().GetProperties();
            foreach (var prop in properties)
            {
                if (!Enum.TryParse<GangerStatistics>(prop.Name, out var result))
                {
                    continue;
                }

                if (prop.Name == Enum.GetName(typeof(GangerStatistics), stat))
                {
                    Assert.AreEqual(2, prop.GetValue(updated));
                }
                else
                {
                    Assert.AreEqual(1, prop.GetValue(updated));
                }
            }
        }

        [Test]
        public void AddGangerInjuryTest()
        {
            _gangerManager.AddGangerInjury("1", InjuryEnum.ArmWound);
            _mockGangerProvider.Verify(m => m.AddGangerInjury(It.IsAny<string>(), It.IsAny<InjuryEnum>()), Times.Once);
        }

        [Test]
        public void LearnSkillTest()
        {
            var ganger = new Ganger()
            {
                GangerId = "1"
            };

            var skill = new Skill()
            {
                SkillId = 1
            };

            _mockGangerProvider.Setup(gp => gp.CanLearnSkill(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(true);

            _mockSkillManager.Setup(sm => sm.GetRandomSkillByType(It.IsAny<SkillType>()))
                .Returns(skill);

            var gangerSkill = _gangerManager.LearnSkill(ganger, "advancementId", SkillType.Agility);

            _mockGangerProvider.Verify(gp => gp.AddGangerSkill(
                It.Is<string>(g => g == ganger.GangerId),
                It.Is<int>(s => s == skill.SkillId)), Times.Once);

            _mockGangerProvider.Verify(gp => gp.RemoveGangerAdvancement(
                It.Is<string>(g => g == ganger.GangerId),
                It.Is<string>(s => s == "advancementId")), Times.Once);

            Assert.IsTrue(ganger.Skills.Contains(skill));
            Assert.AreEqual(ganger.GangerId, gangerSkill.GangerId);
            Assert.AreEqual(skill.SkillId, gangerSkill.SkillId);
        }

        [Test]
        public void RegisterGangerAdvancementTest()
        {
            _gangerManager.RegisterGangerAdvancement("1");
            _mockGangerProvider.Verify(m => m.RegisterGangerAdvancement(It.IsAny<string>()), Times.Once);
        }
    }
}
