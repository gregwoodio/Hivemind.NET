using Hivemind.Entities;
using Hivemind.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hivemind.Providers
{
    /// <summary>
    /// Skill provider interface
    /// </summary>
    public interface ISkillProvider
    {
        /// <summary>
        /// Get all skills
        /// </summary>
        /// <returns>List of all skills</returns>
        IEnumerable<Skill> GetAllSkills();

        /// <summary>
        /// Get skills by type
        /// </summary>
        /// <param name="type">Type</param>
        /// <returns>Skills</returns>
        IEnumerable<Skill> GetSkillsByType(SkillType type);

        /// <summary>
        /// Get Skill by ID
        /// </summary>
        /// <param name="id">Skill ID</param>
        /// <returns>Skill</returns>
        Skill GetSkillById(int id);
    }
}
