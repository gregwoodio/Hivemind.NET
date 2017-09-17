using Hivemind.Entities;
using Hivemind.Services;
using System;
using Microsoft.Practices.Unity;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using NUnit.Framework;
using Hivemind.Contracts;

namespace Hivemind.Tests.Steps
{
    [Binding]
    public class IncomeSteps
    {
        private IIncomeService _incomeService;

        public IncomeSteps()
        {
            var container = Container.GetContainer();
            _incomeService = container.Resolve<IIncomeService>();
        }

        [Given(@"the gang's income is (.*)")]
        public void GivenTheGangSIncomeIs(int income)
        {
            ScenarioContext.Current.Add("Income", income);
        }
        
        [Given(@"the gang has (.*) members")]
        public void GivenTheGangHasMembers(int size)
        {
            ScenarioContext.Current.Add("GangSize", size);
        }
        
        [When(@"I calculate the gang's upkeep")]
        public void WhenICalculateTheGangSUpkeep()
        {
            var gangSize = ScenarioContext.Current.Get<int>("GangSize");
            var income = ScenarioContext.Current.Get<int>("Income");
            ScenarioContext.Current.Add("Result", _incomeService.GetGangUpkeep(gangSize, income));
        }
        
        [When(@"I calculate the giant killer bonus")]
        public void WhenICalculateTheGiantKillerBonus()
        {
            var gangRating = ScenarioContext.Current.Get<int>("GangRating");
            var battleReport = ScenarioContext.Current.Get<BattleReport>("BattleReport");
            ScenarioContext.Current.Add("Result", _incomeService.GetGiantKillerBonus(gangRating, battleReport.OpponentGangRating));
        }
    }
}
