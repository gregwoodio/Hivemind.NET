using Hivemind.Entities;
using Hivemind.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.Controllers
{
    [RoutePrefix("api/territories")]
    public class TerritoriesController : ApiController
    {
        private ITerritoryManager _territoryManager;

        public TerritoriesController(ITerritoryManager territoryManager)
        {
            if (territoryManager == null)
            {
                throw new ArgumentNullException(nameof(territoryManager));
            }
            _territoryManager = territoryManager;
        }

        [HttpGet]
        public IEnumerable<Territory> GetAllTerritories()
        {
            return _territoryManager.GetAllTerritories();
        }

        [HttpGet]
        [Route("{gangId}")]
        public IEnumerable<GangTerritory> GetGangTerritoryById([FromUri] string gangId)
        {
            return _territoryManager.GetTerritoriesByGangId(gangId);
        }

        [HttpPost]
        [Route("{gangId}")]
        public GangTerritory AddGangTerritory([FromUri]string gangId, Territory territory)
        {
            var gangTerritory = new GangTerritory()
            {
                Territory = territory,
                GangId = gangId
            };
            return _territoryManager.AddGangTerritory(gangTerritory);
        }

        [HttpDelete]
        [Route("{gangTerritoryId}")]
        public void RemoveGangTerritory([FromUri] string gangTerritoryId)
        {
            _territoryManager.RemoveGangTerritory(gangTerritoryId);
        }
    }
}
