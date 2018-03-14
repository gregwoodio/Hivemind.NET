using Hivemind.Contracts;
using Hivemind.Entities;
using System;
using System.Collections.Generic;

namespace Hivemind.Managers
{
    public interface ITerritoryManager
    {
        Territory GetTerritory(int territoryId);
        IEnumerable<Territory> GetAllTerritories();
        IEnumerable<Territory> GetTerritoriesByGangId(string gangId);
        Func<TerritoryWorkStatus, TerritoryIncomeReport> GetTerritoryEffect(int territoryId);
        GangTerritory AddGangTerritory(GangTerritory territory);
        void RemoveGangTerritory(string gangTerritoryId);
    }
}
