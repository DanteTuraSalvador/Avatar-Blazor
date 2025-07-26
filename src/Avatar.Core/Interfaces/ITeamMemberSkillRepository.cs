using Avatar.Core.Entities;

namespace Avatar.Core.Interfaces;

public interface ITeamMemberSkillRepository : IRepository<TeamMemberSkill>
{
    Task<bool> TeamMemberHasSkillAsync(int teamMemberId, int skillId);
    Task<IEnumerable<TeamMemberSkill>> GetSkillsByTeamMemberIdAsync(int teamMemberId);
    Task<IEnumerable<TeamMemberSkill>> GetTeamMembersBySkillIdAsync(int skillId);
    Task<TeamMemberSkill?> GetTeamMemberSkillAsync(int teamMemberId, int skillId);
    Task<IEnumerable<TeamMemberSkill>> GetAllWithDetailsAsync();
}
