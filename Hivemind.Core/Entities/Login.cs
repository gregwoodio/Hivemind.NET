// <copyright file="Login.cs" company="weirdvector">
// Copyright (c) weirdvector. All rights reserved.
// </copyright>

namespace Hivemind.Entities
{
    /// <summary>
    /// Login
    /// </summary>
    public class Login
    {
        /// <summary>
        /// Gets or sets the UserId
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the Password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the UserGUID
        /// </summary>
        public string UserGUID { get; set; }
    }
}
