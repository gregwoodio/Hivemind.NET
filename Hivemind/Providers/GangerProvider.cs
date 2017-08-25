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
    public class GangerProvider : HivemindProvider
    {
        public Ganger GetByGangerId(int gangerId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("Gangers_GetById", connection))
                {
                    connection.Open();

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@GangerId", SqlDbType.Int).Value = gangerId;
                    var reader = command.ExecuteReader();

                    return GetGangerFromReader(reader);
                }
            }
        }

        public IEnumerable<Ganger> GetByGangId(int gangId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("Gangers_GetByGangId", connection))
                {
                    connection.Open();

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@GangId", SqlDbType.Int).Value = gangId;
                    var reader = command.ExecuteReader();
                    return GetGangerListFromReader(reader);
                }
            }
        }

        public Ganger UpdateGanger(Ganger ganger)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("Gangers_UpdateGanger", connection))
                {
                    connection.Open();

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@GangerId", SqlDbType.Int).Value = ganger.GangerId;
                    command.Parameters.Add("@GangId", SqlDbType.Int).Value = ganger.GangId;
                    command.Parameters.Add("@Name", SqlDbType.VarChar).Value = ganger.Name;
                    command.Parameters.Add("@Type", SqlDbType.Int).Value = (int)ganger.Type;
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

                    command.ExecuteNonQuery();

                    return GetByGangerId(ganger.GangerId);
                }
            }
        }

        private IEnumerable<Ganger> GetGangerListFromReader(SqlDataReader reader)
        {
            var gangers = new List<Ganger>();

            while (reader.Read())
            {
                gangers.Add(GetGangerFromReader(reader));
            }

            return gangers;
        }

        private Ganger GetGangerFromReader(SqlDataReader reader)
        {
            var ganger = new Ganger();
            reader.Read();

            var value = reader.GetOrdinal("gangerId");
            ganger.GangerId = reader.GetInt32(value);

            value = reader.GetOrdinal("gangId");
            ganger.GangId = reader.GetInt32(value);

            value = reader.GetOrdinal("name");
            ganger.Name = reader.GetString(value);

            value = reader.GetOrdinal("type");
            ganger.Type = (GangerType)reader.GetInt32(value);

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

            return ganger;
        }
    }
}
