using Hivemind.Entities;
using Hivemind.Enums;
using Hivemind.Exceptions;
using Hivemind.Services;
using NUnit.Framework;
using Microsoft.Practices.Unity;
using System;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Hivemind.Tests.Steps
{
    [Binding]
    public class ExperienceSteps
    {
        bool hasWon;
        bool isAttacker;
        public int Result;
        public Ganger ganger;
        int myGangRating;
        int opponentGangRating;
        int downedOpponents;
        int objectives;
        int experience;
        GameType gameType;
        private IExperienceService _experienceService;

        public ExperienceSteps()
        {
            var container = Container.GetContainer();
            _experienceService = container.Resolve<IExperienceService>();
        }

        [Given(@"my gang has a rating of (.*) and the opponent has a rating of (.*)")]
        public void GivenMyGangHasARatingOfAndTheOpponentHasARatingOf(int myGangRating, int opponentGangRating)
        {
            this.myGangRating = myGangRating;
            this.opponentGangRating = opponentGangRating;
        }
        
        [Given(@"my gang won the match")]
        public void GivenMyGangWonTheMatch()
        {
            hasWon = true;
        }
        
        [Given(@"my gang lost the match")]
        public void GivenMyGangLostTheMatch()
        {
            hasWon = false;
        }
        
        [Given(@"a ganger has downed (.*) opponents")]
        public void GivenAGangerHasDownedOpponents(int downed)
        {
            this.downedOpponents = downed;
        }
        
        [Given(@"the game type was '(.*)'")]
        public void GivenTheGameTypeWas(string gameType)
        {
            this.gameType = ParseGameTypeString(gameType);
        }
        
        [Given(@"my gang was not the attacker")]
        public void GivenMyGangWasNotTheAttacker()
        {
            isAttacker = false;
        }
        
        [Given(@"a ganger collects (.*) objectives")]
        public void GivenAGangerCollectsObjectives(int objectives)
        {
            this.objectives = objectives;
        }
        
        [Given(@"a ganger participated in the battle")]
        public void GivenAGangerParticipatedInTheBattle()
        {
            // nothing to do
        }

        [Given(@"a ganger with experience as follows:")]
        public void GivenAGangerWithExperienceAsFollows(Table table)
        {
            ganger = table.CreateInstance<Ganger>();
        }

        [When(@"I calculate the underdog bonus")]
        public void WhenICalculateTheUnderdogBonus()
        {
            Result = _experienceService.GetUnderdogBonus(myGangRating, opponentGangRating, hasWon); 
        }
        
        [When(@"I calculate the wounding hit bonus")]
        public void WhenICalculateTheWoundingHitBonus()
        {
            Result = _experienceService.GetWoundingHitBonus(downedOpponents);
        }
        
        [When(@"I calculate the leader's bonus")]
        public void WhenICalculateTheLeaderSBonus()
        {
            Result = _experienceService.GetLeaderBonus(ganger, gameType, hasWon, isAttacker);
        }
        
        [When(@"the ganger gets (.*) experience")]
        public void WhenTheGangerGetsExperience(int experience)
        {
            this.Result = _experienceService.GetNumberOfAdvanceRolls(ganger, experience);
        }
        
        [When(@"I calculate the objective bonus")]
        public void WhenICalculateTheObjectiveBonus()
        {
            Result = _experienceService.GetObjectivesBonus(objectives, gameType);
        }

        [When(@"I calculate the winning bonus")]
        public void WhenICalculateTheWinningBonus()
        {
            Result = _experienceService.GetWinningBonus(hasWon, gameType);
        }

        [Then(@"the experience result should be (.*)")]
        public void ThenTheExperienceResultShouldBe(int value)
        {
            Assert.AreEqual(value, Result);
        }

        private GameType ParseGameTypeString(string gameType)
        {
            GameType value;
            Enum.TryParse(gameType.ToUpperInvariant(), out value);
            if (value == 0)
            {
                HivemindException.NoSuchInjuryException(gameType);
            }
            return value;
        }
    }
}
