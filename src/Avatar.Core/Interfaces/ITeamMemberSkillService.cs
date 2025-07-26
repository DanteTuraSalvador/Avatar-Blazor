using Avatar.Core.DTOs;

namespace Avatar.Core.Interfaces;

public interface ITeamMemberSkillService
{
    Task<TeamMemberSkillsBuilderDto> GetTeamMemberSkillsBuilderAsync(int teamMemberId);
    Task<TeamMemberSkillDto> AssignSkillToTeamMemberAsync(CreateTeamMemberSkillDto createDto);
    Task<TeamMemberSkillDto> UpdateTeamMemberSkillLevelAsync(int teamMemberSkillId, UpdateTeamMemberSkillDto updateDto);
    Task DeleteTeamMemberSkillAsync(int teamMemberSkillId);
    Task<IEnumerable<TeamMemberSkillDto>> GetSkillsByTeamMemberIdAsync(int teamMemberId);
    Task<IEnumerable<TeamMemberSkillDto>> GetTeamMembersBySkillIdAsync(int skillId);
    Task<bool> TeamMemberHasSkillAsync(int teamMemberId, int skillId);
}
