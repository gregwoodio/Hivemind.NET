// <copyright file="UserManager.cs" company="weirdvector">
// Copyright (c) weirdvector. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using Hivemind.Entities;
using Hivemind.Exceptions;
using Hivemind.Managers.Extensions;
using Hivemind.Providers;

namespace Hivemind.Managers.Implementation
{
    /// <summary>
    /// User manager
    /// </summary>
    public class UserManager : IUserManager
    {
        private IUserProvider _userProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserManager"/> class.
        /// </summary>
        /// <param name="userProvider">User provider</param>
        public UserManager(IUserProvider userProvider)
        {
            _userProvider = userProvider ?? throw new ArgumentNullException(nameof(userProvider));
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="user">The login credentials</param>
        /// <returns>Logged in user</returns>
        public Contracts.User Login(Login user)
        {
            ValidateInput(user);

            var userEntity = _userProvider.GetUserByEmail(user.Email);

            AuthenticateUser(user, userEntity);

            var userContract = userEntity.ToContract();

            GetUserGangs(ref userContract);

            return userContract;
        }

        /// <summary>
        /// Register a new user
        /// </summary>
        /// <param name="user">The credentials to register</param>
        /// <returns>Registered user</returns>
        public Contracts.User RegisterUser(Login user)
        {
            ValidateInput(user);

            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            _userProvider.AddUser(user);

            return user.ToContract();
        }

        /// <summary>
        /// Get a user by ID
        /// </summary>
        /// <param name="userGuid">User GUID</param>
        /// <returns>User</returns>
        public Contracts.User GetUser(string userGuid)
        {
            if (string.IsNullOrEmpty(userGuid))
            {
                HivemindException.InvalidUsernameOrPassword();
            }

            var user = _userProvider.GetUserByGuid(userGuid);
            var userContract = user.ToContract();
            GetUserGangs(ref userContract);

            return userContract;
        }

        /// <summary>
        /// Get user gangs
        /// </summary>
        /// <param name="userGuid">User GUID</param>
        /// <returns>Gangs belonging to specified user</returns>
        public IEnumerable<Gang> GetUserGangs(string userGuid)
        {
            return _userProvider.GetGangsByUserGuid(userGuid);
        }

        private void AuthenticateUser(Entities.Login user, Entities.Login userFromDatabase)
        {
            if (user == null || user.Email == null || userFromDatabase == null)
            {
                HivemindException.InvalidUsernameOrPassword();
            }

            if (!BCrypt.Net.BCrypt.Verify(user.Password, userFromDatabase.Password))
            {
                HivemindException.InvalidUsernameOrPassword();
            }
        }

        private void ValidateInput(Entities.Login user)
        {
            if (string.IsNullOrEmpty(user.Email))
            {
                throw new ArgumentNullException("Email");
            }

            if (string.IsNullOrEmpty(user.Password))
            {
                throw new ArgumentNullException("Password");
            }
        }

        private void GetUserGangs(ref Contracts.User user)
        {
            user.UserGangs = _userProvider.GetGangsByUserGuid(user.UserGUID);
        }
    }
}
