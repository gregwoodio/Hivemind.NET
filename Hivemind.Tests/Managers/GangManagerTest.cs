using Hivemind.Entities;
using Hivemind.Managers.Implementation;
using Hivemind.Providers;
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
    public class GangManagerTest
    {
        private GangManager _gangManager;
        private Mock<IGangProvider> _mockGangProvider;
        private Mock<IGangerProvider> _mockGangerProvider;
        private Mock<ISkillProvider> _mockSkillProvider;
        private Mock<ITerritoryProvider> _mockTerritoryProvider;
        private Mock<IWeaponProvider> _mockWeaponProvider;
        private Mock<IInjuryProvider> _mockInjuryProvider;

        [SetUp]
        public void Setup()
        {
            _mockGangProvider = new Mock<IGangProvider>();
            _mockGangerProvider = new Mock<IGangerProvider>();
            _mockSkillProvider = new Mock<ISkillProvider>();
            _mockTerritoryProvider = new Mock<ITerritoryProvider>();
            _mockWeaponProvider = new Mock<IWeaponProvider>();
            _mockInjuryProvider = new Mock<IInjuryProvider>();

            _gangManager = new GangManager(_mockGangProvider.Object,
                _mockGangerProvider.Object,
                _mockTerritoryProvider.Object,
                _mockWeaponProvider.Object,
                _mockInjuryProvider.Object,
                _mockSkillProvider.Object);
        }

        [TestCase(50, 10, 40, true)]
        [TestCase(50, 50, 0, true)]
        [TestCase(50, 90, 50, false)]
        public void SpendTest(int startingCredits, int spendingAmount, int finalCredits, bool expectSuccess)
        {
            var gangId = "1";
            var gang = new Gang()
            {
                GangId = gangId,
                Name = "TestGang",
                Credits = startingCredits
            };

            _mockGangProvider.Setup(m => m.GetGangById(It.IsAny<string>()))
                .Returns(gang);

            _mockGangProvider.Setup(m => m.UpdateGang(It.IsAny<Gang>()))
                .Returns((Gang g) => g);

            var wasSuccessful = _gangManager.Spend(gangId, spendingAmount);

            Assert.AreEqual(expectSuccess, wasSuccessful);
            Assert.AreEqual(finalCredits, gang.Credits);
        }

        [TestCase]
        public void SpendNegativeTest()
        {
            Assert.Throws<ArgumentException>(() => _gangManager.Spend("1", -123));
        }
    }
}
