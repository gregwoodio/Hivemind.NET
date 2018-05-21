// <copyright file="HivemindException.cs" company="weirdvector">
// Copyright (c) weirdvector. All rights reserved.
// </copyright>

using System;

namespace Hivemind.Exceptions
{
    /// <summary>
    /// Hivemind exceptions are generic exceptions thrown for game-related problems.
    /// </summary>
    public class HivemindException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HivemindException"/> class.
        /// </summary>
        /// <param name="error">Description of the error</param>
        public HivemindException(string error)
            : base(error)
        {
        }

        /// <summary>
        /// Ganger not found exception
        /// </summary>
        /// <param name="id">Incorrect ID</param>
        /// <returns>HivemindException</returns>
        public static HivemindException GangerNotFoundException(int id)
        {
            throw new HivemindException($"Ganger with id '{id}' not found.");
        }

        /// <summary>
        /// No such injury exception
        /// </summary>
        /// <param name="id">Incorrect ID</param>
        /// <returns>HivemindException</returns>
        public static HivemindException NoSuchInjuryException(int id)
        {
            throw new HivemindException($"No injury with id '{id}' found.");
        }

        /// <summary>
        /// No such injury exception
        /// </summary>
        /// <param name="name">Incorrect injury name</param>
        /// <returns>HivemindException</returns>
        public static HivemindException NoSuchInjuryException(string name)
        {
            throw new HivemindException($"No injury with name '{name}' found.");
        }

        /// <summary>
        /// No such gang house
        /// </summary>
        /// <returns>HivemindException</returns>
        public static HivemindException NoSuchGangHouse()
        {
            throw new HivemindException("The specified gang house was not known.");
        }

        /// <summary>
        /// Invalid username or password
        /// </summary>
        /// <returns>HivemindException</returns>
        public static HivemindException InvalidUsernameOrPassword()
        {
            throw new HivemindException("Invalid username or password.");
        }
    }
}
