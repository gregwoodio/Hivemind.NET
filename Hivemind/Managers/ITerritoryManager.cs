using Hivemind.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hivemind.Managers
{
    public interface ITerritoryManager
    {
        Territory GetTerritory(int territoryId);
        IEnumerable<Territory> GetAllTerritories();
        IEnumerable<Territory> GetTerritoriesByGangId(string gangId);
        TerritoryEffect GetTerritoryEffect(int territoryId);
        GangTerritory AddGangTerritory(GangTerritory territory);
        void RemoveGangTerritory(string gangTerritoryId);
    }
}
