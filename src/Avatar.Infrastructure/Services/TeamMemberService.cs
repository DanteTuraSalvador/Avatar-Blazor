using Avatar.Core.DTOs;
using Avatar.Core.Entities;
using Avatar.Core.Interfaces;

namespace Avatar.Infrastructure.Services;

public class TeamMemberService : ITeamMemberService
{
    private readonly ITeamMemberRepository _teamMemberRepository;

    public TeamMemberService(ITeamMemberRepository teamMemberRepository)
    {
        _teamMemberRepository = teamMemberRepository;
    }

    public async Task<IEnumerable<TeamMemberDto>> GetAllTeamMembersAsync()
    {
        var teamMembers = await _teamMemberRepository.GetTeamMembersWithSkillCountAsync();
        return teamMembers.Select(MapToDto);
    }

    public async Task<TeamMemberDto?> GetTeamMemberByIdAsync(int id)
    {
        var teamMember = await _teamMemberRepository.GetByIdAsync(id);
        return teamMember != null ? MapToDto(teamMember) : null;
    }

    public async Task<TeamMemberDto> CreateTeamMemberAsync(CreateTeamMemberDto createTeamMemberDto)
    {
        // Business rule: Team member name must be unique (FirstName + LastName combination)
        if (!await _teamMemberRepository.IsTeamMemberNameUniqueAsync(createTeamMemberDto.FirstName, createTeamMemberDto.LastName))
        {
            throw new InvalidOperationException($"A team member with the name '{createTeamMemberDto.FirstName} {createTeamMemberDto.LastName}' already exists.");
        }

        var teamMember = new TeamMember
        {
            FirstName = createTeamMemberDto.FirstName,
            LastName = createTeamMemberDto.LastName,
            Email = createTeamMemberDto.Email,
            Position = createTeamMemberDto.Position,
            Department = createTeamMemberDto.Department,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = createTeamMemberDto.CreatedBy
        };

        var createdTeamMember = await _teamMemberRepository.AddAsync(teamMember);
        return MapToDto(createdTeamMember);
    }

    public async Task<TeamMemberDto> UpdateTeamMemberAsync(int id, UpdateTeamMemberDto updateTeamMemberDto)
    {
        var teamMember = await _teamMemberRepository.GetByIdAsync(id);
        if (teamMember == null)
        {
            throw new InvalidOperationException($"Team member with ID {id} not found.");
        }

        // Business rule: Team member name must be unique (excluding current team member)
        if (!await _teamMemberRepository.IsTeamMemberNameUniqueAsync(updateTeamMemberDto.FirstName, updateTeamMemberDto.LastName, id))
        {
            throw new InvalidOperationException($"A team member with the name '{updateTeamMemberDto.FirstName} {updateTeamMemberDto.LastName}' already exists.");
        }

        teamMember.FirstName = updateTeamMemberDto.FirstName;
        teamMember.LastName = updateTeamMemberDto.LastName;
        teamMember.Email = updateTeamMemberDto.Email;
        teamMember.Position = updateTeamMemberDto.Position;
        teamMember.Department = updateTeamMemberDto.Department;
        teamMember.UpdatedAt = DateTime.UtcNow;
        teamMember.UpdatedBy = updateTeamMemberDto.UpdatedBy;

        var updatedTeamMember = await _teamMemberRepository.UpdateAsync(teamMember);
        return MapToDto(updatedTeamMember);
    }

    public async Task DeleteTeamMemberAsync(int id)
    {
        var teamMember = await _teamMemberRepository.GetByIdAsync(id);
        if (teamMember == null)
        {
            throw new InvalidOperationException($"Team member with ID {id} not found.");
        }

        await _teamMemberRepository.DeleteAsync(teamMember);
    }

    public async Task<IEnumerable<TeamMemberDto>> SearchTeamMembersAsync(string searchTerm)
    {
        var teamMembers = await _teamMemberRepository.SearchByNameAsync(searchTerm);
        return teamMembers.Select(MapToDto);
    }

    public async Task<bool> IsTeamMemberNameUniqueAsync(string firstName, string lastName, int? excludeId = null)
    {
        return await _teamMemberRepository.IsTeamMemberNameUniqueAsync(firstName, lastName, excludeId);
    }

    public async Task<TeamMemberDto?> GetTeamMemberWithSkillsAsync(int id)
    {
        var teamMember = await _teamMemberRepository.GetTeamMemberWithSkillsAsync(id);
        return teamMember != null ? MapToDtoWithSkills(teamMember) : null;
    }

    private static TeamMemberDto MapToDto(TeamMember teamMember)
    {
        return new TeamMemberDto
        {
            Id = teamMember.Id,
            FirstName = teamMember.FirstName,
            LastName = teamMember.LastName,
            FullName = teamMember.FullName,
            Email = teamMember.Email,
            Position = teamMember.Position,
            Department = teamMember.Department,
            CreatedAt = teamMember.CreatedAt,
            CreatedBy = teamMember.CreatedBy,
            UpdatedAt = teamMember.UpdatedAt,
            UpdatedBy = teamMember.UpdatedBy,
            SkillCount = teamMember.TeamMemberSkills?.Count ?? 0
        };
    }

    private static TeamMemberDto MapToDtoWithSkills(TeamMember teamMember)
    {
        var dto = MapToDto(teamMember);
        dto.Skills = teamMember.TeamMemberSkills?.Select(tms => new TeamMemberSkillDto
        {
            Id = tms.Id,
            TeamMemberId = tms.TeamMemberId,
            SkillId = tms.SkillId,
            TeamMemberName = teamMember.FullName,
            SkillName = tms.Skill.Name,
            Level = tms.Level,
            LevelName = tms.LevelName,
            AssignedAt = tms.AssignedAt,
            AssignedBy = tms.AssignedBy,
            UpdatedAt = tms.UpdatedAt,
            UpdatedBy = tms.UpdatedBy
        }).ToList() ?? new List<TeamMemberSkillDto>();
        
        return dto;
    }
}
