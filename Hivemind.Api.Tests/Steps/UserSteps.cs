using Hivemind.Contracts;
using Hivemind.Entities;
using NUnit.Framework;
using System;
using System.Linq;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Hivemind.Api.Tests.Steps
{
    [Binding]
    public class UserSteps : CommonSteps
    {
        private const string UserPath = "api/user";
        private const string TokenPath = "api/login";
        private const string AddedUserKey = "AddedUserKey";
        private const string TokenKey = "TokenKey";
        private const string GuidKey = "Guid";

        public UserSteps(Context context)
            : base(context)
        {
        }

        [When(@"I add a user as follows:")]
        public void WhenIAddAUserAsFollows(Table table)
        {
            var login = table.CreateInstance<Login>();
            var guid = Guid.NewGuid().ToString();
            ScenarioContext.Current.Add(GuidKey, guid);

            login.Email = login.Email + guid;

            ////var httpClient = new HttpClient();
            ////httpClient.PostAsync()
            var user = _context.Post<Login, User, object>(UserPath, login);
            ScenarioContext.Current.Add(AddedUserKey, user);
        }

        [When(@"I get user information")]
        public void WhenIGetUserInformation()
        {
            _context.Get<User>(UserPath);
        }

        [When(@"I retrieve token with using:")]
        public void WhenIRetrieveTokenWithUsing(Table table)
        {
            var login = table.CreateInstance<Login>();
            var guid = ScenarioContext.Current.Get<string>(GuidKey);
            login.Email = login.Email + guid;

            var jsonResponse = _context.Post<Login, object, object>(TokenPath, login);
            ScenarioContext.Current.Add(TokenKey, jsonResponse);
        }

        [When(@"I get user information with token:")]
        public void WhenIGetUserInformationWithToken(Table table)
        {
            var token = table.Rows.FirstOrDefault().GetString("Token");
            _context.SetTokenHeader(token);

            _context.Get<User>(UserPath);
        }

        [Then(@"I should receive an error as follow:")]
        public void ThenIShouldReceiveAnErrorAsFollow(Table table)
        {
            var expectedErrorMessage = table.ContainsColumn("ErrorMessage") ? table.Rows.FirstOrDefault()?["ErrorMessage"] : throw new ArgumentException();

            var error = _context.LastResult;

            Assert.AreEqual(expectedErrorMessage, error);
        }

        [Then(@"the user should be added")]
        public void ThenTheUserShouldBeAdded()
        {
            var user = ScenarioContext.Current.Get<User>(AddedUserKey);

            Assert.NotNull(user);
            Assert.NotNull(user.UserGUID);
        }

        [Then(@"I should receive a token")]
        public void ThenIShouldReceiveAToken()
        {
            var token = ScenarioContext.Current.Get<object>(TokenKey);

            Assert.NotNull(token);
        }
    }
}
