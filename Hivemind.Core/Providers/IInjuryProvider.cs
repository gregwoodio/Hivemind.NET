// <copyright file="IInjuryProvider.cs" company="weirdvector">
// Copyright (c) weirdvector. All rights reserved.
// </copyright>

using System.Collections.Generic;
using Hivemind.Entities;

namespace Hivemind.Providers
{
    /// <summary>
    /// Injury provider interface
    /// </summary>
    public interface IInjuryProvider
    {
        /// <summary>
        /// Get Injury by ID
        /// </summary>
        /// <param name="injuryId">Injury ID</param>
        /// <returns>Injury</returns>
        Injury GetInjuryById(int injuryId);

        /// <summary>
        /// Get All Injuries
        /// </summary>
        /// <returns>Injuries</returns>
        IEnumerable<Injury> GetAllInjuries();

        /// <summary>
        /// Get injuries for the specified ganger.
        /// </summary>
        /// <param name="gangerId">Ganger ID</param>
        /// <returns>Injury list</returns>
        IEnumerable<Injury> GetInjuriesByGangerId(string gangerId);

        /// <summary>
        /// Get injuries by Gang ID
        /// </summary>
        /// <param name="gangId">Gang ID</param>
        /// <returns>List of GangerInjury</returns>
        IEnumerable<GangerInjury> GetInjuriesByGangId(string gangId);
    }
}
