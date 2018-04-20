// <copyright file="TerritoryProvider.cs" company="weirdvector">
// Copyright (c) weirdvector. All rights reserved.
// </copyright>

using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Hivemind.Entities;
using Hivemind.Enums;

namespace Hivemind.Providers
{
    /// <summary>
    /// Territory provider
    /// </summary>
    public class TerritoryProvider : ITerritoryProvider
    {
        private string _connectionString;
        private IEnumerable<Territory> _territories;

        /// <summary>
        /// Initializes a new instance of the <see cref="TerritoryProvider"/> class.
        /// </summary>
        /// <param name="connectionString">Connection string</param>
        public TerritoryProvider(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Get all territories
        /// </summary>
        /// <returns>All territories</returns>
        public IEnumerable<Territory> GetAllTerritories()
        {
            if (_territories == null)
            {
                _territories = GetAllTerritoriesFromDatabase();
            }

            return _territories;
        }

        /// <summary>
        /// Get territory by ID
        /// </summary>
        /// <param name="territory">Territory ID</param>
        /// <returns>Territory</returns>
        public Territory GetTerritoryById(TerritoryEnum territory)
        {
            return GetTerritoryById((int)territory);
        }

        /// <summary>
        /// Get Territory by ID
        /// </summary>
        /// <param name="territoryId">Territory ID</param>
        /// <returns>Territory</returns>
        public Territory GetTerritoryById(int territoryId)
        {
            if (_territories == null)
            {
                _territories = GetAllTerritoriesFromDatabase();
            }

            return _territories.FirstOrDefault(t => t.TerritoryId == territoryId);
        }

        /// <summary>
        /// Get territory by gang ID
        /// </summary>
        /// <param name="gangId">Gang ID</param>
        /// <returns>List of territories</returns>
        public IEnumerable<Territory> GetTerritoryByGangId(string gangId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("Territories_GetByGangId", connection))
                {
                    connection.Open();

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@GangId", SqlDbType.NVarChar, 100).Value = gangId;
                    var reader = command.ExecuteReader();

                    return GetTerritoryListFromReader(reader);
                }
            }
        }

        /// <summary>
        /// Add gang territory
        /// </summary>
        /// <param name="gangTerritory">Gang territory</param>
        /// <returns>Added GangTerritory</returns>
        public GangTerritory AddGangTerritory(GangTerritory gangTerritory)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("GangTerritories_Add", connection))
                {
                    connection.Open();

                    command.CommandType = CommandType.StoredProcedure;
                    var gangTerritoryId = command.Parameters.Add("@GangTerritoryId", SqlDbType.NVarChar, 100);
                    gangTerritoryId.Direction = ParameterDirection.Output;
                    gangTerritoryId.Value = string.Empty;
                    command.Parameters.Add("@GangId", SqlDbType.NVarChar, 100).Value = gangTerritory.GangId;
                    command.Parameters.Add("@TerritoryId", SqlDbType.Int).Value = gangTerritory.Territory.TerritoryId;

                    command.ExecuteNonQuery();
                    gangTerritory.GangTerritoryId = (string)gangTerritoryId.Value;
                    return gangTerritory;
                }
            }
        }

        /// <summary>
        /// Remove gang territory
        /// </summary>
        /// <param name="gangTerritoryId">Gang territory ID</param>
        public void RemoveGangTerritory(string gangTerritoryId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("GangTerritories_Remove", connection))
                {
                    connection.Open();

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@GangTerritoryId", SqlDbType.NVarChar, 100).Value = gangTerritoryId;
                    command.ExecuteNonQuery();
                }
            }
        }

        private IEnumerable<Territory> GetAllTerritoriesFromDatabase()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("Territories_GetAll", connection))
                {
                    connection.Open();

                    command.CommandType = CommandType.StoredProcedure;
                    var reader = command.ExecuteReader();

                    return GetTerritoryListFromReader(reader);
                }
            }
        }

        private IEnumerable<GangTerritory> GetGangTerritoryListFromReader(SqlDataReader reader)
        {
            var territories = new List<GangTerritory>();
            GangTerritory territory;
            while ((territory = GetGangTerritoryFromReader(reader)) != null)
            {
                territories.Add(territory);
            }

            return territories;
        }

        private GangTerritory GetGangTerritoryFromReader(SqlDataReader reader)
        {
            var gangTerritory = new GangTerritory();
            if (reader.Read())
            {
                var value = reader.GetOrdinal("gangId");
                gangTerritory.GangId = reader.GetString(value);

                value = reader.GetOrdinal("gangTerritoryId");
                gangTerritory.GangTerritoryId = reader.GetString(value);

                value = reader.GetOrdinal("description");
                gangTerritory.Territory.Description = reader.GetString(value);

                value = reader.GetOrdinal("name");
                gangTerritory.Territory.Name = reader.GetString(value);

                value = reader.GetOrdinal("income");
                gangTerritory.Territory.Income = reader.GetString(value);

                value = reader.GetOrdinal("territoryId");
                gangTerritory.Territory.TerritoryId = reader.GetInt32(value);
            }
            else
            {
                return null;
            }

            return gangTerritory;
        }

        private IEnumerable<Territory> GetTerritoryListFromReader(SqlDataReader reader)
        {
            var territories = new List<Territory>();
            Territory territory;

            while ((territory = GetTerritoryFromReader(reader)) != null)
            {
                territories.Add(territory);
            }

            return territories;
        }

        private Territory GetTerritoryFromReader(SqlDataReader reader)
        {
            var territory = new Territory();
            if (reader.Read())
            {
                var value = reader.GetOrdinal("name");
                territory.Name = reader.GetString(value);

                value = reader.GetOrdinal("territoryId");
                territory.TerritoryId = reader.GetInt32(value);

                value = reader.GetOrdinal("description");
                territory.Description = reader.GetString(value);

                value = reader.GetOrdinal("income");
                territory.Income = reader.GetString(value);
            }
            else
            {
                return null;
            }

            return territory;
        }
    }
}
