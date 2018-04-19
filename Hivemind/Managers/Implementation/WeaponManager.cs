// <copyright file="WeaponManager.cs" company="weirdvector">
// Copyright (c) weirdvector. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using Hivemind.Entities;
using Hivemind.Providers;

namespace Hivemind.Managers.Implementation
{
    /// <summary>
    /// Weapon manager
    /// </summary>
    public class WeaponManager : IWeaponManager
    {
        private IGangerProvider _gangerProvider;
        private IWeaponProvider _weaponProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="WeaponManager"/> class.
        /// </summary>
        /// <param name="weaponProvider">Weapon provider</param>
        /// <param name="gangerProvider">Ganger provider</param>
        public WeaponManager(IWeaponProvider weaponProvider, IGangerProvider gangerProvider)
        {
            _weaponProvider = weaponProvider ?? throw new ArgumentNullException(nameof(weaponProvider));
            _gangerProvider = gangerProvider ?? throw new ArgumentNullException(nameof(GangerProvider));
        }

        /// <summary>
        /// Get Weapon
        /// </summary>
        /// <param name="weaponId">The weapon ID</param>
        /// <returns>Requested weapon</returns>
        public Weapon GetWeapon(int weaponId)
        {
            return _weaponProvider.GetById(weaponId);
        }

        /// <summary>
        /// Get all weapons
        /// </summary>
        /// <returns>Weapons</returns>
        public IEnumerable<Weapon> GetAllWeapons()
        {
            return _weaponProvider.GetAllWeapons();
        }

        /// <summary>
        /// Add gang weapon
        /// </summary>
        /// <param name="gangWeapon">Gang weapon</param>
        /// <returns>Added gang weapon</returns>
        public GangWeapon AddGangWeapon(GangWeapon gangWeapon)
        {
            return _weaponProvider.AddGangWeapon(gangWeapon);
        }

        /// <summary>
        /// Remove Gang weapon
        /// </summary>
        /// <param name="gangWeaponId">Gang weapon ID</param>
        public void RemoveGangWeapon(string gangWeaponId)
        {
            _weaponProvider.RemoveGangWeapon(gangWeaponId);
        }

        /// <summary>
        /// Get gang stash (weapons belonging to a gang but unassigned to a ganger)
        /// </summary>
        /// <param name="gangId">Gang ID</param>
        /// <returns>Weapon list</returns>
        public IEnumerable<GangWeapon> GetGangStash(string gangId)
        {
            return _weaponProvider.GetGangStash(gangId);
        }

        /// <summary>
        /// Get ganger weapons by gang ID
        /// </summary>
        /// <param name="gangId">Gang ID</param>
        /// <returns>Weapons equipped to gangers in specified gang</returns>
        public IEnumerable<Weapon> GetGangerWeaponsByGangId(string gangId)
        {
            return _weaponProvider.GetGangerWeaponsByGangId(gangId);
        }

        /// <summary>
        /// Add ganger weapon (equip weapon to ganger)
        /// </summary>
        /// <param name="gangerId">Ganger Id</param>
        /// <param name="gangWeaponId">Ganger weapon</param>
        /// <returns>Added ganger weapon</returns>
        public GangerWeapon AddGangerWeapon(string gangerId, string gangWeaponId)
        {
            var ganger = _gangerProvider.GetByGangerId(gangerId);

            var gangWeapon = _weaponProvider.GetGangStash(ganger.GangId)
                .FirstOrDefault(gw => gw.GangWeaponId == gangWeaponId);

            if (gangWeapon == null)
            {
                throw new ArgumentException($"GangWeapon with id {gangWeaponId} was not found.");
            }

            _weaponProvider.RemoveGangWeapon(gangWeaponId);

            var gangerWeapon = new GangerWeapon()
            {
                Cost = gangWeapon.Cost,
                GangerId = ganger.GangerId,
                Weapon = gangWeapon.Weapon,
            };

            return _weaponProvider.AddGangerWeapon(gangerWeapon);
        }

        /// <summary>
        /// Remove ganger weapon (unequip weapon from ganger)
        /// </summary>
        /// <param name="gangerId">Ganger ID</param>
        /// <param name="gangerWeaponId">Ganger weapon ID to remove</param>
        public void RemoveGangerWeapon(string gangerId, string gangerWeaponId)
        {
            var ganger = _gangerProvider.GetByGangerId(gangerId);

            var gangerWeapon = _weaponProvider.GetGangerWeapons(gangerId)
                .FirstOrDefault(gw => gw.GangerWeaponId == gangerWeaponId);

            if (gangerWeapon == null)
            {
                throw new ArgumentException($"Ganger does not have GangerWeapon with id {gangerWeaponId} equipped.");
            }

            _weaponProvider.RemoveGangerWeapon(gangerWeaponId);

            var gangWeapon = new GangWeapon()
            {
                Cost = gangerWeapon.Cost,
                GangId = ganger.GangId,
                Weapon = gangerWeapon.Weapon,
            };

            _weaponProvider.AddGangWeapon(gangWeapon);
        }

        /// <summary>
        /// Get ganger weapons (all equipped weapons)
        /// </summary>
        /// <param name="gangerId">Ganger ID</param>
        /// <returns>Ganger weapons</returns>
        public IEnumerable<GangerWeapon> GetGangerWeapons(string gangerId)
        {
            return _weaponProvider.GetGangerWeapons(gangerId);
        }
    }
}
