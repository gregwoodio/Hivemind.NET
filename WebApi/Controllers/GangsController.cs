// <copyright file="GangsController.cs" company="weirdvector">
// Copyright (c) weirdvector. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;
using Hivemind.Entities;
using Hivemind.Exceptions;
using Hivemind.Managers;

namespace WebApi.Controllers
{
    /// <summary>
    /// GangsController
    /// </summary>
    [RoutePrefix("api/gangs")]
    public class GangsController : ApiController
    {
        /// <summary>
        /// Gang Manager
        /// </summary>
        private IGangManager _gangManager;

        /// <summary>
        /// Weapon Manager
        /// </summary>
        private IWeaponManager _weaponManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="GangsController"/> class.
        /// </summary>
        /// <param name="gangManager">Gang Manager</param>
        /// <param name="weaponManager">Weapon Manager</param>
        public GangsController(IGangManager gangManager, IWeaponManager weaponManager)
        {
            if (gangManager == null)
            {
                throw new ArgumentNullException(nameof(gangManager));
            }

            if (weaponManager == null)
            {
                throw new ArgumentNullException(nameof(weaponManager));
            }

            _gangManager = gangManager;
            _weaponManager = weaponManager;
        }

        /// <summary>
        /// Gets a gang by ID
        /// </summary>
        /// <param name="gangId">Gang ID</param>
        /// <returns>Gang</returns>
        [Authorize]
        [HttpGet]
        [Route("{gangId}")]
        public Gang GetGang([FromUri] string gangId)
        {
            return _gangManager.GetGang(gangId);
        }

        /// <summary>
        /// Add a new gang
        /// </summary>
        /// <param name="gang">Gang to be added</param>
        /// <returns>Added gang</returns>
        [Authorize]
        [HttpPost]
        [Route("")]
        public Gang AddGang([FromBody] Gang gang)
        {
            // TODO: validate request
            // TODO: Assign gang to principal
            var principal = User as ClaimsPrincipal;
            var userId = principal.Claims.FirstOrDefault(claim => claim.Type == "userId");
            if (userId == null)
            {
                throw new HivemindException("User does not have a user ID");
            }

            var gangEntity = _gangManager.AddGang(gang);

            _gangManager.AssociateGangToUser(gang.GangId, userId.Value);

            return gangEntity;
        }

        /// <summary>
        /// Update a gang
        /// </summary>
        /// <param name="gang">Gang to update</param>
        /// <returns>Updated gang</returns>
        [Authorize]
        [HttpPut]
        public Gang UpdateGang(Gang gang)
        {
            // TODO: validate request
            return _gangManager.UpdateGang(gang);
        }

        #region weapon routes

        /// <summary>
        /// Gets the gang's stash of weapons (unequipped to a ganger).
        /// </summary>
        /// <param name="gangId">Gang ID</param>
        /// <returns>List of gang's weapons.</returns>
        [Authorize]
        [HttpGet]
        [Route("{gangId}/weapons")]
        public IEnumerable<Weapon> GetGangStash([FromUri] string gangId)
        {
            return _weaponManager.GetGangStash(gangId).Select(gw => gw.Weapon);
        }

        /// <summary>
        /// Buy a new gang weapon (add to stash).
        /// </summary>
        /// <param name="gangId">Gang Id</param>
        /// <param name="gangWeapon">GangWeapon</param>
        /// <returns>The added GangWeapon</returns>
        [Authorize]
        [HttpPost]
        [Route("{gangId}/weapons")]
        public GangWeapon AddGangWeapon([FromUri] string gangId, GangWeapon gangWeapon)
        {
            if (!_gangManager.Spend(gangId, gangWeapon.Cost))
            {
                return null;
            }

            return _weaponManager.AddGangWeapon(gangWeapon);
        }

        /// <summary>
        /// Gets all of the weapons for a gang that are equipped to a ganger.
        /// </summary>
        /// <param name="gangId">Gang Id</param>
        /// <returns>Gang's weapons</returns>
        [Authorize]
        [HttpGet]
        [Route("{gangId}/weapons/gangers")]
        public IEnumerable<Weapon> GetGangerWeapons([FromUri] string gangId)
        {
            return _weaponManager.GetGangerWeaponsByGangId(gangId);
        }
        #endregion
    }
}
