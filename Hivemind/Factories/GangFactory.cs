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
        public Gang GetGang(int gangId)
        {
            var provider = new GangProvider();
            return provider.GetGangById(gangId);
        }
    }
}
