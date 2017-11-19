using Hivemind.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hivemind.Managers
{
    public interface IInjuryManager
    {
        Injury GetInjury(int injuryId);

        IEnumerable<Injury> GetAllInjuries();

        IEnumerable<Injury> GetInjuriesByGangId(int gangId);
    }
}
