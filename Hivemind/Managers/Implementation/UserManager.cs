using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hivemind.Contracts;
using Hivemind.Entities;
using Hivemind.Providers;
using Hivemind.Exceptions;
using Hivemind.Managers.Extensions;

namespace Hivemind.Managers.Implementation
{
    public class UserManager : IUserManager
    {
        private UserProvider _userProvider;

        public UserManager(UserProvider userProvider)
        {
            _userProvider = userProvider ?? throw new ArgumentNullException(nameof(userProvider));
        }

        public Contracts.User Login(Entities.Login user)
        {
            ValidateInput(user);

            var userEntity = _userProvider.GetUserByEmail(user.Email);

            AuthenticateUser(user, userEntity);

            var userContract = userEntity.ToContract();

            GetUserGangs(ref userContract);

            return userContract;
        }

        public Contracts.User RegisterUser(Entities.Login user)
        {
            ValidateInput(user);

            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            _userProvider.AddUser(user);

            return user.ToContract();
        }

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

        public IEnumerable<string> GetUserGangs(string userGuid)
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
            user.UserGangIds = _userProvider.GetGangsByUserGuid(user.UserGUID);
        }
    }
}
