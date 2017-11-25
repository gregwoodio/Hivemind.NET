using System;
using System.Collections.Generic;

namespace Hivemind.Contracts
{
    public class User
    {
        public string Email { get; set; }
        public string UserGUID { get; set; }
        public IEnumerable<string> UserGangIds { get; set; }

        public User()
        {
            UserGangIds = new string[0];
        }
    }
}
