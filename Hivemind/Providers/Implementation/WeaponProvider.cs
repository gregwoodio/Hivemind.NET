// <copyright file="WeaponProvider.cs" company="weirdvector">
// Copyright (c) weirdvector. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Hivemind.Entities;
using Hivemind.Enums;

namespace Hivemind.Providers
{
    /// <summary>
    /// Weapon provider
    /// </summary>
    public class WeaponProvider : IWeaponProvider
    {
        private string _connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="WeaponProvider"/> class.
        /// </summary>
        /// <param name="connectionString">Connection string</param>
        public WeaponProvider(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Get by weapon ID
        /// </summary>
        /// <param name="weaponId">Weapon ID</param>
        /// <returns>Weapon</returns>
        public Weapon GetById(int weaponId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("Weapons_GetById", connection))
                {
                    connection.Open();

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@WeaponId", SqlDbType.Int).Value = weaponId;
                    var reader = command.ExecuteReader();

                    var weapon = GetWeaponFromReader(reader);

                    return weapon;
                }
            }
        }

        /// <summary>
        /// Get all weapons
        /// </summary>
        /// <returns>Returns all weapons</returns>
        public IEnumerable<Weapon> GetAllWeapons()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("Weapons_GetAll", connection))
                {
                    connection.Open();

                    command.CommandType = CommandType.StoredProcedure;
                    var reader = command.ExecuteReader();

                    var weapons = GetWeaponListFromReader(reader);

                    return weapons;
                }
            }
        }

        /// <summary>
        /// Get all ganger weapons for a gang
        /// </summary>
        /// <param name="gangId">Gang ID</param>
        /// <returns>List of ganger weapons</returns>
        public IEnumerable<GangerWeapon> GetByGangId(string gangId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("GangerWeapons_GetByGangId", connection))
                {
                    connection.Open();

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@GangId", SqlDbType.NVarChar, 100).Value = gangId;

                    var reader = command.ExecuteReader();

                    return GetGangerWeaponListFromReader(reader, GetGangerWeaponLimitedInformationFromReader);
                }
            }
        }

        /// <summary>
        /// Add gang weapon. Adds a weapon to the gang's stash.
        /// </summary>
        /// <param name="gangWeapon">Gang weapon</param>
        /// <returns>Added gang weapon</returns>
        public GangWeapon AddGangWeapon(GangWeapon gangWeapon)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("GangWeapons_AddGangWeapon", connection))
                {
                    connection.Open();

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@GangId", SqlDbType.NVarChar, 100).Value = gangWeapon.GangId;
                    command.Parameters.Add("@WeaponId", SqlDbType.Int).Value = gangWeapon.Weapon.WeaponId;
                    var gangWeaponId = command.Parameters.Add("@GangWeaponId", SqlDbType.NVarChar, 100);
                    gangWeaponId.Direction = ParameterDirection.Output;

                    var reader = command.ExecuteNonQuery();

                    gangWeapon.GangWeaponId = (string)gangWeaponId.Value;
                    return gangWeapon;
                }
            }
        }

        /// <summary>
        /// Remove gang weapon
        /// </summary>
        /// <param name="gangWeaponId">Gang weapon ID</param>
        public void RemoveGangWeapon(string gangWeaponId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("GangWeapons_RemoveGangWeapon", connection))
                {
                    connection.Open();

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@GangWeaponId", SqlDbType.NVarChar, 100).Value = gangWeaponId;
                    command.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Get gang stash (all gang's GangWeapons).
        /// </summary>
        /// <param name="gangId">Gang ID</param>
        /// <returns>List of weapons</returns>
        public IEnumerable<Weapon> GetGangStash(string gangId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("Weapons_GetGangStash", connection))
                {
                    connection.Open();

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@GangId", SqlDbType.NVarChar, 100).Value = gangId;

                    var reader = command.ExecuteReader();
                    return GetWeaponListFromReader(reader);
                }
            }
        }

        /// <summary>
        /// Get ganger weapons by gang ID
        /// </summary>
        /// <param name="gangId">Gang ID</param>
        /// <returns>Weapons</returns>
        public IEnumerable<Weapon> GetGangerWeaponsByGangId(string gangId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("Weapons_GetGangerWeaponsByGangId", connection))
                {
                    connection.Open();

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@GangId", SqlDbType.NVarChar, 100).Value = gangId;

                    var reader = command.ExecuteReader();
                    return GetWeaponListFromReader(reader);
                }
            }
        }

        /// <summary>
        /// Add ganger weapon
        /// </summary>
        /// <param name="gangerWeapon">Ganger weapon</param>
        /// <returns>Added ganger weapon</returns>
        public GangerWeapon AddGangerWeapon(GangerWeapon gangerWeapon)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("GangerWeapons_AddGangerWeapon", connection))
                {
                    connection.Open();

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@GangerId", SqlDbType.NVarChar, 100).Value = gangerWeapon.GangerId;
                    command.Parameters.Add("@WeaponId", SqlDbType.Int).Value = gangerWeapon.Weapon.WeaponId;
                    var gangerWeaponId = command.Parameters.Add("@GangerWeaponId", SqlDbType.NVarChar, 100);
                    gangerWeaponId.Direction = ParameterDirection.Output;

                    var reader = command.ExecuteNonQuery();

                    gangerWeapon.GangerWeaponId = (string)gangerWeaponId.Value;
                    return gangerWeapon;
                }
            }
        }

        /// <summary>
        /// Remove ganger weapon
        /// </summary>
        /// <param name="gangerWeaponId">Ganger weapon ID</param>
        public void RemoveGangerWeapon(string gangerWeaponId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("GangerWeapons_RemoveGangerWeapon", connection))
                {
                    connection.Open();

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@GangerWeaponId", SqlDbType.NVarChar, 100).Value = gangerWeaponId;
                    command.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Get ganger weapons
        /// </summary>
        /// <param name="gangerId">Ganger ID</param>
        /// <returns>List of ganger weapons</returns>
        public IEnumerable<GangerWeapon> GetGangerWeapons(string gangerId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("Weapons_GetByGangerId", connection))
                {
                    connection.Open();

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@GangerId", SqlDbType.NVarChar, 100).Value = gangerId;

                    var reader = command.ExecuteReader();
                    return GetGangerWeaponListFromReader(reader, GetGangerWeaponFromReader);
                }
            }
        }

        private IEnumerable<Weapon> GetWeaponListFromReader(SqlDataReader reader)
        {
            var weapons = new List<Weapon>();
            Weapon weapon;
            while ((weapon = GetWeaponFromReader(reader)) != null)
            {
                weapons.Add(weapon);
            }

            return weapons;
        }

        private Weapon GetWeaponFromReader(SqlDataReader reader)
        {
            var weapon = new Weapon();
            if (reader.Read())
            {
                var value = reader.GetOrdinal("weaponId");
                weapon.WeaponId = (WeaponEnum)reader.GetInt32(value);

                value = reader.GetOrdinal("weaponName");
                weapon.Name = reader.GetString(value);

                value = reader.GetOrdinal("shortRange");
                if (!reader.IsDBNull(value))
                {
                    weapon.ShortRange = reader.GetString(value);
                }

                value = reader.GetOrdinal("longRange");
                if (!reader.IsDBNull(value))
                {
                    weapon.LongRange = reader.GetString(value);
                }

                value = reader.GetOrdinal("hitShort");
                if (!reader.IsDBNull(value))
                {
                    weapon.HitShort = reader.GetString(value);
                }

                value = reader.GetOrdinal("hitLong");
                if (!reader.IsDBNull(value))
                {
                    weapon.HitLong = reader.GetString(value);
                }

                value = reader.GetOrdinal("strength");
                if (!reader.IsDBNull(value))
                {
                    weapon.Strength = reader.GetString(value);
                }

                value = reader.GetOrdinal("damage");
                if (!reader.IsDBNull(value))
                {
                    weapon.Damage = reader.GetString(value);
                }

                value = reader.GetOrdinal("saveMod");
                if (!reader.IsDBNull(value))
                {
                    weapon.SaveMod = reader.GetString(value);
                }

                value = reader.GetOrdinal("ammoRoll");
                if (!reader.IsDBNull(value))
                {
                    weapon.AmmoRoll = reader.GetString(value);
                }

                value = reader.GetOrdinal("type");
                weapon.WeaponType = (WeaponType)reader.GetInt32(value);

                value = reader.GetOrdinal("cost");
                weapon.Cost = reader.GetString(value);

                value = reader.GetOrdinal("availability");
                if (!reader.IsDBNull(value))
                {
                    weapon.WeaponAvailability = (WeaponAvailability)reader.GetInt32(value);
                }

                value = reader.GetOrdinal("description");
                if (!reader.IsDBNull(value))
                {
                    weapon.SpecialRules = reader.GetString(value);
                }
            }
            else
            {
                return null;
            }

            return weapon;
        }

        private IEnumerable<GangWeapon> GetGangWeaponListFromReader(SqlDataReader reader)
        {
            var gangWeapons = new List<GangWeapon>();
            GangWeapon weapon;
            while ((weapon = GetGangWeaponFromReader(reader)) != null)
            {
                gangWeapons.Add(weapon);
            }

            return gangWeapons;
        }

        private GangWeapon GetGangWeaponFromReader(SqlDataReader reader)
        {
            var gangWeapon = new GangWeapon();
            if (reader.Read())
            {
                var value = reader.GetOrdinal("weaponId");
                gangWeapon.Weapon.WeaponId = (WeaponEnum)reader.GetInt32(value);

                value = reader.GetOrdinal("weaponName");
                gangWeapon.Weapon.Name = reader.GetString(value);

                value = reader.GetOrdinal("shortRange");
                if (!reader.IsDBNull(value))
                {
                    gangWeapon.Weapon.ShortRange = reader.GetString(value);
                }

                value = reader.GetOrdinal("longRange");
                if (!reader.IsDBNull(value))
                {
                    gangWeapon.Weapon.LongRange = reader.GetString(value);
                }

                value = reader.GetOrdinal("hitShort");
                if (!reader.IsDBNull(value))
                {
                    gangWeapon.Weapon.HitShort = reader.GetString(value);
                }

                value = reader.GetOrdinal("hitLong");
                if (!reader.IsDBNull(value))
                {
                    gangWeapon.Weapon.HitLong = reader.GetString(value);
                }

                value = reader.GetOrdinal("strength");
                if (!reader.IsDBNull(value))
                {
                    gangWeapon.Weapon.Strength = reader.GetString(value);
                }

                value = reader.GetOrdinal("damage");
                if (!reader.IsDBNull(value))
                {
                    gangWeapon.Weapon.Damage = reader.GetString(value);
                }

                value = reader.GetOrdinal("saveMod");
                if (!reader.IsDBNull(value))
                {
                    gangWeapon.Weapon.SaveMod = reader.GetString(value);
                }

                value = reader.GetOrdinal("ammoRoll");
                if (!reader.IsDBNull(value))
                {
                    gangWeapon.Weapon.AmmoRoll = reader.GetString(value);
                }

                value = reader.GetOrdinal("type");
                gangWeapon.Weapon.WeaponType = (WeaponType)reader.GetInt32(value);

                value = reader.GetOrdinal("cost");
                gangWeapon.Weapon.Cost = reader.GetString(value);

                value = reader.GetOrdinal("availability");
                if (!reader.IsDBNull(value))
                {
                    gangWeapon.Weapon.WeaponAvailability = (WeaponAvailability)reader.GetInt32(value);
                }

                value = reader.GetOrdinal("description");
                if (!reader.IsDBNull(value))
                {
                    gangWeapon.Weapon.SpecialRules = reader.GetString(value);
                }

                value = reader.GetOrdinal("gangWeaponId");
                gangWeapon.GangWeaponId = reader.GetString(value);

                value = reader.GetOrdinal("gangId");
                gangWeapon.GangId = reader.GetString(value);
            }
            else
            {
                return null;
            }

            return gangWeapon;
        }

        private IEnumerable<GangerWeapon> GetGangerWeaponListFromReader(SqlDataReader reader, Func<SqlDataReader, GangerWeapon> parsingFunction)
        {
            var gangerWeapons = new List<GangerWeapon>();
            GangerWeapon weapon;
            while ((weapon = parsingFunction(reader)) != null)
            {
                gangerWeapons.Add(weapon);
            }

            return gangerWeapons;
        }

        private GangerWeapon GetGangerWeaponFromReader(SqlDataReader reader)
        {
            var gangerWeapon = new GangerWeapon();
            if (reader.Read())
            {
                var value = reader.GetOrdinal("weaponId");
                gangerWeapon.Weapon.WeaponId = (WeaponEnum)reader.GetInt32(value);

                value = reader.GetOrdinal("weaponName");
                gangerWeapon.Weapon.Name = reader.GetString(value);

                value = reader.GetOrdinal("shortRange");
                if (!reader.IsDBNull(value))
                {
                    gangerWeapon.Weapon.ShortRange = reader.GetString(value);
                }

                value = reader.GetOrdinal("longRange");
                if (!reader.IsDBNull(value))
                {
                    gangerWeapon.Weapon.LongRange = reader.GetString(value);
                }

                value = reader.GetOrdinal("hitShort");
                if (!reader.IsDBNull(value))
                {
                    gangerWeapon.Weapon.HitShort = reader.GetString(value);
                }

                value = reader.GetOrdinal("hitLong");
                if (!reader.IsDBNull(value))
                {
                    gangerWeapon.Weapon.HitLong = reader.GetString(value);
                }

                value = reader.GetOrdinal("strength");
                if (!reader.IsDBNull(value))
                {
                    gangerWeapon.Weapon.Strength = reader.GetString(value);
                }

                value = reader.GetOrdinal("damage");
                if (!reader.IsDBNull(value))
                {
                    gangerWeapon.Weapon.Damage = reader.GetString(value);
                }

                value = reader.GetOrdinal("saveMod");
                if (!reader.IsDBNull(value))
                {
                    gangerWeapon.Weapon.SaveMod = reader.GetString(value);
                }

                value = reader.GetOrdinal("ammoRoll");
                if (!reader.IsDBNull(value))
                {
                    gangerWeapon.Weapon.AmmoRoll = reader.GetString(value);
                }

                value = reader.GetOrdinal("type");
                gangerWeapon.Weapon.WeaponType = (WeaponType)reader.GetInt32(value);

                value = reader.GetOrdinal("cost");
                gangerWeapon.Weapon.Cost = reader.GetString(value);

                value = reader.GetOrdinal("availability");
                if (!reader.IsDBNull(value))
                {
                    gangerWeapon.Weapon.WeaponAvailability = (WeaponAvailability)reader.GetInt32(value);
                }

                value = reader.GetOrdinal("description");
                if (!reader.IsDBNull(value))
                {
                    gangerWeapon.Weapon.SpecialRules = reader.GetString(value);
                }

                value = reader.GetOrdinal("gangerWeaponId");
                gangerWeapon.GangerWeaponId = reader.GetString(value);

                value = reader.GetOrdinal("gangerId");
                gangerWeapon.GangerId = reader.GetString(value);
            }
            else
            {
                return null;
            }

            return gangerWeapon;
        }

        private GangerWeapon GetGangerWeaponLimitedInformationFromReader(SqlDataReader reader)
        {
            var gangerWeapon = new GangerWeapon();
            if (reader.Read())
            {
                var value = reader.GetOrdinal("gangerId");
                gangerWeapon.GangerId = reader.GetString(value);

                value = reader.GetOrdinal("weaponName");
                gangerWeapon.Weapon.Name = reader.GetString(value);

                value = reader.GetOrdinal("cost");
                gangerWeapon.Cost = (int)reader.GetInt32(value);
            }
            else
            {
                return null;
            }

            return gangerWeapon;
        }
    }
}
