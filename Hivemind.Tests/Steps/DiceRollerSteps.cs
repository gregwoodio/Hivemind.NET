using Hivemind.Utilities;
using NUnit.Framework;
using System;
using TechTalk.SpecFlow;

namespace Hivemind.Tests.Steps
{
    [Binding]
    public class DiceRollerSteps
    {
        public int Result;

        [Given(@"no setup")]
        public void GivenNoSetup()
        {
            // nothing to do
        }
        
        [When(@"I roll a die")]
        public void WhenIRollADie()
        {
            Result = DiceRoller.RollDie();
        }
        
        [When(@"I roll (.*) dice with (.*) sides")]
        public void WhenIRollDiceWithSides(int number, int sides)
        {
            Result = DiceRoller.RollDice(sides, number);
        }
        
        [When(@"I roll D(.*)")]
        public void WhenIRollD(int d66)
        {
            Result = DiceRoller.RollD66();
        }
        
        [Then(@"the result should be between (.*) and (.*)")]
        public void ThenTheResultShouldBeBetweenAnd(int low, int high)
        {
            if (high < low)
            {
                throw new ArgumentException("First argument should be less than second argument");
            }
            Assert.IsTrue(Result >= low && Result <= high);
        }

        [Then(@"the result should be between valid for a D(.*)\.")]
        public void ThenTheResultShouldBeBetweenValidForAD_(int d66)
        {
            // D66 means the first roll is the 10s column, and second is 1s. So results will
            // be between 11-16, 21-26, 31-36, etc. We'll check that the two rolls are within
            // those bounds.
            Assert.IsTrue(Result / 10 >= 1 && Result / 10 <= 6);
            Assert.IsTrue(Result % 10 >= 1 && Result % 10 <= 6);
        }
    }
}
