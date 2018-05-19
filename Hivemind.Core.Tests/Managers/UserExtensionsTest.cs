using Hivemind.Entities;
using Hivemind.Managers.Extensions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hivemind.Tests.Managers
{
    [TestFixture]
    public class UserExtensionsTest
    {
        [TestCase]
        public void ToContractTest()
        {
            var guid = Guid.NewGuid();

            var login = new Login()
            {
                Email = "someone@gmail.com",
                UserGUID = guid.ToString()
            };

            var contract = login.ToContract();

            Assert.AreEqual(login.Email, contract.Email);
            Assert.AreEqual(guid.ToString(), contract.UserGUID);
        }
    }
}
