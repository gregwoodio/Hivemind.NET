using Hivemind.Entities;
using System;
using System.Collections.Generic;

namespace Hivemind.Contracts
{
    public class User
    {
        public string Email { get; set; }
        public string UserGUID { get; set; }
        public IEnumerable<Gang> UserGangs { get; set; }

        public User()
        {
            UserGangs = new Gang[0];
        }
    }
}
