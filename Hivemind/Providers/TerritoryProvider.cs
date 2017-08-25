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
        public IEnumerable<Territory> GetTerritoryByGangId(int gangId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("Territories_GetByGangId", connection))
                {
                    connection.Open();

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@GangId", SqlDbType.Int).Value = gangId;
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

        public void UpdateGangTerritory(int gangId, Territory territory)
        {
            // TODO:
        }

        public void InsertGangTerritory(int gangId, Territory territory)
        {
            // TODO
        }

        private IEnumerable<Territory> GetTerritoryListFromReader(SqlDataReader reader)
        {
            var territories = new List<Territory>();

            while (reader.Read())
            {
                territories.Add(GetTerritoryFromReader(reader));
            }

            return territories;
        }

        private Territory GetTerritoryFromReader(SqlDataReader reader)
        {
            var territory = new Territory();
            reader.Read();

            var value = reader.GetOrdinal("name");
            territory.Name = reader.GetString(value);

            value = reader.GetOrdinal("territoryId");
            territory.TerritoryId = reader.GetInt32(value);

            value = reader.GetOrdinal("description");
            territory.Description = reader.GetString(value);

            value = reader.GetOrdinal("income");
            territory.Income = reader.GetString(value);

            return territory;
        }
    }
}
