using Hivemind.Entities;
using Hivemind.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hivemind.Providers
{
    public class TerritoryProvider: HivemindProvider
    {
        public IEnumerable<Territory> GetAllTerritories()
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

        public Territory GetTerritoryById(TerritoryEnum territory)
        {
            return GetTerritoryById((int)territory);
        }

        public Territory GetTerritoryById(int territoryId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("Territories_GetById", connection))
                {
                    connection.Open();

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@TerritoryId", SqlDbType.Int).Value = territoryId;
                    var reader = command.ExecuteReader();

                    return GetTerritoryFromReader(reader);
                }
            }
        }

        public IEnumerable<GangTerritory> GetGangTerritoryByGangId(string gangId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("Territories_GetByGangId", connection))
                {
                    connection.Open();

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@GangId", SqlDbType.NVarChar, 100).Value = gangId;
                    var reader = command.ExecuteReader();

                    return GetGangTerritoryListFromReader(reader);
                }
            }
        }

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
