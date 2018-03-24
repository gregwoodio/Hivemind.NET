// <copyright file="IGangManager.cs" company="weirdvector">
// Copyright (c) weirdvector. All rights reserved.
// </copyright>

using Hivemind.Entities;

namespace Hivemind.Managers
{
    /// <summary>
    /// Gang Manager interface
    /// </summary>
    public interface IGangManager
    {
        /// <summary>
        /// Get gang
        /// </summary>
        /// <param name="gangId">Gang ID</param>
        /// <returns>The requested gang</returns>
        Gang GetGang(string gangId);

        /// <summary>
        /// Update a gang
        /// </summary>
        /// <param name="gang">Gang to update</param>
        /// <returns>Updated gang</returns>
        Gang UpdateGang(Gang gang);

        /// <summary>
        /// Add gang
        /// </summary>
        /// <param name="gang">Gang to add</param>
        /// <returns>Added gang</returns>
        Gang AddGang(Gang gang);

        /// <summary>
        /// Associate gang to a user
        /// </summary>
        /// <param name="gangId">Gang ID</param>
        /// <param name="userId">User ID</param>
        void AssociateGangToUser(string gangId, string userId);
    }
}
