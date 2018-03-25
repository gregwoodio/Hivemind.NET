// <copyright file="UserProvider.cs" company="weirdvector">
// Copyright (c) weirdvector. All rights reserved.
// </copyright>

using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Hivemind.Entities;

namespace Hivemind.Providers
{
    /// <summary>
    /// User provider
    /// </summary>
    public class UserProvider : HivemindProvider, IUserProvider
    {
        /// <summary>
        /// Add user
        /// </summary>
        /// <param name="login">User</param>
        /// <returns>Added user</returns>
        public Login AddUser(Login login)
        {
            using (var connection = new SqlConnection(ConnectionString))
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

        /// <summary>
        /// Get user by GUID
        /// </summary>
        /// <param name="guid">GUID</param>
        /// <returns>User</returns>
        public Login GetUserByGuid(string guid)
        {
            using (var connection = new SqlConnection(ConnectionString))
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

        /// <summary>
        /// Get User By Email
        /// </summary>
        /// <param name="email">Email address</param>
        /// <returns>User</returns>
        public Login GetUserByEmail(string email)
        {
            using (var connection = new SqlConnection(ConnectionString))
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

        /// <summary>
        /// Get gangs by user ID
        /// </summary>
        /// <param name="guid">GUID</param>
        /// <returns>List og gangs belonging to that user</returns>
        public IEnumerable<Gang> GetGangsByUserGuid(string guid)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                using (var command = new SqlCommand("UserGangs_GetByUserGuid", connection))
                {
                    connection.Open();

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@UserGUID", SqlDbType.NVarChar, 100).Value = guid.ToString();
                    var reader = command.ExecuteReader();

                    var gangList = new List<Gang>();
                    while (reader.Read())
                    {
                        var gang = new Gang();

                        var value = reader.GetOrdinal("gangId");
                        gang.GangId = reader.GetString(value);

                        value = reader.GetOrdinal("gangName");
                        gang.Name = reader.GetString(value);

                        gangList.Add(gang);
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
                var value = reader.GetOrdinal("email");
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
