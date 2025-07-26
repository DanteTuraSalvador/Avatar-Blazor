using Avatar.Core.DTOs;
using Avatar.Core.Entities;
using Avatar.Core.Interfaces;

namespace Avatar.Infrastructure.Services;

public class SkillService : ISkillService
{
    private readonly ISkillRepository _skillRepository;

    public SkillService(ISkillRepository skillRepository)
    {
        _skillRepository = skillRepository;
    }

    public async Task<IEnumerable<SkillDto>> GetAllSkillsAsync()
    {
        var skills = await _skillRepository.GetSkillsWithTeamMemberCountAsync();
        return skills.Select(MapToDto);
    }

    public async Task<SkillDto?> GetSkillByIdAsync(int id)
    {
        var skill = await _skillRepository.GetSkillWithTeamMembersAsync(id);
        return skill != null ? MapToDto(skill) : null;
    }

    public async Task<SkillDto> CreateSkillAsync(CreateSkillDto createSkillDto)
    {
        // Business rule: Skill name must be unique
        if (!await _skillRepository.IsSkillNameUniqueAsync(createSkillDto.Name))
        {
            throw new InvalidOperationException($"A skill with the name '{createSkillDto.Name}' already exists.");
        }

        var skill = new Skill
        {
            Name = createSkillDto.Name,
            Description = createSkillDto.Description,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = createSkillDto.CreatedBy
        };

        var createdSkill = await _skillRepository.AddAsync(skill);
        return MapToDto(createdSkill);
    }

    public async Task<SkillDto> UpdateSkillAsync(int id, UpdateSkillDto updateSkillDto)
    {
        var skill = await _skillRepository.GetByIdAsync(id);
        if (skill == null)
        {
            throw new InvalidOperationException($"Skill with ID {id} not found.");
        }

        // Business rule: Skill name must be unique (excluding current skill)
        if (!await _skillRepository.IsSkillNameUniqueAsync(updateSkillDto.Name, id))
        {
            throw new InvalidOperationException($"A skill with the name '{updateSkillDto.Name}' already exists.");
        }

        skill.Name = updateSkillDto.Name;
        skill.Description = updateSkillDto.Description;
        skill.UpdatedAt = DateTime.UtcNow;
        skill.UpdatedBy = updateSkillDto.UpdatedBy;

        var updatedSkill = await _skillRepository.UpdateAsync(skill);
        return MapToDto(updatedSkill);
    }

    public async Task DeleteSkillAsync(int id)
    {
        var skill = await _skillRepository.GetByIdAsync(id);
        if (skill == null)
        {
            throw new InvalidOperationException($"Skill with ID {id} not found.");
        }

        await _skillRepository.DeleteAsync(skill);
    }

    public async Task<IEnumerable<SkillDto>> SearchSkillsAsync(string searchTerm)
    {
        var skills = await _skillRepository.SearchByNameAsync(searchTerm);
        return skills.Select(MapToDto);
    }

    public async Task<bool> IsSkillNameUniqueAsync(string name, int? excludeId = null)
    {
        return await _skillRepository.IsSkillNameUniqueAsync(name, excludeId);
    }

    private static SkillDto MapToDto(Skill skill)
    {
        return new SkillDto
        {
            Id = skill.Id,
            Name = skill.Name,
            Description = skill.Description,
            CreatedAt = skill.CreatedAt,
            CreatedBy = skill.CreatedBy,
            UpdatedAt = skill.UpdatedAt,
            UpdatedBy = skill.UpdatedBy,
            TeamMemberCount = skill.TeamMemberSkills?.Count ?? 0
        };
    }
}
