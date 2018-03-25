// <copyright file="IWeaponProvider.cs" company="weirdvector">
// Copyright (c) weirdvector. All rights reserved.
// </copyright>

using System.Collections.Generic;
using Hivemind.Entities;

namespace Hivemind.Providers
{
    /// <summary>
    /// Weapon provider interface
    /// </summary>
    public interface IWeaponProvider
    {
        /// <summary>
        /// Get by weapon ID
        /// </summary>
        /// <param name="weaponId">Weapon ID</param>
        /// <returns>Weapon</returns>
        Weapon GetById(int weaponId);

        /// <summary>
        /// Get all weapons
        /// </summary>
        /// <returns>Returns all weapons</returns>
        IEnumerable<Weapon> GetAllWeapons();

        /// <summary>
        /// Get all ganger weapons for a gang
        /// </summary>
        /// <param name="gangId">Gang ID</param>
        /// <returns>List of ganger weapons</returns>
        IEnumerable<GangerWeapon> GetByGangId(string gangId);

        /// <summary>
        /// Add gang weapon. Adds a weapon to the gang's stash.
        /// </summary>
        /// <param name="gangWeapon">Gang weapon</param>
        /// <returns>Added gang weapon</returns>
        GangWeapon AddGangWeapon(GangWeapon gangWeapon);

        /// <summary>
        /// Remove gang weapon
        /// </summary>
        /// <param name="gangWeaponId">Gang weapon ID</param>
        void RemoveGangWeapon(string gangWeaponId);

        /// <summary>
        /// Get gang stash (all gang's GangWeapons).
        /// </summary>
        /// <param name="gangId">Gang ID</param>
        /// <returns>List of weapons</returns>
        IEnumerable<Weapon> GetGangStash(string gangId);

        /// <summary>
        /// Get ganger weapons by gang ID
        /// </summary>
        /// <param name="gangId">Gang ID</param>
        /// <returns>Weapons</returns>
        IEnumerable<Weapon> GetGangerWeaponsByGangId(string gangId);

        /// <summary>
        /// Add ganger weapon
        /// </summary>
        /// <param name="gangerWeapon">Ganger weapon</param>
        /// <returns>Added ganger weapon</returns>
        GangerWeapon AddGangerWeapon(GangerWeapon gangerWeapon);

        /// <summary>
        /// Remove ganger weapon
        /// </summary>
        /// <param name="gangerWeaponId">Ganger weapon ID</param>
        void RemoveGangerWeapon(string gangerWeaponId);

        /// <summary>
        /// Get ganger weapons
        /// </summary>
        /// <param name="gangerId">Ganger ID</param>
        /// <returns>List of ganger weapons</returns>
        IEnumerable<GangerWeapon> GetGangerWeapons(string gangerId);
    }
}
