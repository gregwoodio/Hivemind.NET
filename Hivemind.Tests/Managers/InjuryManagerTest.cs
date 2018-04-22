using Hivemind.Entities;
using Hivemind.Enums;
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
    public class InjuryManagerTest
    {
        private Ganger _ganger;
        private IInjuryManager _injuryManager;
        private Mock<IInjuryProvider> _mockInjuryProvider;
        private Mock<IDiceRoller> _mockDiceRoller;

        [SetUp]
        public void Setup()
        {
            _ganger = new Ganger()
            {
                GangerId = "1",
                Name = "Ganger",
                GangId = "Gang1",
                Move = 4,
                WeaponSkill = 3,
                BallisticSkill = 3,
                Strength = 3,
                Toughness = 3,
                Wounds = 1,
                Attack = 1,
                Initiative = 7,
                Leadership = 7,
                Active = true,
                Experience = 20,
                Cost = 50,
                GangerType = GangerType.Ganger,
            };

            _mockInjuryProvider = new Mock<IInjuryProvider>();
            _mockDiceRoller = new Mock<IDiceRoller>();
            _injuryManager = new InjuryManager(_mockInjuryProvider.Object, _mockDiceRoller.Object);

            _mockInjuryProvider.Setup(m => m.GetInjuryById(It.IsAny<int>()))
                .Returns(new Injury());
        }

        [Test]
        public void NoStatsEffectTest()
        {
            var injury = _injuryManager.GetInjury((int)InjuryEnum.FullRecovery);

            var injuredGanger = injury.InjuryEffect(_ganger);
            DoAssertions(injuredGanger, 4, 3, 3, 3, 3, 1, 1, 7, 7, true);
        }
        
        [TestCase(InjuryEnum.Dead, 4, 3, 3, 3, 3, 1, 1, 7, 7, false)]
        [TestCase(InjuryEnum.ChestWound, 4, 3, 3, 3, 2, 1, 1, 7, 7, true)]
        [TestCase(InjuryEnum.LegWound, 3, 3, 3, 3, 3, 1, 1, 7, 7, true)]
        [TestCase(InjuryEnum.ArmWound, 4, 3, 3, 2, 3, 1, 1, 7, 7, true)]
        [TestCase(InjuryEnum.ShellShock, 4, 3, 3, 3, 3, 1, 1, 6, 7, true)]
        public void InjuryEffectsTest(InjuryEnum injuryId, int move, int ws, int bs, int strength, int toughness, int wounds, int attack, int initiative, int leadership, bool active)
        {
            var injury = _injuryManager.GetInjury((int)injuryId);

            var injuredGanger = injury.InjuryEffect(_ganger);
            DoAssertions(injuredGanger, move, ws, bs, strength, toughness, wounds, attack, initiative, leadership, active);
        }

        [Test]
        public void HeadWoundTest()
        {
            var injury = _injuryManager.GetInjury((int)InjuryEnum.HeadWound);

            var injuredGanger = injury.InjuryEffect(_ganger);
            DoAssertions(injuredGanger, 4, 3, 3, 3, 3, 1, 1, 7, 7, true);
            Assert.IsTrue(injuredGanger.HasHeadWound);
        }

        [Test]
        public void BlindedTest()
        {
            var injury = _injuryManager.GetInjury((int)InjuryEnum.BlindedInOneEye);

            var injuredGanger = injury.InjuryEffect(_ganger);
            DoAssertions(injuredGanger, 4, 3, 2, 3, 3, 1, 1, 7, 7, true);
            Assert.IsTrue(injuredGanger.IsOneEyed);

            var injuredAgainGanger = injury.InjuryEffect(injuredGanger);
            DoAssertions(injuredAgainGanger, 4, 3, 2, 3, 3, 1, 1, 7, 7, false);
        }

        [Test]
        public void DeafenedTest()
        {
            var injury = _injuryManager.GetInjury((int)InjuryEnum.PartiallyDeafened);

            var injuredGanger = injury.InjuryEffect(_ganger);
            DoAssertions(injuredGanger, 4, 3, 3, 3, 3, 1, 1, 7, 7, true);
            Assert.IsTrue(injuredGanger.IsDeafened);

            var injuredAgainGanger = injury.InjuryEffect(injuredGanger);
            DoAssertions(injuredAgainGanger, 4, 3, 3, 3, 3, 1, 1, 7, 6, true);
        }

        [TestCase(1)]
        [TestCase(6)]
        public void FingersTest(int roll)
        {
            _mockDiceRoller.Setup(m => m.RollDie()).Returns(roll);
            _mockDiceRoller.Setup(m => m.RollDice(3, 1)).Returns(5);

            var injury = _injuryManager.GetInjury((int)InjuryEnum.HandInjury);

            var injuredGanger = injury.InjuryEffect(_ganger);
            DoAssertions(injuredGanger, 4, 2, 3, 3, 3, 1, 1, 7, 7, true);
            Assert.IsTrue(injuredGanger.IsOneHanded);

            var injuredAgainGanger = injury.InjuryEffect(injuredGanger);
            DoAssertions(injuredGanger, 4, 1, 3, 3, 3, 1, 1, 7, 7, false);
        }

        [Test]
        public void BitterEnmityTest()
        {
            var injury = _injuryManager.GetInjury((int)InjuryEnum.BitterEnmity);

            var injuredGanger = injury.InjuryEffect(_ganger);
            DoAssertions(injuredGanger, 4, 3, 3, 3, 3, 1, 1, 7, 7, true);
            Assert.IsTrue(injuredGanger.HasBitterEnmity);
        }

        [Test]
        public void HorribleScarsTest()
        {
            var injury = _injuryManager.GetInjury((int)InjuryEnum.HorribleScars);

            var injuredGanger = injury.InjuryEffect(_ganger);
            DoAssertions(injuredGanger, 4, 3, 3, 3, 3, 1, 1, 7, 7, true);
            Assert.IsTrue(injuredGanger.HasHorribleScars);
        }

        [Test]
        public void ImpressiveScarsTest()
        {
            var injury = _injuryManager.GetInjury((int)InjuryEnum.ImpressiveScars);

            var injuredGanger = injury.InjuryEffect(_ganger);
            DoAssertions(injuredGanger, 4, 3, 3, 3, 3, 1, 1, 7, 8, true);
            Assert.IsTrue(injuredGanger.HasImpressiveScars);

            var injuredAgainGanger = injury.InjuryEffect(injuredGanger);
            DoAssertions(injuredGanger, 4, 3, 3, 3, 3, 1, 1, 7, 8, true);
        }

        [Test]
        public void CapturedTest()
        {
            var injury = _injuryManager.GetInjury((int)InjuryEnum.Captured);

            var injuredGanger = injury.InjuryEffect(_ganger);
            DoAssertions(injuredGanger, 4, 3, 3, 3, 3, 1, 1, 7, 7, true);
            Assert.IsTrue(injuredGanger.IsCaptured);
        }

        [Test]
        public void GetAllInjuriesTest()
        {
            _injuryManager.GetAllInjuries();
            _mockInjuryProvider.Verify(m => m.GetAllInjuries(), Times.Once);
        }

        [Test]
        public void GetAllInjuriesByGangIdTest()
        {
            var gangId = "1";
            _injuryManager.GetInjuriesByGangId(gangId);
            _mockInjuryProvider.Verify(m => m.GetInjuriesByGangId(gangId), Times.Once);
        }

        private void DoAssertions(Ganger ganger, int move, int ws, int bs, int strength, int toughness, int wounds, int attack, int initiative, int leadership, bool active)
        {
            Assert.AreEqual(move, ganger.Move);
            Assert.AreEqual(ws, ganger.WeaponSkill);
            Assert.AreEqual(bs, ganger.BallisticSkill);
            Assert.AreEqual(strength, ganger.Strength);
            Assert.AreEqual(toughness, ganger.Toughness);
            Assert.AreEqual(wounds, ganger.Wounds);
            Assert.AreEqual(attack, ganger.Attack);
            Assert.AreEqual(initiative, ganger.Initiative);
            Assert.AreEqual(leadership, ganger.Leadership);
            Assert.AreEqual(active, ganger.Active);
        }
    }
}
