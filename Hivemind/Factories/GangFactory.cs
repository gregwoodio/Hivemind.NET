using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hivemind.Entities;
using Hivemind.Providers;

namespace Hivemind.Factories
{
    public class GangFactory : IGangFactory
    {
        //TODO: Use injection.
        GangProvider provider = new GangProvider();

        public Gang GetGang(int gangId)
        {
            return provider.GetGangById(gangId);
        }

        public Gang UpdateGang(Gang gang)
        {
            return provider.UpdateGang(gang);
        }
    }
}
