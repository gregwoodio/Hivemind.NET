// <copyright file="GangManager.cs" company="weirdvector">
// Copyright (c) weirdvector. All rights reserved.
// </copyright>

using System;
using System.Linq;
using Hivemind.Entities;
using Hivemind.Enums;
using Hivemind.Exceptions;
using Hivemind.Providers;
using Hivemind.Utilities;

namespace Hivemind.Managers.Implementation
{
    /// <summary>
    /// Gang Manager
    /// </summary>
    public class GangManager : IGangManager
    {
        private readonly IGangProvider _gangProvider;
        private readonly IGangerProvider _gangerProvider;
        private readonly ITerritoryProvider _territoryProvider;
        private readonly IWeaponProvider _weaponProvider;
        private readonly IInjuryProvider _injuryProvider;
        private readonly ISkillProvider _skillProvider;

        private const int StartingGangTerritories = 5;

        /// <summary>
        /// Initializes a new instance of the <see cref="GangManager"/> class.
        /// </summary>
        /// <param name="gangProvider">Gang provider</param>
        /// <param name="gangerProvider">Ganger provider</param>
        /// <param name="territoryProvider">Territory provider</param>
        /// <param name="weaponProvider">Weapon provider</param>
        /// <param name="injuryProvider">Injury provider</param>
        /// <param name="skillProvider">Skill provider</param>
        public GangManager(
            IGangProvider gangProvider,
            IGangerProvider gangerProvider,
            ITerritoryProvider territoryProvider,
            IWeaponProvider weaponProvider,
            IInjuryProvider injuryProvider,
            ISkillProvider skillProvider)
        {
            _gangProvider = gangProvider ?? throw new ArgumentNullException(nameof(gangProvider));
            _gangerProvider = gangerProvider ?? throw new ArgumentNullException(nameof(gangerProvider));
            _territoryProvider = territoryProvider ?? throw new ArgumentNullException(nameof(territoryProvider));
            _weaponProvider = weaponProvider ?? throw new ArgumentNullException(nameof(weaponProvider));
            _injuryProvider = injuryProvider ?? throw new ArgumentNullException(nameof(InjuryProvider));
            _skillProvider = skillProvider ?? throw new ArgumentNullException(nameof(skillProvider));
        }

        /// <summary>
        /// Get gang
        /// </summary>
        /// <param name="gangId">Gang ID</param>
        /// <returns>The requested gang</returns>
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

                ganger.Weapons = gangerWeapons;

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

        /// <summary>
        /// Add gang
        /// </summary>
        /// <param name="gang">Gang to add</param>
        /// <returns>Added gang</returns>
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

        /// <summary>
        /// Update a gang
        /// </summary>
        /// <param name="gang">Gang to update</param>
        /// <returns>Updated gang</returns>
        public Gang UpdateGang(Gang gang)
        {
            return _gangProvider.UpdateGang(gang);
        }

        /// <summary>
        /// Associate gang to a user
        /// </summary>
        /// <param name="gangId">Gang ID</param>
        /// <param name="userId">User ID</param>
        public void AssociateGangToUser(string gangId, string userId)
        {
            _gangProvider.AssociateGangToUser(gangId, userId);
        }

        /// <summary>
        /// Spend gang's credits.
        /// </summary>
        /// <param name="gangId">Gang Id</param>
        /// <param name="cost">Credits to spend</param>
        /// <returns>True if gang had the required credits.</returns>
        public bool Spend(string gangId, int cost)
        {
            if (cost < 0)
            {
                throw new ArgumentException("Cost must be greater than 0.");
            }

            var gang = GetGang(gangId);

            if (gang.Credits >= cost)
            {
                gang.Credits -= cost;
                UpdateGang(gang);
                return true;
            }

            return false;
        }

        private void InitialGangTerritories(string gangId)
        {
            for (int i = 0; i < StartingGangTerritories; i++)
            {
                var gangTerritory = new GangTerritory()
                {
                    GangId = gangId,
                    Territory = GetRandomTerritory(),
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
