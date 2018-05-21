// <copyright file="IUserManager.cs" company="weirdvector">
// Copyright (c) weirdvector. All rights reserved.
// </copyright>

using System.Collections.Generic;
using Hivemind.Entities;

namespace Hivemind.Managers
{
    /// <summary>
    /// User manager interface
    /// </summary>
    public interface IUserManager
    {
        /// <summary>
        /// Register a new user.
        /// </summary>
        /// <param name="user">Login user</param>
        /// <returns>User</returns>
        Contracts.User RegisterUser(Entities.Login user);

        /// <summary>
        /// Login an existing user.
        /// </summary>
        /// <param name="user">Login user</param>
        /// <returns>User</returns>
        Contracts.User Login(Entities.Login user);

        /// <summary>
        /// Gets user by GUID.
        /// </summary>
        /// <param name="userGuid">User GUID</param>
        /// <returns>User</returns>
        Contracts.User GetUser(string userGuid);

        /// <summary>
        /// Gets the gang IDs of a given user.
        /// </summary>
        /// <param name="userGuid">User GUID</param>
        /// <returns>Gang</returns>
        IEnumerable<Gang> GetUserGangs(string userGuid);
    }
}
