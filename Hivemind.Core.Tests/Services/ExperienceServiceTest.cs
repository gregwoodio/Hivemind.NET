using Hivemind.Entities;
using Hivemind.Enums;
using Hivemind.Managers;
using Hivemind.Managers.Implementation;
using Hivemind.Services;
using Hivemind.Services.Implementation;
using Hivemind.Utilities;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hivemind.Tests.Services
{
    [TestFixture]
    public class ExperienceServiceTest
    {
        private IExperienceService _experienceService;
        private Mock<IGangerManager> _mockGangerManager;
        private Mock<IGangManager> _mockGangManager;
        private Mock<IDiceRoller> _mockDiceRoller;

        [SetUp]
        public void SetUp()
        {
            _mockGangerManager = new Mock<IGangerManager>();
            _mockGangManager = new Mock<IGangManager>();
            _mockDiceRoller = new Mock<IDiceRoller>();
            _experienceService = new ExperienceService(
                _mockGangerManager.Object,
                _mockGangManager.Object,
                _mockDiceRoller.Object);
        }
        
        [TestCase(0, 16, 3)]
        [TestCase(50, 10, 1)]
        [TestCase(40, 5, 1)]
        public void GetNumberOfAdvanceRollsTest(int startingExperience, int gainedExperience, int expectedAdvanceRolls)
        {
            var ganger = new Ganger()
            {
                Name = "TestGanger",
                Experience = startingExperience
            };

            var advanceRolls = _experienceService.GetNumberOfAdvanceRolls(ganger, gainedExperience);

            Assert.AreEqual(expectedAdvanceRolls, advanceRolls);
        }
        
        [TestCase(GangerType.Ganger, GameType.GangFight, true, true, 0)]
        [TestCase(GangerType.Juve, GameType.GangFight, true, true, 0)]
        [TestCase(GangerType.Heavy, GameType.GangFight, true, true, 0)]
        [TestCase(GangerType.Leader, GameType.GangFight, true, true, 10)]
        [TestCase(GangerType.Leader, GameType.GangFight, false, true, 0)]
        [TestCase(GangerType.Leader, GameType.Scavengers, true, true, 10)]
        [TestCase(GangerType.Leader, GameType.Scavengers, false, true, 0)]
        [TestCase(GangerType.Leader, GameType.Ambush, true, true, 10)]
        [TestCase(GangerType.Leader, GameType.Ambush, false, true, 0)]
        [TestCase(GangerType.Leader, GameType.RescueMission, true, true, 0)]
        [TestCase(GangerType.Leader, GameType.RescueMission, true, false, 10)]
        [TestCase(GangerType.Leader, GameType.RescueMission, false, false, 0)]
        [TestCase(GangerType.Leader, GameType.RescueMission, false, true, 0)]
        [TestCase(GangerType.Leader, GameType.Shootout, false, false, 0)]
        [TestCase(GangerType.Leader, GameType.TheRaid, false, false, 0)]
        public void GetLeaderBonusTest(GangerType type, GameType gameType, bool hasWon, bool isAttacker, int expectedValue)
        {
            var ganger = new Ganger()
            {
                GangerType = type
            };

            var result = _experienceService.GetLeaderBonus(ganger, gameType, hasWon, isAttacker);
            Assert.AreEqual(result, expectedValue);
        }

        [TestCase(0, 0)]
        [TestCase(1, 5)]
        [TestCase(2, 10)]
        public void GetWoundingHitBonusTest(int woundingHits, int expected)
        {
            var result = _experienceService.GetWoundingHitBonus(woundingHits);
            Assert.AreEqual(result, expected);
        }

        [TestCase(0, GameType.Scavengers, 0)]
        [TestCase(1, GameType.Scavengers, 1)]
        [TestCase(2, GameType.Scavengers, 2)]
        [TestCase(0, GameType.TheRaid, 0)]
        [TestCase(1, GameType.TheRaid, 5)]
        [TestCase(2, GameType.TheRaid, 10)]
        [TestCase(0, GameType.RescueMission, 0)]
        [TestCase(1, GameType.RescueMission, 5)]
        [TestCase(2, GameType.RescueMission, 10)]
        [TestCase(0, GameType.GangFight, 0)]
        [TestCase(1, GameType.GangFight, 0)]
        [TestCase(2, GameType.GangFight, 0)]
        [TestCase(0, GameType.Ambush, 0)]
        [TestCase(1, GameType.Ambush, 0)]
        [TestCase(2, GameType.Ambush, 0)]
        [TestCase(0, GameType.HitAndRun, 0)]
        [TestCase(1, GameType.HitAndRun, 0)]
        [TestCase(2, GameType.HitAndRun, 0)]
        public void GetObjectivesBonusTest(int objectives, GameType gameType, int expectedValue)
        {
            var result = _experienceService.GetObjectivesBonus(objectives, gameType);
            Assert.AreEqual(expectedValue, result);
        }

        [TestCase(true, GameType.HitAndRun, 10)]
        [TestCase(false, GameType.HitAndRun, 0)]
        [TestCase(true, GameType.GangFight, 0)]
        [TestCase(false, GameType.GangFight, 0)]
        [TestCase(true, GameType.Ambush, 0)]
        [TestCase(false, GameType.Ambush, 0)]
        [TestCase(true, GameType.RescueMission, 0)]
        [TestCase(false, GameType.RescueMission, 0)]
        [TestCase(true, GameType.Scavengers, 0)]
        [TestCase(false, GameType.Scavengers, 0)]
        [TestCase(true, GameType.Shootout, 0)]
        [TestCase(false, GameType.Shootout, 0)]
        [TestCase(true, GameType.TheRaid, 0)]
        [TestCase(false, GameType.TheRaid, 0)]
        public void GetWinningBonusTest(bool hasWon, GameType gameType, int expectedValue)
        {
            var result = _experienceService.GetWinningBonus(hasWon, gameType);
            Assert.AreEqual(expectedValue, result);
        }

        [TestCase]
        public void TestGetSurvivalBonus()
        {
            var result = _experienceService.GetSurvivalBonus();
            _mockDiceRoller.Verify(m => m.RollDie(), Times.Once);
        }

        [TestCase(GangerType.Juve, GangHouse.Cawdor, new[] { SkillType.Combat, SkillType.Ferocity })]
        [TestCase(GangerType.Ganger, GangHouse.Cawdor, new[] { SkillType.Combat, SkillType.Ferocity, SkillType.Agility })]
        [TestCase(GangerType.Heavy, GangHouse.Cawdor, new[] { SkillType.Ferocity, SkillType.Muscle, SkillType.Shooting, SkillType.Techno })]
        [TestCase(GangerType.Leader, GangHouse.Cawdor, new[] { SkillType.Agility, SkillType.Combat, SkillType.Ferocity, SkillType.Muscle, SkillType.Shooting, SkillType.Techno })]
        [TestCase(GangerType.Juve, GangHouse.Escher, new[] { SkillType.Agility, SkillType.Combat })]
        [TestCase(GangerType.Ganger, GangHouse.Escher, new[] { SkillType.Agility, SkillType.Combat, SkillType.Stealth })]
        [TestCase(GangerType.Heavy, GangHouse.Escher, new[] { SkillType.Agility, SkillType.Muscle, SkillType.Shooting, SkillType.Techno })]
        [TestCase(GangerType.Leader, GangHouse.Escher, new[] { SkillType.Agility, SkillType.Combat, SkillType.Ferocity, SkillType.Shooting, SkillType.Stealth, SkillType.Techno })]
        [TestCase(GangerType.Juve, GangHouse.Delaque, new[] { SkillType.Shooting, SkillType.Stealth })]
        [TestCase(GangerType.Ganger, GangHouse.Delaque, new[] { SkillType.Agility, SkillType.Shooting, SkillType.Stealth })]
        [TestCase(GangerType.Heavy, GangHouse.Delaque, new[] { SkillType.Muscle, SkillType.Stealth, SkillType.Shooting, SkillType.Techno })]
        [TestCase(GangerType.Leader, GangHouse.Delaque, new[] { SkillType.Agility, SkillType.Combat, SkillType.Ferocity, SkillType.Shooting, SkillType.Stealth, SkillType.Techno })]
        [TestCase(GangerType.Juve, GangHouse.Goliath, new[] { SkillType.Ferocity, SkillType.Muscle })]
        [TestCase(GangerType.Ganger, GangHouse.Goliath, new[] { SkillType.Ferocity, SkillType.Muscle, SkillType.Combat })]
        [TestCase(GangerType.Heavy, GangHouse.Goliath, new[] { SkillType.Muscle, SkillType.Combat, SkillType.Shooting, SkillType.Techno })]
        [TestCase(GangerType.Leader, GangHouse.Goliath, new[] { SkillType.Combat, SkillType.Ferocity, SkillType.Muscle, SkillType.Shooting, SkillType.Stealth, SkillType.Techno })]
        [TestCase(GangerType.Juve, GangHouse.Orlock, new[] { SkillType.Ferocity, SkillType.Shooting })]
        [TestCase(GangerType.Ganger, GangHouse.Orlock, new[] { SkillType.Combat, SkillType.Ferocity, SkillType.Shooting })]
        [TestCase(GangerType.Heavy, GangHouse.Orlock, new[] { SkillType.Combat, SkillType.Muscle, SkillType.Shooting, SkillType.Techno })]
        [TestCase(GangerType.Leader, GangHouse.Orlock, new[] { SkillType.Agility, SkillType.Combat, SkillType.Ferocity, SkillType.Shooting, SkillType.Stealth, SkillType.Techno })]
        [TestCase(GangerType.Juve, GangHouse.VanSaar, new[] { SkillType.Shooting, SkillType.Techno })]
        [TestCase(GangerType.Ganger, GangHouse.VanSaar, new[] { SkillType.Combat, SkillType.Shooting, SkillType.Techno })]
        [TestCase(GangerType.Heavy, GangHouse.VanSaar, new[] { SkillType.Combat, SkillType.Muscle, SkillType.Shooting, SkillType.Techno })]
        [TestCase(GangerType.Leader, GangHouse.VanSaar, new[] { SkillType.Agility, SkillType.Combat, SkillType.Ferocity, SkillType.Shooting, SkillType.Stealth, SkillType.Techno })]
        public void GetGangSkillTest(GangerType gangerType, GangHouse gangHouse, IEnumerable<SkillType> expectedSkillList)
        {
            var skillList = _experienceService.GetGangSkill(gangerType, gangHouse);
            Assert.AreEqual(expectedSkillList, skillList);
        }
    }
}
