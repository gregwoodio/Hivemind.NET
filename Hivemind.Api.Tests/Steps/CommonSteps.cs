using NUnit.Framework;
using System;
using System.Configuration;
using System.Linq;
using System.Net;
using TechTalk.SpecFlow;

namespace Hivemind.Api.Tests.Steps
{
    [Binding]
    public class CommonSteps : Steps
    {
        public CommonSteps(Context context) 
            : base(context)
        {
        }

        [Then(@"I should receive an error as follows:")]
        public void ThenIShouldReceiveAnErrorAsFollow(Table table)
        {
            var expectedErrorValue = table.ContainsColumn("StatusCode") 
                ? table.Rows.FirstOrDefault()?["StatusCode"] 
                : throw new ArgumentException();

            Enum.TryParse(expectedErrorValue, out HttpStatusCode expectedErrorStatus);
            var errorStatus = _context.LastError.StatusCode;

            Assert.AreEqual(expectedErrorStatus, errorStatus);
        }
    }
}
