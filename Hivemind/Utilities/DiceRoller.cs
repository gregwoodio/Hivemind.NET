using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hivemind.Utilities
{
    public static class DiceRoller
    {
        public static int RollDie()
        {
            return RollDice(6, 1);
        }

        public static int RollDie(int numberOfSides)
        {
            return RollDice(numberOfSides, 1);
        }

        public static int RollDice(int numberOfSides, int numberOfDice)
        {
            if (numberOfSides < 2)
                throw new ArgumentException("Dice must have at least two sides.");

            if (numberOfDice < 1)
                throw new ArgumentException("Must roll at least one die.");

            var random = new Random();

            return new int[numberOfDice].Select(die => random.Next(numberOfSides) + 1).Sum();
        }

        public static int RollD66()
        {
            var random = new Random();
            return (random.Next(6) + 1) * 10 + (random.Next(6) + 1);
        }

        public static int MultipleInjuriesRoll()
        {
            // Rerolling after getting multiple injuries cannot be dead or full recovery, so no 11-16 or 41-55
            // We'll leave out additional Multiple Injuries rolls as well.
            var roll = RollD66();
            while (roll <= 21 || (roll >= 41 && roll <= 55))
            {
                roll = RollD66();
            }
            return roll;
        }

        public static int ParseDiceString(string dice)
        {
            if (dice.Contains('*'))
            {
                var parts = dice.Split('*');
                var product = 1;
                foreach (var part in parts)
                {
                    if (part.Contains('D'))
                    {
                        product *= ParseDieString(part);
                    }
                    else
                    {
                        int.TryParse(part, out int value);
                        product *= value;
                    }
                }
                return product;
            }
            else if (dice.Contains('+'))
            {
                var parts = dice.Split('+');
                var sum = 0;
                foreach (var part in parts)
                {
                    if (part.Contains('D'))
                    {
                        sum += ParseDieString(part);
                    }
                    else
                    {
                        int.TryParse(part, out int value);
                        sum += value;
                    }
                }
                return sum;
            }
            else
            {
                if (dice.Contains('D'))
                {
                    return ParseDieString(dice);
                }
                else
                {
                    int.TryParse(dice, out int value);
                    return value;
                }
            }
        }

        private static int ParseDieString(string die)
        {
            var parts = die.Split('D');

            if (parts.Length == 1)
            {
                int.TryParse(parts[0], out int sides);
                return RollDie(sides);
            }
            else
            {
                int.TryParse(parts[0], out int number);
                int.TryParse(parts[1], out int sides);
                return RollDice(sides, number);
            }
        }
    }
}
