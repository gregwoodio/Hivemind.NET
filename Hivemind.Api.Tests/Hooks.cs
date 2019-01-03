using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Hivemind.Api.Tests
{
    public class Hooks
    {
        [BeforeFeature]
        public void Before()
        {
            FeatureContext.Current.Add("Aliases", new Dictionary<string, string>());
        }
    }
}
