using Hivemind.Contracts;
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
    public class TerritoryManagerTest
    {
        private TerritoryManager _territoryManager;
        private Mock<IInjuryProvider> _injuryProviderMock;
        private IInjuryManager _injuryManager;
        private Mock<IGangerManager> _gangerManagerMock;
        private Mock<IDiceRoller> _diceRollerMock;

        [SetUp]
        public void Setup()
        {
            _diceRollerMock = new Mock<IDiceRoller>();
            _injuryProviderMock = new Mock<IInjuryProvider>();
            _injuryManager = new InjuryManager(_injuryProviderMock.Object, _diceRollerMock.Object);

            _injuryProviderMock.Setup(m => m.GetInjuryById(It.IsAny<int>()))
                .Returns((int id) => new Injury()
                {
                    Name = "Injury Name",
                    Description = "Injury Description",
                    InjuryId = (InjuryEnum)id,
                });

            _gangerManagerMock = new Mock<IGangerManager>();

            _territoryManager = new TerritoryManager(
                _injuryManager,
                _gangerManagerMock.Object,
                new TerritoryProviderMock(),
                _diceRollerMock.Object);
        }

        [Test]
        public void GetTerritoryTest()
        {
            var id = 1;
            var territory = _territoryManager.GetTerritory(id);
            Assert.AreEqual(id, territory.TerritoryId);
        }

        [Test]
        public void GetAllTerritoriesTest()
        {
            var territories = _territoryManager.GetAllTerritories();
            Assert.AreEqual(3, territories.Count());
            Assert.IsTrue(territories.Any(t => t.TerritoryId == 1));
            Assert.IsTrue(territories.Any(t => t.TerritoryId == 2));
            Assert.IsTrue(territories.Any(t => t.TerritoryId == 3));
        }

        [Test]
        public void GangTerritoryTest()
        {
            var gangId = "1";
            var gangTerritory = new GangTerritory()
            {
                GangId = gangId,
                Territory = new Territory()
                {
                    TerritoryId = 1
                }
            };

            Assert.IsTrue(_territoryManager.GetTerritoriesByGangId(gangId).Count() == 0);

            var addedTerritory = _territoryManager.AddGangTerritory(gangTerritory);
            Assert.IsTrue(_territoryManager.GetTerritoriesByGangId(gangId).Count() == 1);

            _territoryManager.RemoveGangTerritory(addedTerritory.GangTerritoryId);
            Assert.IsTrue(_territoryManager.GetTerritoriesByGangId(gangId).Count() == 0);
        }

        [Test]
        public void NoTerritoryEffectTest()
        {
            var effect = _territoryManager.GetTerritoryEffect((int)TerritoryEnum.OldRuins);

            var report = effect(new TerritoryWorkStatus()
            {
                TerritoryName = "Old Ruins",
                Roll = 6
            });

            Assert.AreEqual("Old Ruins", report.TerritoryName);
            Assert.AreEqual(6, report.Income);
        }

        [Test]
        public void ChemPitEffectTest()
        {
            var effect = _territoryManager.GetTerritoryEffect((int)TerritoryEnum.Chempit);

            var report = effect(new TerritoryWorkStatus()
            {
                TerritoryName = "Chempit",
                Roll = 6
            });

            Assert.AreEqual("Chempit", report.TerritoryName);
            Assert.AreEqual(6, report.Income);

            var ganger = new Ganger();
            report = effect(new TerritoryWorkStatus()
            {
                TerritoryName = "Chempit",
                Roll = 12,
                Ganger = ganger
            });

            Assert.AreEqual("Chempit", report.TerritoryName);
            Assert.AreEqual(0, report.Income);
            Assert.IsTrue(ganger.HasHorribleScars);
            Assert.NotNull(report.Description);
        }

        [Test]
        public void SettlementEffectTest()
        {
            var effect = _territoryManager.GetTerritoryEffect((int)TerritoryEnum.Settlement);
            _gangerManagerMock.Setup(m => m.CreateJuve(It.IsAny<string>())).Returns(new Ganger());
            _diceRollerMock.Setup(m => m.RollDie()).Returns(1);

            var report = effect(new TerritoryWorkStatus()
            {
                TerritoryName = "Settlement",
                Roll = 6
            });

            Assert.AreEqual("Settlement", report.TerritoryName);
            Assert.AreEqual(30, report.Income);

            _diceRollerMock.Setup(m => m.RollDie()).Returns(6);
            report = effect(new TerritoryWorkStatus()
            {
                TerritoryName = "Settlement",
                Roll = 6
            });

            Assert.AreEqual("Settlement", report.TerritoryName);
            Assert.NotNull(report.Description);
            Assert.AreEqual(30, report.Income);
            _gangerManagerMock.Verify(m => m.AddGanger(It.IsAny<Ganger>()), Times.Once);
        }

        [Test]
        public void MineWorkingsEffectTest()
        {
            var effect = _territoryManager.GetTerritoryEffect((int)TerritoryEnum.MineWorkings);

            var report = effect(new TerritoryWorkStatus()
            {
                TerritoryName = "Mine workings",
                Roll = 60,
            });

            Assert.AreEqual("Mine workings", report.TerritoryName);
            Assert.AreEqual(60, report.Income);
        }

        [Test]
        public void GamblingDenEffectTest()
        {
            var effect = _territoryManager.GetTerritoryEffect((int)TerritoryEnum.GamblingDen);

            _diceRollerMock.Setup(m => m.RollDie()).Returns(3);

            var report = effect(new TerritoryWorkStatus()
            {
                TerritoryName = "Gambling Den",
            });

            Assert.AreEqual("Gambling Den", report.TerritoryName);
            Assert.AreEqual(-60, report.Income);
            Assert.NotNull(report.Description);

            _diceRollerMock.SetupSequence(m => m.RollDie())
                .Returns(4)
                .Returns(2);

            report = effect(new TerritoryWorkStatus()
            {
                TerritoryName = "Gambling Den",
            });

            Assert.AreEqual("Gambling Den", report.TerritoryName);
            Assert.AreEqual(60, report.Income);
        }

        [Test]
        public void SporeCaveEffectTest()
        {
            var effect = _territoryManager.GetTerritoryEffect((int)TerritoryEnum.SporeCave);

            var ganger = new Ganger();
            var report = effect(new TerritoryWorkStatus()
            {
                Roll = 20,
                Ganger = ganger,
                TerritoryName = "Spore Cave"
            });

            Assert.AreEqual(20, report.Income);
            Assert.AreEqual("Spore Cave", report.TerritoryName);
            Assert.NotNull(report.Description);
            Assert.IsTrue(ganger.HasSporeSickness);

            report = effect(new TerritoryWorkStatus()
            {
                Roll = 30,
                TerritoryName = "Spore Cave"
            });

            Assert.AreEqual(30, report.Income);
            Assert.AreEqual("Spore Cave", report.TerritoryName);
        }

        [Test]
        public void GuilderContractEffectTest()
        {
            var effect = _territoryManager.GetTerritoryEffect((int)TerritoryEnum.GuilderContract);

            var report = effect(new TerritoryWorkStatus()
            {
                TerritoryName = "Guilder Contract",
                PreviousBattleType = GameType.GangFight,
                Objectives = 0,
                Roll = 60,
            });

            Assert.AreEqual("Guilder Contract", report.TerritoryName);
            Assert.AreEqual(60, report.Income);

            report = effect(new TerritoryWorkStatus()
            {
                TerritoryName = "Guilder Contract",
                PreviousBattleType = GameType.Scavengers,
                Objectives = 2,
                Roll = 60,
            });

            Assert.AreEqual("Guilder Contract", report.TerritoryName);
            Assert.AreEqual(70, report.Income);
            Assert.NotNull(report.Description);
        }

        [Test]
        public void FriendlyDocEffectTest()
        {
            var effect = _territoryManager.GetTerritoryEffect((int)TerritoryEnum.FriendlyDoc);
            _diceRollerMock.Setup(m => m.RollDie()).Returns(2);

            var report = effect(new TerritoryWorkStatus()
            {
                TerritoryName = "Friendly Doc",
                Roll = 60,
                Deaths = 1
            });

            Assert.AreEqual("Friendly Doc", report.TerritoryName);
            Assert.AreEqual(70, report.Income);
            Assert.NotNull(report.Description);

            report = effect(new TerritoryWorkStatus()
            {
                TerritoryName = "Friendly Doc",
                Roll = 60,
            });

            Assert.AreEqual("Friendly Doc", report.TerritoryName);
            Assert.AreEqual(60, report.Income);
        }
    }

    class TerritoryProviderMock : ITerritoryProvider
    {
        private List<Territory> _territories;
        private List<GangTerritory> _gangTerritory;

        public TerritoryProviderMock()
        {
            _territories = new List<Territory>()
            {
                new Territory()
                {
                    TerritoryId = 1
                },
                new Territory()
                {
                    TerritoryId = 2
                },
                new Territory()
                {
                    TerritoryId = 3
                }
            };

            _gangTerritory = new List<GangTerritory>();
        }

        public GangTerritory AddGangTerritory(GangTerritory gangTerritory)
        {
            gangTerritory.GangTerritoryId = "AAA-BBB-CCC";
            _gangTerritory.Add(gangTerritory);
            return gangTerritory;
        }

        public IEnumerable<Territory> GetAllTerritories()
        {
            return _territories;
        }

        public IEnumerable<Territory> GetTerritoryByGangId(string gangId)
        {
            return _gangTerritory
                .Where(gt => gt.GangId == gangId)
                .Select(gt => gt.Territory);
        }

        public Territory GetTerritoryById(TerritoryEnum territory)
        {
            throw new NotImplementedException();
        }

        public Territory GetTerritoryById(int territoryId)
        {
            return _territories.First(t => t.TerritoryId == territoryId);
        }

        public void RemoveGangTerritory(string gangTerritoryId)
        {
            var territory = _gangTerritory.Find(gt => gt.GangTerritoryId == gangTerritoryId);
            _gangTerritory.Remove(territory);
        }
    }
}
