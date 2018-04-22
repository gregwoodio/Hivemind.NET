using Hivemind.Contracts;
using Hivemind.Entities;
using Hivemind.Managers;
using Hivemind.Managers.Implementation;
using Hivemind.Providers;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hivemind.Tests.Managers
{
    [TestFixture]
    public class UserManagerTest
    {
        private Mock<IUserProvider> _userProvider;
        private IUserManager _userManager;

        [SetUp]
        public void Setup()
        {
            _userProvider = new Mock<IUserProvider>();

            _userManager = new UserManager(_userProvider.Object);
        }

        [TestCase(null, null)]
        [TestCase("email@email.com", null)]
        [TestCase(null, "password")]
        public void ValidateInputTest(string email, string password)
        {
            Assert.Throws<ArgumentNullException>(() => _userManager.Login(new Login()
            {
                Email = email,
                Password = password
            }));
        }

        [TestCase()]
        public void LoginTest()
        {
            var login = new Login()
            {
                Email = "abc@email.com",
                Password = "password"
            };

            var fromDb = new Login()
            {
                Email = "abc@email.com",
                Password = BCrypt.Net.BCrypt.HashPassword("password"),
                UserGUID = "A"
            };

            _userProvider.Setup(m => m.GetUserByEmail(It.IsAny<string>())).Returns(fromDb);

            var contract = _userManager.Login(login);
            Assert.AreEqual("abc@email.com", contract.Email);
            _userProvider.Verify(m => m.GetGangsByUserGuid("A"), Times.Once);
        }

        [Test]
        public void RegisterUserTest()
        {
            var login = new Login()
            {
                Email = "abc@email.com",
                Password = "password",
            };

            var contract = _userManager.RegisterUser(login);
            Assert.AreEqual("abc@email.com", contract.Email);
            _userProvider.Verify(m => m.AddUser(login), Times.Once);
        }

        [Test]
        public void GetUserTest()
        {
            var fromDb = new Login()
            {
                Email = "abc@email.com",
                Password = "password",
                UserGUID = "A"
            };

            _userProvider.Setup(m => m.GetUserByGuid("A")).Returns(fromDb);

            var contract = _userManager.GetUser("A");
            Assert.NotNull(contract);
            Assert.AreEqual("abc@email.com", contract.Email);
            Assert.AreEqual("A", contract.UserGUID);
        }
    }
}
