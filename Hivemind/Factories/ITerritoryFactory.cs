using Hivemind.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hivemind.Factories
{
    public interface ITerritoryFactory
    {
        Territory GetTerritory(int territoryId);
        IEnumerable<Territory> GetAllTerritories();
        IEnumerable<GangTerritory> GetTerritoriesByGangId(string gangId);
        TerritoryEffect GetTerritoryEffect(int territoryId);
        GangTerritory AddGangTerritory(GangTerritory territory);
        void RemoveGangTerritory(string gangTerritoryId);
    }
}
