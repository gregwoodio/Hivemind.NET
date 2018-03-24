// <copyright file="SkillProvider.cs" company="weirdvector">
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
    /// Skill provider
    /// </summary>
    public class SkillProvider : HivemindProvider
    {
        private IEnumerable<Skill> _skills;

        /// <summary>
        /// Get all skills
        /// </summary>
        /// <returns>List of all skills</returns>
        public IEnumerable<Skill> GetAllSkills()
        {
            if (_skills == null)
            {
                _skills = GetSkillsFromDatabase();
            }

            return _skills;
        }

        /// <summary>
        /// Get skills by type
        /// </summary>
        /// <param name="type">Type</param>
        /// <returns>Skills</returns>
        public IEnumerable<Skill> GetSkillsByType(SkillType type)
        {
            if (_skills == null)
            {
                _skills = GetSkillsFromDatabase();
            }

            return _skills.Where(skill => skill.SkillType == type);
        }

        /// <summary>
        /// Get Skill by ID
        /// </summary>
        /// <param name="id">Skill ID</param>
        /// <returns>Skill</returns>
        public Skill GetSkillById(int id)
        {
            if (_skills == null)
            {
                _skills = GetSkillsFromDatabase();
            }

            return _skills.Where(skill => skill.SkillId == id).FirstOrDefault();
        }

        private IEnumerable<Skill> GetSkillsFromDatabase()
        {
            using (var connection = new SqlConnection(ConnectionString))
            using (var command = new SqlCommand("Skills_GetAll", connection))
            {
                connection.Open();
                command.CommandType = CommandType.StoredProcedure;

                var reader = command.ExecuteReader();
                var skills = new List<Skill>();

                while (reader.Read())
                {
                    var skill = new Skill();

                    var value = reader.GetOrdinal("category");
                    skill.SkillType = (SkillType)reader.GetInt32(value);

                    value = reader.GetOrdinal("description");
                    skill.Description = reader.GetString(value);

                    value = reader.GetOrdinal("skillName");
                    skill.Name = reader.GetString(value);

                    value = reader.GetOrdinal("skillId");
                    skill.SkillId = reader.GetInt32(value);

                    skills.Add(skill);
                }

                return skills;
            }
        }
    }
}
