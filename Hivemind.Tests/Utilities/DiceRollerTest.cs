using Hivemind.Utilities;
using NUnit.Framework;

namespace Hivemind.Tests.Utilities
{
    [TestFixture]
    public class DiceRollerTest
    {
        [TestCase]
        public void RollDieTest()
        {
            var result = DiceRoller.RollDie();
            Assert.GreaterOrEqual(result, 1);
            Assert.LessOrEqual(result, 6);
        }

        [TestCase]
        public void Roll2D6Test()
        {
            var result = DiceRoller.RollDice(6, 2);
            Assert.GreaterOrEqual(result, 2);
            Assert.LessOrEqual(result, 12);
        }

        [TestCase]
        public void RollD66Test()
        {
            var result = DiceRoller.RollD66();
            Assert.GreaterOrEqual(result / 10, 1);
            Assert.LessOrEqual(result / 10, 6);
            Assert.GreaterOrEqual(result % 10, 1);
            Assert.LessOrEqual(result % 10, 6);
        }

        [TestCase("D6*10", 10, 60)]
        [TestCase("10", 10, 10)]
        [TestCase("2D6*10", 20, 120)]
        [TestCase("35+3D6", 38, 53)]
        public void ParseDiceStringTest(string diceString, int lowValue, int highValue)
        {
            var result = DiceRoller.ParseDiceString(diceString);
            Assert.GreaterOrEqual(result, lowValue);
            Assert.LessOrEqual(result, highValue);
        }
    }
}
