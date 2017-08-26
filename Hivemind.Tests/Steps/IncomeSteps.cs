using Hivemind.Entities;
using Hivemind.Services;
using System;
using Microsoft.Practices.Unity;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using NUnit.Framework;

namespace Hivemind.Tests.Steps
{
    [Binding]
    public class IncomeSteps
    {
        public int Income { get; set; }
        public int GangSize { get; set; }
        public int GangRating { get; set; }
        public int OpponentGangRating { get; set; }
        public int Result { get; set; }
        private IIncomeService _incomeService;

        public IncomeSteps()
        {
            var container = Container.GetContainer();
            _incomeService = container.Resolve<IIncomeService>();
        }

        [Given(@"the gang's income is (.*)")]
        public void GivenTheGangSIncomeIs(int income)
        {
            Income = income;
        }
        
        [Given(@"the gang has (.*) members")]
        public void GivenTheGangHasMembers(int size)
        {
            GangSize = size;
        }

        [Given(@"my gang has a rating of (.*)")]
        public void GivenMyGangHasARatingOf(int rating)
        {
            GangRating = rating;
        }

        [Given(@"an opponent gang rating of (.*)")]
        public void GivenAnOpponentGangRatingOf(int opponent)
        {
            OpponentGangRating = opponent;
        }
        
        [When(@"I calculate the gang's upkeep")]
        public void WhenICalculateTheGangSUpkeep()
        {
            Result = _incomeService.GetGangUpkeep(GangSize, Income);
        }
        
        [When(@"I calculate the giant killer bonus")]
        public void WhenICalculateTheGiantKillerBonus()
        {
            Result = _incomeService.GetGiantKillerBonus(GangRating, OpponentGangRating);
        }
        
        [Then(@"the upkeep should be (.*)")]
        public void ThenTheUpkeepShouldBe(int value)
        {
            Assert.AreEqual(value, Result);
        }
        
        [Then(@"the bonus should be (.*)")]
        public void ThenTheBonusShouldBe(int value)
        {
            Assert.AreEqual(value, Result);
        }
    }
}
