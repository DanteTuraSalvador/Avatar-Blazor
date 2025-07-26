using Avatar.Core.DTOs;
using Avatar.Core.Entities;
using Avatar.Core.Interfaces;

namespace Avatar.Infrastructure.Services;

public class TeamMemberSkillService : ITeamMemberSkillService
{
    private readonly ITeamMemberSkillRepository _teamMemberSkillRepository;
    private readonly ITeamMemberRepository _teamMemberRepository;
    private readonly ISkillRepository _skillRepository;

    public TeamMemberSkillService(
        ITeamMemberSkillRepository teamMemberSkillRepository,
        ITeamMemberRepository teamMemberRepository,
        ISkillRepository skillRepository)
    {
        _teamMemberSkillRepository = teamMemberSkillRepository;
        _teamMemberRepository = teamMemberRepository;
        _skillRepository = skillRepository;
    }

    public async Task<TeamMemberSkillsBuilderDto> GetTeamMemberSkillsBuilderAsync(int teamMemberId)
    {
        var teamMember = await _teamMemberRepository.GetTeamMemberWithSkillsAsync(teamMemberId);
        if (teamMember == null)
        {
            throw new InvalidOperationException($"Team member with ID {teamMemberId} not found.");
        }

        var allSkills = await _skillRepository.GetAllAsync();
        var assignedSkills = await _teamMemberSkillRepository.GetSkillsByTeamMemberIdAsync(teamMemberId);

        return new TeamMemberSkillsBuilderDto
        {
            TeamMember = new TeamMemberDto
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
            },
            AvailableSkills = allSkills.Select(s => new SkillDto
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description,
                CreatedAt = s.CreatedAt,
                CreatedBy = s.CreatedBy,
                UpdatedAt = s.UpdatedAt,
                UpdatedBy = s.UpdatedBy,
                TeamMemberCount = s.TeamMemberSkills?.Count ?? 0
            }).ToList(),
            AssignedSkills = assignedSkills.Select(MapToDto).ToList()
        };
    }

    public async Task<TeamMemberSkillDto> AssignSkillToTeamMemberAsync(CreateTeamMemberSkillDto createDto)
    {
        // Business rule: A team member cannot have duplicate skills
        if (await _teamMemberSkillRepository.TeamMemberHasSkillAsync(createDto.TeamMemberId, createDto.SkillId))
        {
            throw new InvalidOperationException("This team member already has this skill assigned.");
        }

        // Validate that team member exists
        var teamMember = await _teamMemberRepository.GetByIdAsync(createDto.TeamMemberId);
        if (teamMember == null)
        {
            throw new InvalidOperationException($"Team member with ID {createDto.TeamMemberId} not found.");
        }

        // Validate that skill exists
        var skill = await _skillRepository.GetByIdAsync(createDto.SkillId);
        if (skill == null)
        {
            throw new InvalidOperationException($"Skill with ID {createDto.SkillId} not found.");
        }

        var teamMemberSkill = new TeamMemberSkill
        {
            TeamMemberId = createDto.TeamMemberId,
            SkillId = createDto.SkillId,
            Level = createDto.Level,
            AssignedAt = DateTime.UtcNow,
            AssignedBy = createDto.AssignedBy
        };

        var createdTeamMemberSkill = await _teamMemberSkillRepository.AddAsync(teamMemberSkill);
        
        // Reload with navigation properties
        var result = await _teamMemberSkillRepository.GetTeamMemberSkillAsync(createDto.TeamMemberId, createDto.SkillId);
        return MapToDto(result!);
    }

    public async Task<TeamMemberSkillDto> UpdateTeamMemberSkillLevelAsync(int teamMemberSkillId, UpdateTeamMemberSkillDto updateDto)
    {
        var teamMemberSkill = await _teamMemberSkillRepository.GetByIdAsync(teamMemberSkillId);
        if (teamMemberSkill == null)
        {
            throw new InvalidOperationException($"Team member skill with ID {teamMemberSkillId} not found.");
        }

        teamMemberSkill.Level = updateDto.Level;
        teamMemberSkill.UpdatedAt = DateTime.UtcNow;
        teamMemberSkill.UpdatedBy = updateDto.UpdatedBy;

        await _teamMemberSkillRepository.UpdateAsync(teamMemberSkill);
        
        // Reload with navigation properties
        var result = await _teamMemberSkillRepository.GetTeamMemberSkillAsync(teamMemberSkill.TeamMemberId, teamMemberSkill.SkillId);
        return MapToDto(result!);
    }

    public async Task DeleteTeamMemberSkillAsync(int teamMemberSkillId)
    {
        var teamMemberSkill = await _teamMemberSkillRepository.GetByIdAsync(teamMemberSkillId);
        if (teamMemberSkill == null)
        {
            throw new InvalidOperationException($"Team member skill with ID {teamMemberSkillId} not found.");
        }

        await _teamMemberSkillRepository.DeleteAsync(teamMemberSkill);
    }

    public async Task<IEnumerable<TeamMemberSkillDto>> GetSkillsByTeamMemberIdAsync(int teamMemberId)
    {
        var teamMemberSkills = await _teamMemberSkillRepository.GetSkillsByTeamMemberIdAsync(teamMemberId);
        return teamMemberSkills.Select(MapToDto);
    }

    public async Task<IEnumerable<TeamMemberSkillDto>> GetTeamMembersBySkillIdAsync(int skillId)
    {
        var teamMemberSkills = await _teamMemberSkillRepository.GetTeamMembersBySkillIdAsync(skillId);
        return teamMemberSkills.Select(MapToDto);
    }

    public async Task<bool> TeamMemberHasSkillAsync(int teamMemberId, int skillId)
    {
        return await _teamMemberSkillRepository.TeamMemberHasSkillAsync(teamMemberId, skillId);
    }

    private static TeamMemberSkillDto MapToDto(TeamMemberSkill teamMemberSkill)
    {
        return new TeamMemberSkillDto
        {
            Id = teamMemberSkill.Id,
            TeamMemberId = teamMemberSkill.TeamMemberId,
            SkillId = teamMemberSkill.SkillId,
            TeamMemberName = teamMemberSkill.TeamMember?.FullName ?? "",
            SkillName = teamMemberSkill.Skill?.Name ?? "",
            Level = teamMemberSkill.Level,
            LevelName = teamMemberSkill.LevelName,
            AssignedAt = teamMemberSkill.AssignedAt,
            AssignedBy = teamMemberSkill.AssignedBy,
            UpdatedAt = teamMemberSkill.UpdatedAt,
            UpdatedBy = teamMemberSkill.UpdatedBy
        };
    }
}
