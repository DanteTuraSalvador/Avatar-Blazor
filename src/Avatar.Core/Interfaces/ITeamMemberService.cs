using Avatar.Core.DTOs;

namespace Avatar.Core.Interfaces;

public interface ITeamMemberService
{
    Task<IEnumerable<TeamMemberDto>> GetAllTeamMembersAsync();
    Task<TeamMemberDto?> GetTeamMemberByIdAsync(int id);
    Task<TeamMemberDto> CreateTeamMemberAsync(CreateTeamMemberDto createTeamMemberDto);
    Task<TeamMemberDto> UpdateTeamMemberAsync(int id, UpdateTeamMemberDto updateTeamMemberDto);
    Task DeleteTeamMemberAsync(int id);
    Task<IEnumerable<TeamMemberDto>> SearchTeamMembersAsync(string searchTerm);
    Task<bool> IsTeamMemberNameUniqueAsync(string firstName, string lastName, int? excludeId = null);
    Task<TeamMemberDto?> GetTeamMemberWithSkillsAsync(int id);
}
