// <copyright file="HivemindProvider.cs" company="weirdvector">
// Copyright (c) weirdvector. All rights reserved.
// </copyright>

namespace Hivemind.Providers
{
    /// <summary>
    /// Hivemind provider
    /// </summary>
    public class HivemindProvider
    {
        private string _connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=HivemindDb;Integrated Security=True";

        /// <summary>
        /// Gets or sets the connection string.
        /// </summary>
        protected string ConnectionString
        {
            get
            {
                return _connectionString;
            }

            set
            {
                _connectionString = value;
            }
        }
    }
}
