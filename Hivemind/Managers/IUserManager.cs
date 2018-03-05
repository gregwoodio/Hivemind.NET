using Hivemind.Entities;
using System.Collections.Generic;

namespace Hivemind.Managers
{
    public interface IUserManager
    {
        /// <summary>
        /// Register a new user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Contracts.User RegisterUser(Entities.Login user);

        /// <summary>
        /// Login an existing user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Contracts.User Login(Entities.Login user);

        /// <summary>
        /// Gets user by GUID.
        /// </summary>
        /// <param name="userGuid"></param>
        /// <returns></returns>
        Contracts.User GetUser(string userGuid);

        /// <summary>
        /// Gets the gang IDs of a given user.
        /// </summary>
        /// <param name="userGuid"></param>
        /// <returns></returns>
        IEnumerable<Gang> GetUserGangs(string userGuid);
    }
}
