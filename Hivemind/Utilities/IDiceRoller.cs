// <copyright file="IDiceRoller.cs" company="weirdvector">
// Copyright (c) weirdvector. All rights reserved.
// </copyright>

namespace Hivemind.Utilities
{
    /// <summary>
    /// DiceRoller interface
    /// </summary>
    public interface IDiceRoller
    {
        /// <summary>
        /// Roll a standard D6 die.
        /// </summary>
        /// <returns>Result</returns>
        int RollDie();

        /// <summary>
        /// Roll a die with a specified number of sides.
        /// </summary>
        /// <param name="numberOfSides">Number of sides</param>
        /// <returns>Result</returns>
        int RollDie(int numberOfSides);

        /// <summary>
        /// Roll dice with specified number of sides.
        /// </summary>
        /// <param name="numberOfSides">Number of sides</param>
        /// <param name="numberOfDice">Number of dice</param>
        /// <returns>Result</returns>
        int RollDice(int numberOfSides, int numberOfDice);

        /// <summary>
        /// Roll a D66.
        /// </summary>
        /// <returns>Result</returns>
        int RollD66();

        /// <summary>
        /// Roll a D66, specifying the seed. Used for rare item rolls to prevent refreshing the 
        /// page to find new, better rares.
        /// </summary>
        /// <param name="seed">Seed for random</param>
        /// <returns>Result</returns>
        int RollD66(int seed);

        /// <summary>
        /// Do the multiple injuries roll.
        /// </summary>
        /// <returns>Result</returns>
        int MultipleInjuriesRoll();

        /// <summary>
        /// Parse a dice string to a result (D6).
        /// </summary>
        /// <param name="dice">Dice string</param>
        /// <returns>Result</returns>
        int ParseDiceString(string dice);
    }
}
