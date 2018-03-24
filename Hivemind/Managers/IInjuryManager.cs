// <copyright file="IInjuryManager.cs" company="weirdvector">
// Copyright (c) weirdvector. All rights reserved.
// </copyright>

using System.Collections.Generic;
using Hivemind.Entities;

namespace Hivemind.Managers
{
    /// <summary>
    /// Injury manager interface
    /// </summary>
    public interface IInjuryManager
    {
        /// <summary>
        /// Get Injury by ID
        /// </summary>
        /// <param name="injuryId">Injury ID</param>
        /// <returns>Injury</returns>
        Injury GetInjury(int injuryId);

        /// <summary>
        /// Get all injuries
        /// </summary>
        /// <returns>Injuries</returns>
        IEnumerable<Injury> GetAllInjuries();

        /// <summary>
        /// Get all injuries for a given gang
        /// </summary>
        /// <param name="gangId">Gang Id</param>
        /// <returns>Injuries</returns>
        IEnumerable<GangerInjury> GetInjuriesByGangId(string gangId);
    }
}
