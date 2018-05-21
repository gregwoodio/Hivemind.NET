// <copyright file="IUserProvider.cs" company="weirdvector">
// Copyright (c) weirdvector. All rights reserved.
// </copyright>

using System.Collections.Generic;
using Hivemind.Entities;

namespace Hivemind.Providers
{
    /// <summary>
    /// User provider interface
    /// </summary>
    public interface IUserProvider
    {
        /// <summary>
        /// Add user
        /// </summary>
        /// <param name="login">User</param>
        /// <returns>Added user</returns>
        Login AddUser(Login login);

        /// <summary>
        /// Get user by GUID
        /// </summary>
        /// <param name="guid">GUID</param>
        /// <returns>User</returns>
        Login GetUserByGuid(string guid);

        /// <summary>
        /// Get User By Email
        /// </summary>
        /// <param name="email">Email address</param>
        /// <returns>User</returns>
        Login GetUserByEmail(string email);

        /// <summary>
        /// Get gangs by user ID
        /// </summary>
        /// <param name="guid">GUID</param>
        /// <returns>List og gangs belonging to that user</returns>
        IEnumerable<Gang> GetGangsByUserGuid(string guid);
    }
}
