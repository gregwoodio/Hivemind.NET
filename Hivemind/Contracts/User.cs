using System;
using System.Collections.Generic;

namespace Hivemind.Contracts
{
    public class User
    {
        public string Username { get; set; }
        public Guid UserGUID { get; set; }
        public IEnumerable<string> UserGangIds { get; set; }
    }
}
