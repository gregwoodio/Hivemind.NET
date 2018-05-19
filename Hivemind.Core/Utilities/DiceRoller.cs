// <copyright file="DiceRoller.cs" company="weirdvector">
// Copyright (c) weirdvector. All rights reserved.
// </copyright>

using System;
using System.Linq;

namespace Hivemind.Utilities
{
    /// <summary>
    /// Dice roller implementation.
    /// </summary>
    public class DiceRoller : IDiceRoller
    {
        private static int _seed;

        /// <summary>
        /// Roll a standard D6 die.
        /// </summary>
        /// <returns>Result</returns>
        public int RollDie()
        {
            return RollDice(6, 1);
        }

        /// <summary>
        /// Roll a die with a specified number of sides.
        /// </summary>
        /// <param name="numberOfSides">Number of sides</param>
        /// <returns>Results</returns>
        public int RollDie(int numberOfSides)
        {
            return RollDice(numberOfSides, 1);
        }

        /// <summary>
        /// Roll dice with specified number of sides.
        /// </summary>
        /// <param name="numberOfSides">Number of sides</param>
        /// <param name="numberOfDice">Number of dice</param>
        /// <returns>Results</returns>
        public int RollDice(int numberOfSides, int numberOfDice)
        {
            if (numberOfSides < 2)
            {
                throw new ArgumentException("Dice must have at least two sides.");
            }

            if (numberOfDice < 1)
            {
                throw new ArgumentException("Must roll at least one die.");
            }

            var random = new Random(_seed);
            _seed = random.Next();

            return new int[numberOfDice].Select(die => random.Next(numberOfSides) + 1).Sum();
        }

        /// <summary>
        /// Roll a D66.
        /// </summary>
        /// <returns>Result</returns>
        public int RollD66()
        {
            var random = new Random(_seed);
            _seed = random.Next();

            return ((random.Next(6) + 1) * 10) + (random.Next(6) + 1);
        }

        /// <summary>
        /// Roll a D66, specifying the seed. Used for rare item rolls to prevent refreshing the 
        /// page to find new, better rares.
        /// </summary>
        /// <param name="seed">Seed for random</param>
        /// <returns>Result</returns>
        public int RollD66(int seed)
        {
            var random = new Random(seed);

            return ((random.Next(6) + 1) * 10) + (random.Next(6) + 1);
        }

        /// <summary>
        /// Do the multiple injuries roll.
        /// </summary>
        /// <returns>Result</returns>
        public int MultipleInjuriesRoll()
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

        /// <summary>
        /// Parse a dice string to a result (D6).
        /// </summary>
        /// <param name="dice">Dice string</param>
        /// <returns>Result</returns>
        public int ParseDiceString(string dice)
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

        private int ParseDieString(string die)
        {
            var parts = die.Split(new[] { 'D' }, StringSplitOptions.RemoveEmptyEntries);

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
