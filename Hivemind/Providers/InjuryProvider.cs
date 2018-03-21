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
    public class InjuryProvider : HivemindProvider
    {
        public Injury GetInjuryById(int injuryId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("Injuries_GetById", connection))
                {
                    connection.Open();

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@InjuryId", SqlDbType.Int).Value = injuryId;

                    var reader = command.ExecuteReader();
                    return GetInjuryFromReader(reader);
                }
            }
        }

        public IEnumerable<Injury> GetAllInjuries()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("Injuries_GetAll", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        var injuries = new List<Injury>();

                        while (reader.Read())
                        {
                            var injury = new Injury();

                            var value = reader.GetOrdinal("injuryId");
                            injury.InjuryId = (InjuryEnum)reader.GetInt32(value);

                            value = reader.GetOrdinal("injuryName");
                            injury.Name = reader.GetString(value);

                            value = reader.GetOrdinal("description");
                            injury.Description = reader.GetString(value);

                            injuries.Add(injury);
                        }

                        return injuries;
                    }
                }
            }
        }

        public IEnumerable<Injury> GetInjuriesByGangerId(string gangerId)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("Injuries_GetByGangerId", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@GangerId", SqlDbType.Int).Value = gangerId;

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    var injuries = new List<Injury>();
                    Injury injury;

                    while ((injury = GetInjuryFromReader(reader)) != null)
                    {
                        injuries.Add(injury);
                    }

                    return injuries;
                }
            }
        }

        public IEnumerable<GangerInjury> GetInjuriesByGangId(string gangId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("Injuries_GetByGangId", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@GangId", SqlDbType.NVarChar, 100).Value = gangId;

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        var injuries = new List<GangerInjury>();
                        GangerInjury gangerInjury;

                        while (reader.Read())
                        {
                            gangerInjury = new GangerInjury();
                            gangerInjury.Injury = new Injury();

                            var value = reader.GetOrdinal("injuryId");
                            gangerInjury.Injury.InjuryId = (InjuryEnum)reader.GetInt32(value);

                            value = reader.GetOrdinal("injuryName");
                            gangerInjury.Injury.Name = reader.GetString(value);

                            value = reader.GetOrdinal("description");
                            gangerInjury.Injury.Description = reader.GetString(value);

                            value = reader.GetOrdinal("gangerId");
                            gangerInjury.GangerId = reader.GetString(value);

                            injuries.Add(gangerInjury);
                        }

                        return injuries;
                    }
                }
            }
        }

        private Injury GetInjuryFromReader(SqlDataReader reader)
        {
            var injury = new Injury();
            if (reader.Read())
            {
                var value = reader.GetOrdinal("injuryId");
                injury.InjuryId = (InjuryEnum)reader.GetInt32(value);

                value = reader.GetOrdinal("injuryName");
                injury.Name = reader.GetString(value);

                value = reader.GetOrdinal("description");
                injury.Description = reader.GetString(value);
            }
            else
            {
                return null;
            }

            return injury;
        }
    }
}
