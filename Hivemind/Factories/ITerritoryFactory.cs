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
        IEnumerable<Territory> GetTerritoriesByGangId(int gangId);
        TerritoryEffect GetTerritoryEffect(int territoryId);
        Territory UpdateGangTerritory(int gangId, Territory territory);
    }
}
