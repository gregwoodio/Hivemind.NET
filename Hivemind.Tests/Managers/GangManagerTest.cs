using Hivemind.Entities;
using Hivemind.Enums;
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

        [Test]
        public void AddGangTest()
        {
            var gang = new Gang()
            {
                Name = "New Gang",
                Gangers = new[]
                {
                    new Ganger()
                    {
                        Name = "Ganger 1"
                    },
                    new Ganger()
                    {
                        Name = "Ganger 2"
                    },
                    new Ganger()
                    {
                        Name = "Ganger 3"
                    }
                }
            };

            _mockGangProvider.Setup(m => m.AddGang(It.IsAny<Gang>()))
                .Returns((Gang g) =>
                {
                    g.GangId = "AAAA-BBBB-CCCC";
                    return g;
                });

            var addedGang = _gangManager.AddGang(gang);
            _mockGangProvider.Verify(m => m.AddGang(gang), Times.Once);
            Assert.AreEqual("AAAA-BBBB-CCCC", addedGang.GangId);
            _mockGangerProvider.Verify(m => m.AddGanger(It.IsAny<Ganger>()), Times.Exactly(3));
            _mockTerritoryProvider.Verify(m => m.AddGangTerritory(It.Is<GangTerritory>(gt => gt.GangId == gang.GangId)), Times.Exactly(5));
        }

        [Test]
        public void GetGangTest()
        {
            var gangId = "Gang1";

            var sampleGang = new Gang()
            {
                Name = "TestGang",
                GangId = gangId,
                GangHouse = GangHouse.Cawdor
            };

            var weapons = new[]
            {
                new GangerWeapon()
                {
                    GangerId = "1",
                    Cost = 10,
                    GangerWeaponId = "A",
                },
                new GangerWeapon()
                {
                    GangerId = "1",
                    Cost = 25,
                    GangerWeaponId = "B",
                },
                new GangerWeapon()
                {
                    GangerId = "2",
                    Cost = 30,
                    GangerWeaponId = "C",
                },
                new GangerWeapon()
                {
                    GangerId = "4",
                    Cost = 15,
                    GangerWeaponId = "D",
                }
            };

            var gangers = new[]
            {
                new Ganger()
                {
                    GangerId = "1",
                    Active = true,
                    GangId = gangId,
                    GangerType = GangerType.Ganger,
                },
                new Ganger()
                {
                    GangerId = "2",
                    Active = true,
                    GangId = gangId,
                    GangerType = GangerType.Ganger,
                },
                new Ganger()
                {
                    GangerId = "3",
                    Active = true,
                    GangId = gangId,
                    GangerType = GangerType.Ganger,
                },
                new Ganger()
                {
                    GangerId = "4",
                    Active = false,
                    GangId = gangId,
                    GangerType = GangerType.Ganger,
                }
            };

            var skills = new[]
            {
                new Skill()
                {
                    SkillId = 1,
                },
                new Skill()
                {
                    SkillId = 2,
                },
                new Skill()
                {
                    SkillId = 3,
                },
                new Skill()
                {
                    SkillId = 4
                }
            };

            var gangerSkills = new[]
            {
                new GangerSkill()
                {
                    GangerId = "1",
                    SkillId = 1,
                },
                new GangerSkill()
                {
                    GangerId = "2",
                    SkillId = 2,
                },
                new GangerSkill()
                {
                    GangerId = "4",
                    SkillId = 4,
                }
            };

            var gangerInjuries = new[]
            {
                new GangerInjury()
                {
                    GangerId = "1",
                    Injury = new Injury()
                    {
                        InjuryId = (InjuryEnum)1
                    }
                },
                new GangerInjury()
                {
                    GangerId = "2",
                    Injury = new Injury()
                    {
                        InjuryId = (InjuryEnum)2
                    }
                },
            };

            _mockGangProvider.Setup(m => m.GetGangById(It.IsAny<string>()))
                .Returns(sampleGang);

            _mockGangerProvider.Setup(m => m.GetByGangId(It.IsAny<string>()))
                .Returns(gangers);

            _mockWeaponProvider.Setup(m => m.GetByGangId(It.IsAny<string>()))
                .Returns(weapons);

            _mockSkillProvider.Setup(m => m.GetAllSkills())
                .Returns(skills);

            _mockGangerProvider.Setup(m => m.GetGangerSkills(It.IsAny<string>()))
                .Returns(gangerSkills);

            _mockInjuryProvider.Setup(m => m.GetInjuriesByGangId(It.IsAny<string>()))
                .Returns(gangerInjuries);

            var gang = _gangManager.GetGang(gangId);

            Assert.AreEqual(3, gang.Gangers.Count());
            Assert.IsFalse(gang.Gangers.Any(g => g.GangerId == "4"));

            //weapons
            Assert.IsTrue(gang.Gangers.Where(g => g.GangerId == "1").First().Weapons.Any(g => g.GangerWeaponId == "A"));
            Assert.IsTrue(gang.Gangers.Where(g => g.GangerId == "1").First().Weapons.Any(g => g.GangerWeaponId == "B"));
            Assert.IsTrue(gang.Gangers.Where(g => g.GangerId == "2").First().Weapons.Any(g => g.GangerWeaponId == "C"));
            Assert.IsFalse(gang.Gangers.Where(g => g.GangerId == "3").First().Weapons.Any());

            //skills
            Assert.IsTrue(gang.Gangers.Where(g => g.GangerId == "1").First().Skills.Any(s => s.SkillId == 1));
            Assert.IsTrue(gang.Gangers.Where(g => g.GangerId == "2").First().Skills.Any(s => s.SkillId == 2));
            Assert.IsFalse(gang.Gangers.Where(g => g.GangerId == "3").First().Skills.Any());

            //injuries
            Assert.IsTrue(gang.Gangers.Where(g => g.GangerId == "1").First().Injuries.Any(i => i.InjuryId == (InjuryEnum)1));
            Assert.IsTrue(gang.Gangers.Where(g => g.GangerId == "2").First().Injuries.Any(i => i.InjuryId == (InjuryEnum)2));
            Assert.IsFalse(gang.Gangers.Where(g => g.GangerId == "3").First().Injuries.Any());
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

        [Test]
        public void AssociateGangToUserTest()
        {
            var gangId = "gangId";
            var userId = "userId";
            _gangManager.AssociateGangToUser(gangId, userId);
            _mockGangProvider.Verify(m => m.AssociateGangToUser(
                It.Is<string>(g => g == gangId), 
                It.Is<string>(u => u == userId)
            ), Times.Once);
        }
    }
}
