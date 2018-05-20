using Hivemind.Entities;
using Hivemind.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace WebApi.Controllers
{
    [Route("api/territories")]
    public class TerritoriesController : Controller
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

        [Authorize]
        [HttpGet]
        public IEnumerable<Territory> GetAllTerritories()
        {
            return _territoryManager.GetAllTerritories();
        }

        [Authorize]
        [HttpGet]
        [Route("{gangId}")]
        public IEnumerable<Territory> GetGangTerritoryById([FromRoute] string gangId)
        {
            return _territoryManager.GetTerritoriesByGangId(gangId);
        }

        [Authorize]
        [HttpPost]
        [Route("{gangId}")]
        public GangTerritory AddGangTerritory([FromRoute]string gangId, Territory territory)
        {
            var gangTerritory = new GangTerritory()
            {
                Territory = territory,
                GangId = gangId
            };
            return _territoryManager.AddGangTerritory(gangTerritory);
        }

        [Authorize]
        [HttpDelete]
        [Route("{gangTerritoryId}")]
        public void RemoveGangTerritory([FromRoute] string gangTerritoryId)
        {
            _territoryManager.RemoveGangTerritory(gangTerritoryId);
        }
    }
}
