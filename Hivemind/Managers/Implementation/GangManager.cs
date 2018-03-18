using System;
using System.Linq;
using Hivemind.Entities;
using Hivemind.Enums;
using Hivemind.Exceptions;
using Hivemind.Providers;
using Hivemind.Utilities;

namespace Hivemind.Managers.Implementation
{
    public class GangManager : IGangManager
    {
        private readonly GangProvider _gangProvider;
        private readonly GangerProvider _gangerProvider;
        private readonly TerritoryProvider _territoryProvider;
        private readonly WeaponProvider _weaponProvider;
        private readonly InjuryProvider _injuryProvider;
        private readonly SkillProvider _skillProvider;

        public GangManager(
            GangProvider gangProvider, 
            GangerProvider gangerProvider, 
            TerritoryProvider territoryProvider,
            WeaponProvider weaponProvider,
            InjuryProvider injuryProvider,
            SkillProvider skillProvider)
        {
            _gangProvider = gangProvider ?? throw new ArgumentNullException(nameof(gangProvider));
            _gangerProvider = gangerProvider ?? throw new ArgumentNullException(nameof(gangerProvider));
            _territoryProvider = territoryProvider ?? throw new ArgumentNullException(nameof(territoryProvider));
            _weaponProvider = weaponProvider ?? throw new ArgumentNullException(nameof(weaponProvider));
            _injuryProvider = injuryProvider ?? throw new ArgumentNullException(nameof(InjuryProvider));
            _skillProvider = skillProvider ?? throw new ArgumentNullException(nameof(skillProvider));
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
            var skills = _skillProvider.GetAllSkills();
            var gangerSkills = _gangerProvider.GetGangerSkills(gang.GangId);

            foreach (var ganger in gang.Gangers)
            {
                ganger.Injuries = injuries.Where(gi => gi.GangerId == ganger.GangerId)
                    .Select(gi => gi.Injury)
                    .ToList();

                ganger.Skills = gangerSkills.Where(gs => gs.GangerId == ganger.GangerId)
                    .Select(gs => gs.SkillId)
                    .Select(id => skills.Where(skill => skill.SkillId == id).First())
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

            InitialGangTerritories(addedGang.GangId);

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

        private void InitialGangTerritories(string gangId)
        {
            for (int i = 0; i < 5; i++)
            {
                var gangTerritory = new GangTerritory()
                {
                    GangId = gangId,
                    Territory = GetRandomTerritory()
                };

                _territoryProvider.AddGangTerritory(gangTerritory);
            }
        }

        private Territory GetRandomTerritory()
        {
            var roll = DiceRoller.RollD66();

            switch (roll)
            {
                case 11:
                    return _territoryProvider.GetTerritoryById((int)TerritoryEnum.Chempit);
                case 12:
                case 13:
                case 14:
                case 15:
                case 16:
                    return _territoryProvider.GetTerritoryById((int)TerritoryEnum.OldRuins);
                case 21:
                case 22:
                case 23:
                case 24:
                case 25:
                    return _territoryProvider.GetTerritoryById((int)TerritoryEnum.Slag);
                case 26:
                    return _territoryProvider.GetTerritoryById((int)TerritoryEnum.MineralOutcrop);
                case 31:
                case 32:
                case 33:
                case 34:
                case 35:
                    return _territoryProvider.GetTerritoryById((int)TerritoryEnum.Settlement);
                case 36:
                    return _territoryProvider.GetTerritoryById((int)TerritoryEnum.MineWorkings);
                case 41:
                case 42:
                    return _territoryProvider.GetTerritoryById((int)TerritoryEnum.Tunnels);
                case 43:
                case 44:
                    return _territoryProvider.GetTerritoryById((int)TerritoryEnum.Vents);
                case 45:
                case 46:
                    return _territoryProvider.GetTerritoryById((int)TerritoryEnum.Holestead);
                case 51:
                case 52:
                    return _territoryProvider.GetTerritoryById((int)TerritoryEnum.Waterstill);
                case 53:
                case 54:
                    return _territoryProvider.GetTerritoryById((int)TerritoryEnum.DrinkingHole);
                case 55:
                case 56:
                    return _territoryProvider.GetTerritoryById((int)TerritoryEnum.GuilderContract);
                case 61:
                    return _territoryProvider.GetTerritoryById((int)TerritoryEnum.FriendlyDoc);
                case 62:
                    return _territoryProvider.GetTerritoryById((int)TerritoryEnum.Workshop);
                case 63:
                    return _territoryProvider.GetTerritoryById((int)TerritoryEnum.GamblingDen);
                case 64:
                    return _territoryProvider.GetTerritoryById((int)TerritoryEnum.SporeCave);
                case 65:
                    return _territoryProvider.GetTerritoryById((int)TerritoryEnum.Archeotech);
                case 66:
                    return _territoryProvider.GetTerritoryById((int)TerritoryEnum.GreenHivers);
                default:
                    throw new HivemindException("Invalid territory ID");
            }
        }

    }
}
