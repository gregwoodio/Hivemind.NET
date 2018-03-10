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
        private readonly GangProvider _gangProvider;
        private readonly GangerProvider _gangerProvider;
        private readonly TerritoryProvider _territoryProvider;
        private readonly WeaponProvider _weaponProvider;

        public GangManager(
            GangProvider gangProvider, 
            GangerProvider gangerProvider, 
            TerritoryProvider territoryProvider,
            WeaponProvider weaponProvider)
        {
            _gangProvider = gangProvider ?? throw new ArgumentNullException(nameof(gangProvider));
            _gangerProvider = gangerProvider ?? throw new ArgumentNullException(nameof(gangerProvider));
            _territoryProvider = territoryProvider ?? throw new ArgumentNullException(nameof(territoryProvider));
            _weaponProvider = weaponProvider ?? throw new ArgumentNullException(nameof(weaponProvider));
        }

        public Gang GetGang(string gangId)
        {
            var gang = _gangProvider.GetGangById(gangId);
            gang.Gangers = _gangerProvider.GetByGangId(gangId);
            gang.Territories = _territoryProvider.GetTerritoryByGangId(gangId);

            var weapons = _weaponProvider.GetByGangId(gangId);
            foreach (var ganger in gang.Gangers)
            {
                ganger.Weapons = weapons
                    .Where(gw => gw.GangerId == ganger.GangerId)
                    .Select(gw => gw.Weapon);
            }

            return gang;
        }

        public Gang AddGang(Gang gang)
        {
            var addedGang = _gangProvider.AddGang(gang);
            foreach (var ganger in addedGang.Gangers)
            {
                _gangerProvider.AddGanger(ganger);
            }

            return addedGang;
        }

        public Gang UpdateGang(Gang gang)
        {
            return _gangProvider.UpdateGang(gang);
        }

        public void AssociateGangToUser(string gangId, string userId)
        {
            _gangProvider.AssociateGangToUser(gangId, userId);
        }
    }
}
