using Hivemind.Utilities;
using NUnit.Framework;
using System;
using TechTalk.SpecFlow;

namespace Hivemind.Tests.Steps
{
    [Binding]
    public class DiceRollerSteps
    {
        [Given(@"no setup")]
        public void GivenNoSetup()
        {
            // nothing to do
        }

        [When(@"I roll a die")]
        public void WhenIRollADie()
        {
            ScenarioContext.Current.Add("Result", DiceRoller.RollDie());
        }
        
        [When(@"I roll (.*) dice with (.*) sides")]
        public void WhenIRollDiceWithSides(int number, int sides)
        {
            ScenarioContext.Current.Add("Result", DiceRoller.RollDice(sides, number));
        }
        
        [When(@"I roll D(.*)")]
        public void WhenIRollD(int d66)
        {
            ScenarioContext.Current.Add("Result", DiceRoller.RollD66());
        }

        [When(@"I parse the dice string '(.*)'")]
        public void WhenIParseTheDiceString(string diceString)
        {
            ScenarioContext.Current.Add("Result", DiceRoller.ParseDiceString(diceString));
        }

        [Then(@"the result should be (.*)")]
        public void ThenTheResultShouldBe(int value)
        {
            Assert.AreEqual(value, ScenarioContext.Current.Get<int>("Result"));
        }


        [Then(@"the result should be between (.*) and (.*)")]
        public void ThenTheResultShouldBeBetweenAnd(int low, int high)
        {
            if (high < low)
            {
                throw new ArgumentException("First argument should be less than second argument");
            }
            var result = ScenarioContext.Current.Get<int>("Result");
            Assert.IsTrue(result >= low && result <= high);
        }

        [Then(@"the result should be valid for a D(.*)\.")]
        public void ThenTheResultShouldBeBetweenValidForAD_(int d66)
        {
            // D66 means the first roll is the 10s column, and second is 1s. So results will
            // be between 11-16, 21-26, 31-36, etc. We'll check that the two rolls are within
            // those bounds.
            var result = ScenarioContext.Current.Get<int>("Result");
            Assert.IsTrue(result / 10 >= 1 && result / 10 <= 6);
            Assert.IsTrue(result % 10 >= 1 && result % 10 <= 6);
        }
    }
}
