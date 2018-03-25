// <copyright file="GangerProvider.cs" company="weirdvector">
// Copyright (c) weirdvector. All rights reserved.
// </copyright>

using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Hivemind.Entities;
using Hivemind.Enums;

namespace Hivemind.Providers
{
    /// <summary>
    /// Ganger provider
    /// </summary>
    public class GangerProvider : HivemindProvider, IGangerProvider
    {
        /// <summary>
        /// Get by Ganger ID
        /// </summary>
        /// <param name="gangerId">Ganger ID</param>
        /// <returns>Ganger</returns>
        public Ganger GetByGangerId(string gangerId)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                using (var command = new SqlCommand("Gangers_GetById", connection))
                {
                    connection.Open();

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@GangerId", SqlDbType.NVarChar, 100).Value = gangerId;
                    var reader = command.ExecuteReader();

                    return GetGangerFromReader(reader);
                }
            }
        }

        /// <summary>
        /// Get by Gang ID
        /// </summary>
        /// <param name="gangId">Gang ID</param>
        /// <returns>List of gangers in the gang</returns>
        public IEnumerable<Ganger> GetByGangId(string gangId)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                using (var command = new SqlCommand("Gangers_GetByGangId", connection))
                {
                    connection.Open();

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@GangId", SqlDbType.NVarChar, 100).Value = gangId;
                    var reader = command.ExecuteReader();
                    return GetGangerListFromReader(reader);
                }
            }
        }

        /// <summary>
        /// Add ganger
        /// </summary>
        /// <param name="ganger">Ganger</param>
        /// <returns>Added ganger</returns>
        public Ganger AddGanger(Ganger ganger)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                using (var command = new SqlCommand("Gangers_AddGanger", connection))
                {
                    connection.Open();

                    command.CommandType = CommandType.StoredProcedure;
                    var gangerId = command.Parameters.Add("@GangerId", SqlDbType.NVarChar, 100);
                    gangerId.Direction = ParameterDirection.Output;
                    command.Parameters.Add("@GangId", SqlDbType.NVarChar, 100).Value = ganger.GangId;
                    command.Parameters.Add("@Name", SqlDbType.VarChar).Value = ganger.Name;
                    command.Parameters.Add("@Type", SqlDbType.Int).Value = (int)ganger.GangerType;
                    command.Parameters.Add("@Move", SqlDbType.Int).Value = ganger.Move;
                    command.Parameters.Add("@WeaponSkill", SqlDbType.Int).Value = ganger.WeaponSkill;
                    command.Parameters.Add("@BallisticSkill", SqlDbType.Int).Value = ganger.BallisticSkill;
                    command.Parameters.Add("@Strength", SqlDbType.Int).Value = ganger.Strength;
                    command.Parameters.Add("@Toughness", SqlDbType.Int).Value = ganger.Toughness;
                    command.Parameters.Add("@Wounds", SqlDbType.Int).Value = ganger.Wounds;
                    command.Parameters.Add("@Initiative", SqlDbType.Int).Value = ganger.Initiative;
                    command.Parameters.Add("@Attack", SqlDbType.Int).Value = ganger.Attack;
                    command.Parameters.Add("@Leadership", SqlDbType.Int).Value = ganger.Leadership;
                    command.Parameters.Add("@Experience", SqlDbType.Int).Value = ganger.Experience;
                    command.Parameters.Add("@Active", SqlDbType.Int).Value = ganger.Active;
                    command.Parameters.Add("@IsOneEyed", SqlDbType.Int).Value = ganger.IsOneEyed;
                    command.Parameters.Add("@IsDeafened", SqlDbType.Int).Value = ganger.IsDeafened;
                    command.Parameters.Add("@IsOneHanded", SqlDbType.Int).Value = ganger.IsOneHanded;
                    command.Parameters.Add("@RightHandFingers", SqlDbType.Int).Value = ganger.RightHandFingers;
                    command.Parameters.Add("@LeftHandFingers", SqlDbType.Int).Value = ganger.LeftHandFingers;
                    command.Parameters.Add("@HasHorribleScars", SqlDbType.Int).Value = ganger.HasHorribleScars;
                    command.Parameters.Add("@HasImpressiveScars", SqlDbType.Int).Value = ganger.HasHorribleScars;
                    command.Parameters.Add("@HasOldBattleWound", SqlDbType.Int).Value = ganger.HasOldBattleWound;
                    command.Parameters.Add("@HasHeadWound", SqlDbType.Int).Value = ganger.HasHorribleScars;
                    command.Parameters.Add("@IsCaptured", SqlDbType.Int).Value = ganger.HasHorribleScars;
                    command.Parameters.Add("@HasBitterEnmity", SqlDbType.Int).Value = ganger.HasHorribleScars;
                    command.Parameters.Add("@Cost", SqlDbType.Int).Value = ganger.Cost;

                    command.ExecuteNonQuery();
                    ganger.GangerId = (string)gangerId.Value;

                    return ganger;
                }
            }
        }

        /// <summary>
        /// Get ganger skills
        /// </summary>
        /// <param name="gangId">GangId</param>
        /// <returns>List of GangerSkills</returns>
        public IEnumerable<GangerSkill> GetGangerSkills(string gangId)
        {
            using (var connection = new SqlConnection(ConnectionString))
            using (var command = new SqlCommand("GangerSkills_GetByGangId", connection))
            {
                connection.Open();

                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@GangId", SqlDbType.NVarChar, 100).Value = gangId;

                var reader = command.ExecuteReader();
                var gangerSkills = new List<GangerSkill>();

                while (reader.Read())
                {
                    var gangerSkill = new GangerSkill();

                    var value = reader.GetOrdinal("gangerId");
                    gangerSkill.GangerId = reader.GetString(value);

                    value = reader.GetOrdinal("skillId");
                    gangerSkill.SkillId = reader.GetInt32(value);

                    gangerSkills.Add(gangerSkill);
                }

                return gangerSkills;
            }
        }

        /// <summary>
        /// Update ganger
        /// </summary>
        /// <param name="ganger">Ganger</param>
        /// <returns>Updated ganger</returns>
        public Ganger UpdateGanger(Ganger ganger)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                using (var command = new SqlCommand("Gangers_UpdateGanger", connection))
                {
                    connection.Open();

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@GangerId", SqlDbType.NVarChar, 100).Value = ganger.GangerId;
                    command.Parameters.Add("@GangId", SqlDbType.NVarChar, 100).Value = ganger.GangId;
                    command.Parameters.Add("@Name", SqlDbType.VarChar).Value = ganger.Name;
                    command.Parameters.Add("@Type", SqlDbType.Int).Value = (int)ganger.GangerType;
                    command.Parameters.Add("@Move", SqlDbType.Int).Value = ganger.Move;
                    command.Parameters.Add("@WeaponSkill", SqlDbType.Int).Value = ganger.WeaponSkill;
                    command.Parameters.Add("@BallisticSkill", SqlDbType.Int).Value = ganger.BallisticSkill;
                    command.Parameters.Add("@Strength", SqlDbType.Int).Value = ganger.Strength;
                    command.Parameters.Add("@Toughness", SqlDbType.Int).Value = ganger.Toughness;
                    command.Parameters.Add("@Wounds", SqlDbType.Int).Value = ganger.Wounds;
                    command.Parameters.Add("@Initiative", SqlDbType.Int).Value = ganger.Initiative;
                    command.Parameters.Add("@Attack", SqlDbType.Int).Value = ganger.Attack;
                    command.Parameters.Add("@Leadership", SqlDbType.Int).Value = ganger.Leadership;
                    command.Parameters.Add("@Experience", SqlDbType.Int).Value = ganger.Experience;
                    command.Parameters.Add("@Active", SqlDbType.Int).Value = ganger.Active;
                    command.Parameters.Add("@IsOneEyed", SqlDbType.Int).Value = ganger.IsOneEyed;
                    command.Parameters.Add("@IsDeafened", SqlDbType.Int).Value = ganger.IsDeafened;
                    command.Parameters.Add("@IsOneHanded", SqlDbType.Int).Value = ganger.IsOneHanded;
                    command.Parameters.Add("@RightHandFingers", SqlDbType.Int).Value = ganger.RightHandFingers;
                    command.Parameters.Add("@LeftHandFingers", SqlDbType.Int).Value = ganger.LeftHandFingers;
                    command.Parameters.Add("@HasHorribleScars", SqlDbType.Int).Value = ganger.HasHorribleScars;
                    command.Parameters.Add("@HasImpressiveScars", SqlDbType.Int).Value = ganger.HasHorribleScars;
                    command.Parameters.Add("@HasHeadWound", SqlDbType.Int).Value = ganger.HasHorribleScars;
                    command.Parameters.Add("@IsCaptured", SqlDbType.Int).Value = ganger.HasHorribleScars;
                    command.Parameters.Add("@HasBitterEnmity", SqlDbType.Int).Value = ganger.HasHorribleScars;
                    command.Parameters.Add("@HasOldBattleWound", SqlDbType.TinyInt).Value = ganger.HasOldBattleWound;

                    command.ExecuteNonQuery();

                    return GetByGangerId(ganger.GangerId);
                }
            }
        }

        /// <summary>
        /// Add ganger skill
        /// </summary>
        /// <param name="gangerId">Ganger ID</param>
        /// <param name="skillId">Skill ID</param>
        public void AddGangerSkill(string gangerId, int skillId)
        {
            using (var connection = new SqlConnection(ConnectionString))
            using (var command = new SqlCommand("GangerSkills_Add", connection))
            {
                connection.Open();

                command.CommandType = CommandType.StoredProcedure;
                var output = command.Parameters.Add("@GangerSkillId", SqlDbType.NVarChar, 100);
                output.Direction = ParameterDirection.Output;
                command.Parameters.Add("@GangerId", SqlDbType.NVarChar, 100).Value = gangerId;
                command.Parameters.Add("@SkillId", SqlDbType.Int).Value = skillId;

                command.ExecuteNonQuery();
            }
        }

        #region Ganger Advancements

        /// <summary>
        /// Can the ganger learn a skill? Check if advancement ID is valid for ganger.
        /// </summary>
        /// <param name="gangerId">Ganger ID</param>
        /// <param name="advancementId">Advancement ID</param>
        /// <returns>Whether ganger can learn a skill or not</returns>
        public bool CanLearnSkill(string gangerId, string advancementId)
        {
            using (var connection = new SqlConnection(ConnectionString))
            using (var command = new SqlCommand("GangerAdvancements_IsValid", connection))
            {
                connection.Open();

                command.CommandType = CommandType.StoredProcedure;
                var output = command.Parameters.Add("@Output", SqlDbType.Int);
                output.Direction = ParameterDirection.Output;
                command.Parameters.Add("@GangerId", SqlDbType.NVarChar, 100).Value = gangerId;
                command.Parameters.Add("@AdvancementId", SqlDbType.NVarChar, 100).Value = advancementId;

                command.ExecuteNonQuery();

                return (int)output.Value > 0;
            }
        }

        /// <summary>
        /// Register ganger advancement. Make a ganger eligible for an advance roll.
        /// </summary>
        /// <param name="gangerId">Ganger ID</param>
        /// <returns>The advancement ID</returns>
        public string RegisterGangerAdvancement(string gangerId)
        {
            using (var connection = new SqlConnection(ConnectionString))
            using (var command = new SqlCommand("GangerAdvancements_Add", connection))
            {
                connection.Open();

                command.CommandType = CommandType.StoredProcedure;
                var advancementId = command.Parameters.Add("@AdvancementId", SqlDbType.NVarChar, 100);
                advancementId.Direction = ParameterDirection.Output;
                command.Parameters.Add("@GangerId", SqlDbType.NVarChar, 100).Value = gangerId;

                command.ExecuteNonQuery();

                return (string)advancementId.Value;
            }
        }

        /// <summary>
        /// Remove ganger advancment. Ganger has advanced.
        /// </summary>
        /// <param name="gangerId">Ganger ID</param>
        /// <param name="advancementId">Advancement ID</param>
        public void RemoveGangerAdvancement(string gangerId, string advancementId)
        {
            using (var connection = new SqlConnection(ConnectionString))
            using (var command = new SqlCommand("GangerAdvancements_Remove", connection))
            {
                connection.Open();

                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@AdvancementId", SqlDbType.NVarChar, 100).Value = advancementId;
                command.Parameters.Add("@GangerId", SqlDbType.NVarChar, 100).Value = gangerId;

                command.ExecuteNonQuery();
            }
        }
        #endregion

        /// <summary>
        /// Add injury to ganger
        /// </summary>
        /// <param name="gangerId">Ganger ID</param>
        /// <param name="injury">Injury ID</param>
        public void AddGangerInjury(string gangerId, InjuryEnum injury)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                using (var command = new SqlCommand("GangerInjuries_Add", connection))
                {
                    connection.Open();

                    command.CommandType = CommandType.StoredProcedure;
                    var gangerInjuryId = command.Parameters.Add("@GangerInjuryId", SqlDbType.NVarChar, 100);
                    gangerInjuryId.Direction = ParameterDirection.Output;
                    command.Parameters.Add("@GangerId", SqlDbType.NVarChar, 100).Value = gangerId;
                    command.Parameters.Add("@InjuryId", SqlDbType.Int).Value = (int)injury;

                    command.ExecuteNonQuery();
                }
            }
        }

        private IEnumerable<Ganger> GetGangerListFromReader(SqlDataReader reader)
        {
            var gangers = new List<Ganger>();
            Ganger ganger;

            while ((ganger = GetGangerFromReader(reader)) != null)
            {
                gangers.Add(ganger);
            }

            return gangers;
        }

        private Ganger GetGangerFromReader(SqlDataReader reader)
        {
            var ganger = new Ganger();
            if (reader.Read())
            {
                var value = reader.GetOrdinal("gangerId");
                ganger.GangerId = reader.GetString(value);

                value = reader.GetOrdinal("gangId");
                ganger.GangId = reader.GetString(value);

                value = reader.GetOrdinal("name");
                ganger.Name = reader.GetString(value);

                value = reader.GetOrdinal("type");
                ganger.GangerType = (GangerType)reader.GetInt32(value);

                value = reader.GetOrdinal("move");
                ganger.Move = reader.GetInt32(value);

                value = reader.GetOrdinal("weaponSkill");
                ganger.WeaponSkill = reader.GetInt32(value);

                value = reader.GetOrdinal("ballisticSkill");
                ganger.BallisticSkill = reader.GetInt32(value);

                value = reader.GetOrdinal("strength");
                ganger.Strength = reader.GetInt32(value);

                value = reader.GetOrdinal("toughness");
                ganger.Toughness = reader.GetInt32(value);

                value = reader.GetOrdinal("wounds");
                ganger.Wounds = reader.GetInt32(value);

                value = reader.GetOrdinal("initiative");
                ganger.Initiative = reader.GetInt32(value);

                value = reader.GetOrdinal("attack");
                ganger.Attack = reader.GetInt32(value);

                value = reader.GetOrdinal("leadership");
                ganger.Leadership = reader.GetInt32(value);

                value = reader.GetOrdinal("experience");
                ganger.Experience = reader.GetInt32(value);

                value = reader.GetOrdinal("active");
                ganger.Active = reader.GetByte(value) == 1 ? true : false;

                value = reader.GetOrdinal("isOneEyed");
                ganger.IsOneEyed = reader.GetByte(value) == 1 ? true : false;

                value = reader.GetOrdinal("isDeafened");
                ganger.IsDeafened = reader.GetByte(value) == 1 ? true : false;

                value = reader.GetOrdinal("isOneHanded");
                ganger.IsOneHanded = reader.GetByte(value) == 1 ? true : false;

                value = reader.GetOrdinal("rightHandFingers");
                ganger.RightHandFingers = reader.GetInt32(value);

                value = reader.GetOrdinal("leftHandFingers");
                ganger.LeftHandFingers = reader.GetInt32(value);

                value = reader.GetOrdinal("hasHorribleScars");
                ganger.HasHorribleScars = reader.GetByte(value) == 1 ? true : false;

                value = reader.GetOrdinal("hasImpressiveScars");
                ganger.HasImpressiveScars = reader.GetByte(value) == 1 ? true : false;

                value = reader.GetOrdinal("hasHeadWound");
                ganger.HasHeadWound = reader.GetByte(value) == 1 ? true : false;

                value = reader.GetOrdinal("hasOldBattleWound");
                ganger.HasOldBattleWound = reader.GetByte(value) == 1 ? true : false;

                value = reader.GetOrdinal("isCaptured");
                ganger.IsCaptured = reader.GetByte(value) == 1 ? true : false;

                value = reader.GetOrdinal("hasBitterEnmity");
                ganger.HasBitterEnmity = reader.GetByte(value) == 1 ? true : false;

                value = reader.GetOrdinal("hasSporeSickness");
                ganger.HasSporeSickness = reader.GetByte(value) == 1 ? true : false;
            }
            else
            {
                return null;
            }

            return ganger;
        }
    }
}
