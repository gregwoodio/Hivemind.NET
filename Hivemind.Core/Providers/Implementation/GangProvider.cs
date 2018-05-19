// <copyright file="GangProvider.cs" company="weirdvector">
// Copyright (c) weirdvector. All rights reserved.
// </copyright>

using System.Data;
using System.Data.SqlClient;
using Hivemind.Entities;
using Hivemind.Enums;

namespace Hivemind.Providers
{
    /// <summary>
    /// Gang provider
    /// </summary>
    public class GangProvider : IGangProvider
    {
        private string _connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="GangProvider"/> class.
        /// </summary>
        /// <param name="connectionString">Connection string</param>
        public GangProvider(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Get Gang by ID
        /// </summary>
        /// <param name="gangId">Gang ID</param>
        /// <returns>Gang</returns>
        public Gang GetGangById(string gangId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("Gangs_GetById", connection))
                {
                    connection.Open();

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@GangId", SqlDbType.NVarChar, 100).Value = gangId;
                    var reader = command.ExecuteReader();

                    return GetGangFromReader(reader);
                }
            }
        }

        /// <summary>
        /// Associate a gang to a user
        /// </summary>
        /// <param name="gangId">Gang ID</param>
        /// <param name="userId">User ID</param>
        public void AssociateGangToUser(string gangId, string userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("UserGangs_AssociateGangToUser", connection))
                {
                    connection.Open();

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@GangId", SqlDbType.NVarChar).Value = gangId;
                    command.Parameters.Add("@UserGUID", SqlDbType.NVarChar).Value = userId;
                    command.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Add a gang
        /// </summary>
        /// <param name="gang">Gang</param>
        /// <returns>Added gang</returns>
        public Gang AddGang(Gang gang)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("Gangs_AddGang", connection))
                {
                    connection.Open();

                    command.CommandType = CommandType.StoredProcedure;
                    var gangId = command.Parameters.Add("@GangId", SqlDbType.NVarChar, 100);
                    gangId.Direction = ParameterDirection.Output;
                    command.Parameters.Add("@GangName", SqlDbType.NVarChar).Value = gang.Name;
                    command.Parameters.Add("@House", SqlDbType.Int).Value = (int)gang.GangHouse;
                    command.Parameters.Add("@Credits", SqlDbType.Int).Value = (int)gang.Credits;
                    command.ExecuteNonQuery();

                    gang.GangId = (string)gangId.Value;

                    return gang;
                }
            }
        }

        /// <summary>
        /// Update a gang
        /// </summary>
        /// <param name="gang">Gang</param>
        /// <returns>Updated gang</returns>
        public Gang UpdateGang(Gang gang)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("Gangs_UpdateGang", connection))
                {
                    connection.Open();

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@GangId", SqlDbType.NVarChar, 100).Value = gang.GangId;
                    command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = gang.Name;
                    command.Parameters.Add("@House", SqlDbType.Int).Value = (int)gang.GangHouse;
                    command.Parameters.Add("@Credits", SqlDbType.Int).Value = gang.Credits;
                    command.ExecuteNonQuery();

                    return GetGangById(gang.GangId);
                }
            }
        }

        private Gang GetGangFromReader(SqlDataReader reader)
        {
            var gang = new Gang();
            if (reader.Read())
            {
                var value = reader.GetOrdinal("gangId");
                gang.GangId = reader.GetString(value);

                value = reader.GetOrdinal("gangName");
                gang.Name = reader.GetString(value);

                value = reader.GetOrdinal("house");
                gang.GangHouse = (GangHouse)reader.GetInt32(value);

                value = reader.GetOrdinal("credits");
                gang.Credits = reader.GetInt32(value);
            }

            return gang;
        }
    }
}
