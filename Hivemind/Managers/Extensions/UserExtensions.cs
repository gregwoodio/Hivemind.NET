using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hivemind.Managers.Extensions
{
    public static class UserExtensions
    {
        public static Contracts.User ToContract(this Entities.Login user)
        {
            return new Contracts.User()
            {
                Email = user.Email,
                UserGUID = user.UserGUID
            };
        }
    }
}
