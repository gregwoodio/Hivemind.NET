using Hivemind.Contracts;
using Hivemind.Entities;
using NUnit.Framework;
using System.Resources;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using WebApi.Controllers;

namespace Hivemind.Api.Tests.Steps
{
    [Binding]
    public class UserSteps : CommonSteps
    {
        private const string UserPath = "api/user";
        private const string AddedUserKey = "AddedUserKey";

        public UserSteps(Context context)
            : base(context)
        {
        }

        [When(@"I add a user as follows:")]
        public void WhenIAddAUserAsFollows(Table table)
        {
            var login = table.CreateInstance<Login>();

            var user = _context.Post<Login, User, object>(UserPath, login);
            ScenarioContext.Current.Add(AddedUserKey, user);
        }

        [When(@"I get user information")]
        public void WhenIGetUserInformation()
        {
            _context.Get<User>("/api/user");
        }

        [Then(@"I should receive an error as follow:")]
        public void ThenIShouldReceiveAnErrorAsFollow(Table table)
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"the user should be added")]
        public void ThenTheUserShouldBeAdded()
        {
            var user = ScenarioContext.Current.Get<User>(AddedUserKey);

            Assert.NotNull(user);
            Assert.NotNull(user.UserGUID);
        }
    }
}
