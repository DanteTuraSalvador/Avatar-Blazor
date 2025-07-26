using Avatar.Core.Entities;

namespace Avatar.Core.Interfaces;

public interface ITeamMemberRepository : IRepository<TeamMember>
{
    Task<bool> IsTeamMemberNameUniqueAsync(string firstName, string lastName, int? excludeId = null);
    Task<IEnumerable<TeamMember>> SearchByNameAsync(string searchTerm);
    Task<TeamMember?> GetTeamMemberWithSkillsAsync(int id);
    Task<IEnumerable<TeamMember>> GetTeamMembersWithSkillCountAsync();
}
