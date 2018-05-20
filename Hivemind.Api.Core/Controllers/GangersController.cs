// <copyright file="GangersController.cs" company="weirdvector">
// Copyright (c) weirdvector. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using Hivemind.Entities;
using Hivemind.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    /// <summary>
    /// Gangers Controller
    /// </summary>
    [Route("api/gangers")]
    public class GangersController : Controller
    {
        /// <summary>
        /// Ganger manager
        /// </summary>
        private IGangerManager _gangerManager;

        /// <summary>
        /// Weapon manager
        /// </summary>
        private IWeaponManager _weaponManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="GangersController"/> class.
        /// </summary>
        /// <param name="gangerManager">Ganger Manager</param>
        /// <param name="weaponManager">Weapon Manager</param>
        public GangersController(IGangerManager gangerManager, IWeaponManager weaponManager)
        {
            if (gangerManager == null)
            {
                throw new ArgumentNullException(nameof(gangerManager));
            }

            if (weaponManager == null)
            {
                throw new ArgumentNullException(nameof(weaponManager));
            }

            _gangerManager = gangerManager;
            _weaponManager = weaponManager;
        }

        /// <summary>
        /// Add ganger
        /// </summary>
        /// <param name="ganger">Ganger to add</param>
        /// <returns>Added ganger</returns>
        [Authorize]
        [HttpPost]
        public Ganger AddGanger(Ganger ganger)
        {
            // TODO: Add validation (can gang afford new ganger?)
            return _gangerManager.AddGanger(ganger);
        }

        /// <summary>
        /// Get a ganger
        /// </summary>
        /// <param name="gangerId">Ganger ID</param>
        /// <returns>Ganger</returns>
        [Authorize]
        [HttpGet]
        [Route("{gangerId}")]
        public Ganger GetGanger([FromRoute] string gangerId)
        {
            return _gangerManager.GetGanger(gangerId);
        }

        /// <summary>
        /// Update a ganger
        /// </summary>
        /// <param name="ganger">Ganger to update</param>
        /// <returns>Updated ganger</returns>
        [Authorize]
        [HttpPut]
        public Ganger UpdateGanger(Ganger ganger)
        {
            return _gangerManager.UpdateGanger(ganger);
        }

        #region weapon routes

        /// <summary>
        /// Get ganger's weapons
        /// </summary>
        /// <param name="gangerId">Ganger ID</param>
        /// <returns>Ganger's weapons</returns>
        [Authorize]
        [HttpGet]
        [Route("{gangerId}/weapons")]
        public IEnumerable<GangerWeapon> GetWeapons([FromRoute] string gangerId)
        {
            return _weaponManager.GetGangerWeapons(gangerId);
        }

        /// <summary>
        /// Add ganger weapon. Gangers are equipped weapons from the stash, so we provide the gangWeapon ID in the request.
        /// </summary>
        /// <param name="gangerId">Ganger ID</param>
        /// <param name="gangWeaponId">GangWeapon ID</param>
        /// <returns>Ganger Weapon</returns>
        [Authorize]
        [HttpPost]
        [Route("{gangerId}/weapons/{gangWeaponId}")]
        public GangerWeapon AddGangerWeapon([FromRoute] string gangerId, [FromRoute] string gangWeaponId)
        {
            return _weaponManager.AddGangerWeapon(gangerId, gangWeaponId);
        }

        /// <summary>
        /// Remove ganger weapon (return to stash)
        /// </summary>
        /// <param name="gangerId">Ganger ID</param>
        /// <param name="gangerWeaponId">Ganger Weapon ID</param>
        [Authorize]
        [HttpDelete]
        [Route("{gangerId}/weapons/{gangerWeaponId}")]
        public void RemoveGangerWeapon([FromRoute] string gangerId, [FromRoute] string gangerWeaponId)
        {
            _weaponManager.RemoveGangerWeapon(gangerId, gangerWeaponId);
        }

        #endregion
    }
}