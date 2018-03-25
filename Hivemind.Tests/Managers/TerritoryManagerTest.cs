using Hivemind.Entities;
using Hivemind.Enums;
using Hivemind.Managers;
using Hivemind.Managers.Implementation;
using Hivemind.Providers;
using Microsoft.Practices.Unity;
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
        private UnityContainer _container;

        public TerritoryManagerTest()
        {
            _container = Container.GetContainer();

            _territoryManager = new TerritoryManager(
                _container.Resolve<IInjuryManager>(),
                _container.Resolve<IGangerManager>(),
                new TerritoryProviderMock());
        }

        [TestCase]
        public void GetTerritoryTest()
        {
            var id = 1;
            var territory = _territoryManager.GetTerritory(id);
            Assert.AreEqual(id, territory.TerritoryId);
        }

        [TestCase]
        public void GetAllTerritoriesTest()
        {
            var territories = _territoryManager.GetAllTerritories();
            Assert.AreEqual(3, territories.Count());
            Assert.IsTrue(territories.Any(t => t.TerritoryId == 1));
            Assert.IsTrue(territories.Any(t => t.TerritoryId == 2));
            Assert.IsTrue(territories.Any(t => t.TerritoryId == 3));
        }

        [TestCase]
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
