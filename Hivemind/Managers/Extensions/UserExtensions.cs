// <copyright file="UserExtensions.cs" company="weirdvector">
// Copyright (c) weirdvector. All rights reserved.
// </copyright>

namespace Hivemind.Managers.Extensions
{
    /// <summary>
    /// User extensions
    /// </summary>
    public static class UserExtensions
    {
        /// <summary>
        /// Converts a login into a user
        /// </summary>
        /// <param name="login">The login</param>
        /// <returns>User</returns>
        public static Contracts.User ToContract(this Entities.Login login)
        {
            return new Contracts.User()
            {
                Email = login.Email,
                UserGUID = login.UserGUID,
            };
        }
    }
}
