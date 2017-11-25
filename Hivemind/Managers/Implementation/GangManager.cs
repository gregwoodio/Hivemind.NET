using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hivemind.Entities;
using Hivemind.Providers;

namespace Hivemind.Managers.Implementation
{
    public class GangManager : IGangManager
    {
        private GangProvider _provider;

        public GangManager(GangProvider provider)
        {
            _provider = provider ?? throw new ArgumentNullException(nameof(provider));
        }

        public Gang GetGang(string gangId)
        {
            return _provider.GetGangById(gangId);
        }

        public Gang AddGang(Gang gang)
        {
            return _provider.AddGang(gang);
        }

        public Gang UpdateGang(Gang gang)
        {
            return _provider.UpdateGang(gang);
        }
    }
}
