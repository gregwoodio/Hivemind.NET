using Hivemind.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hivemind.Providers
{
    public class UserProvider : HivemindProvider
    {
        public Login AddUser(Login login)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("Users_Add", connection))
                {
                    connection.Open();

                    command.CommandType = CommandType.StoredProcedure;
                    var userGuid = command.Parameters.Add("@UserGUID", SqlDbType.NVarChar, 100);
                    userGuid.Direction = ParameterDirection.Output;
                    command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = login.Email;
                    command.Parameters.Add("@Password", SqlDbType.NVarChar).Value = login.Password;
                    command.ExecuteNonQuery();

                    login.UserGUID = (string)userGuid.Value;

                    return login;
                }
            }
        }

        public Login GetUserByGuid(string guid)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("Users_GetUserByGuid", connection))
                {
                    connection.Open();

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@UserGUID", SqlDbType.NVarChar, 100).Value = guid.ToString();
                    var reader = command.ExecuteReader();

                    return GetUserFromReader(reader);
                }
            }
        }

        public Login GetUserByEmail(string email)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("Users_GetByEmail", connection))
                {
                    connection.Open();

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@Email", SqlDbType.NVarChar, 100).Value = email;
                    var reader = command.ExecuteReader();

                    return GetUserFromReader(reader);
                }
            }
        }

        public IEnumerable<string> GetGangsByUserGuid(string guid)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("UserGangs_GetByUserGuid", connection))
                {
                    connection.Open();

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@UserGUID", SqlDbType.NVarChar, 100).Value = guid.ToString();
                    var reader = command.ExecuteReader();

                    var gangList = new List<string>();
                    while (reader.Read())
                    {
                        var value = reader.GetOrdinal("gangId");
                        gangList.Add(reader.GetString(value));
                    }

                    return gangList;
                }
            }
        }

        private Login GetUserFromReader(SqlDataReader reader)
        {
            var user = new Login();
            if (reader.Read())
            {
                var value = reader.GetOrdinal("userId");
                user.UserId = reader.GetInt32(value);

                value = reader.GetOrdinal("email");
                user.Email = reader.GetString(value);

                value = reader.GetOrdinal("password");
                user.Password = reader.GetString(value);

                value = reader.GetOrdinal("userGUID");
                user.UserGUID = reader.GetString(value);
            }
            else
            {
                return null;
            }

            return user;
        }
    }
}
