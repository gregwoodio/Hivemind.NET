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
        private readonly InjuryProvider _injuryProvider;

        public GangManager(
            GangProvider gangProvider, 
            GangerProvider gangerProvider, 
            TerritoryProvider territoryProvider,
            WeaponProvider weaponProvider,
            InjuryProvider injuryProvider)
        {
            _gangProvider = gangProvider ?? throw new ArgumentNullException(nameof(gangProvider));
            _gangerProvider = gangerProvider ?? throw new ArgumentNullException(nameof(gangerProvider));
            _territoryProvider = territoryProvider ?? throw new ArgumentNullException(nameof(territoryProvider));
            _weaponProvider = weaponProvider ?? throw new ArgumentNullException(nameof(weaponProvider));
            _injuryProvider = injuryProvider ?? throw new ArgumentNullException(nameof(InjuryProvider));
        }

        public Gang GetGang(string gangId)
        {
            var gang = _gangProvider.GetGangById(gangId);
            gang.Gangers = _gangerProvider.GetByGangId(gangId).Where(ganger => ganger.Active);
            gang.Territories = _territoryProvider.GetTerritoryByGangId(gangId);

            var weapons = _weaponProvider.GetByGangId(gangId);
            foreach (var ganger in gang.Gangers)
            {
                var gangerWeapons = weapons.Where(gw => gw.GangerId == ganger.GangerId);

                foreach (var gangerWeapon in gangerWeapons)
                {
                    gangerWeapon.Weapon.Cost = gangerWeapon.Cost.ToString();
                }
                
                ganger.Weapons = gangerWeapons.Select(gw => gw.Weapon);

                ganger.GetCost();
            }

            var injuries = _injuryProvider.GetInjuriesByGangId(gangId);
            foreach (var ganger in gang.Gangers)
            {
                ganger.Injuries = injuries.Where(gi => gi.GangerId == ganger.GangerId)
                                        .Select(gi => gi.Injury)
                                        .ToList();
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
