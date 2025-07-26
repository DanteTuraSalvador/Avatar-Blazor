using Avatar.Core.Entities;

namespace Avatar.Core.Interfaces;

public interface ISkillRepository : IRepository<Skill>
{
    Task<bool> IsSkillNameUniqueAsync(string name, int? excludeId = null);
    Task<IEnumerable<Skill>> SearchByNameAsync(string searchTerm);
    Task<Skill?> GetSkillWithTeamMembersAsync(int id);
    Task<IEnumerable<Skill>> GetSkillsWithTeamMemberCountAsync();
}
