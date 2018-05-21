// <copyright file="IGangProvider.cs" company="weirdvector">
// Copyright (c) weirdvector. All rights reserved.
// </copyright>

using Hivemind.Entities;

namespace Hivemind.Providers
{
    /// <summary>
    /// Gang provider interface
    /// </summary>
    public interface IGangProvider
    {
        /// <summary>
        /// Get Gang by ID
        /// </summary>
        /// <param name="gangId">Gang ID</param>
        /// <returns>Gang</returns>
        Gang GetGangById(string gangId);

        /// <summary>
        /// Associate a gang to a user
        /// </summary>
        /// <param name="gangId">Gang ID</param>
        /// <param name="userId">User ID</param>
        void AssociateGangToUser(string gangId, string userId);

        /// <summary>
        /// Add a gang
        /// </summary>
        /// <param name="gang">Gang</param>
        /// <returns>Added gang</returns>
        Gang AddGang(Gang gang);

        /// <summary>
        /// Update a gang
        /// </summary>
        /// <param name="gang">Gang</param>
        /// <returns>Updated gang</returns>
        Gang UpdateGang(Gang gang);
    }
}
