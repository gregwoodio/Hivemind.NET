using Hivemind.Entities;
using Hivemind.Enums;
using Hivemind.Exceptions;
using Hivemind.Services;
using NUnit.Framework;
using Microsoft.Practices.Unity;
using System;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using Hivemind.Contracts;

namespace Hivemind.Tests.Steps
{
    [Binding]
    public class ExperienceSteps
    {
        private IExperienceService _experienceService;

        public ExperienceSteps()
        {
            var container = Container.GetContainer();
            _experienceService = container.Resolve<IExperienceService>();
        }

        [Given(@"my gang has a rating of (.*)")]
        public void GivenMyGangHasARatingOf(int myGangRating)
        {
            ScenarioContext.Current.Add("GangRating", myGangRating);
        }

        [Given(@"a ganger has downed (.*) opponents")]
        public void GivenAGangerHasDownedOpponents(int downed)
        {
            ScenarioContext.Current.Add("DownedOpponents", downed);
        }
        
        [Given(@"a ganger collects (.*) objectives")]
        public void GivenAGangerCollectsObjectives(int objectives)
        {
            ScenarioContext.Current.Add("Objectives", objectives);
        }

        [Given(@"a battle report as follows:")]
        public void GivenABattleReportAsFollows(Table table)
        {
            var battleReport = table.CreateInstance<BattleReport>();
            ScenarioContext.Current.Add("BattleReport", battleReport);
        }

        [Given(@"a ganger with experience as follows:")]
        public void GivenAGangerWithExperienceAsFollows(Table table)
        {
            var ganger = table.CreateInstance<Ganger>();
            ScenarioContext.Current.Add("Ganger", ganger);
        }

        [When(@"I calculate the underdog bonus")]
        public void WhenICalculateTheUnderdogBonus()
        {
            var battleReport = ScenarioContext.Current.Get<BattleReport>("BattleReport");
            var gangRating = ScenarioContext.Current.Get<int>("GangRating");
            var result = _experienceService.GetUnderdogBonus(gangRating, battleReport.OpponentGangRating, battleReport.HasWon);

            ScenarioContext.Current.Add("Result", result);
        }
        
        [When(@"I calculate the wounding hit bonus")]
        public void WhenICalculateTheWoundingHitBonus()
        {
            var downedOpponents = ScenarioContext.Current.Get<int>("DownedOpponents");
            ScenarioContext.Current.Add("Result", _experienceService.GetWoundingHitBonus(downedOpponents));
        }
        
        [When(@"I calculate the leader's bonus")]
        public void WhenICalculateTheLeaderSBonus()
        {
            var ganger = ScenarioContext.Current.Get<Ganger>("Ganger");
            var battleReport = ScenarioContext.Current.Get<BattleReport>("BattleReport");
            ScenarioContext.Current.Add("Result", _experienceService.GetLeaderBonus(ganger, battleReport.GameType, battleReport.HasWon, battleReport.IsAttacker));
        }
        
        [When(@"the ganger gets (.*) experience")]
        public void WhenTheGangerGetsExperience(int experience)
        {
            var ganger = ScenarioContext.Current.Get<Ganger>("Ganger");
            ScenarioContext.Current.Add("Result", _experienceService.GetNumberOfAdvanceRolls(ganger, experience));
        }
        
        [When(@"I calculate the objective bonus")]
        public void WhenICalculateTheObjectiveBonus()
        {
            var objectives = ScenarioContext.Current.Get<int>("Objectives");
            var battleReport = ScenarioContext.Current.Get<BattleReport>("BattleReport");
            ScenarioContext.Current.Add("Result", _experienceService.GetObjectivesBonus(objectives, battleReport.GameType));
        }

        [When(@"I calculate the winning bonus")]
        public void WhenICalculateTheWinningBonus()
        {
            var battleReport = ScenarioContext.Current.Get<BattleReport>("BattleReport");
            ScenarioContext.Current.Add("Result", _experienceService.GetWinningBonus(battleReport.HasWon, battleReport.GameType));
        }

        [Then(@"the experience result should be (.*)")]
        public void ThenTheExperienceResultShouldBe(int value)
        {
            var result = ScenarioContext.Current.Get<int>("Result");
            Assert.AreEqual(value, result);
        }

        ////private GameType ParseGameTypeString(string gameType)
        ////{
        ////    GameType value;
        ////    Enum.TryParse(gameType.ToUpperInvariant(), out value);
        ////    if (value == 0)
        ////    {
        ////        HivemindException.NoSuchInjuryException(gameType);
        ////    }
        ////    return value;
        ////}
    }
}
