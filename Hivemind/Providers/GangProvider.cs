using Hivemind.Contracts;
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
    public class GangProvider: HivemindProvider
    {
        public Gang GetGangById(int gangId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("Gangs_GetById", connection))
                {
                    connection.Open();

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@GangId", SqlDbType.Int).Value = gangId;
                    var reader = command.ExecuteReader();

                    return GetGangFromReader(reader);
                }
            }
        }

        public Gang UpdateGang(Gang gang)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("Gangs_UpdateGang", connection))
                {
                    connection.Open();

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@GangId", SqlDbType.Int).Value = gang.GangId;
                    command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = gang.Name;
                    command.Parameters.Add("@House", SqlDbType.Int).Value = (int)gang.House;
                    command.Parameters.Add("@Credits", SqlDbType.Int).Value = gang.Credits;
                    command.ExecuteNonQuery();

                    return GetGangById(gang.GangId);
                }
            }
        }

        private Gang GetGangFromReader(SqlDataReader reader)
        {
            var gang = new Gang();
            reader.Read();

            var value = reader.GetOrdinal("gangId");
            gang.GangId = reader.GetInt32(value);

            value = reader.GetOrdinal("gangName");
            gang.Name = reader.GetString(value);

            value = reader.GetOrdinal("house");
            gang.House = (GangHouse)reader.GetInt32(value);

            value = reader.GetOrdinal("credits");
            gang.Credits = reader.GetInt32(value);

            gang.Gangers = new GangerProvider().GetByGangId(gang.GangId);

            return gang;
        }
    }
}
