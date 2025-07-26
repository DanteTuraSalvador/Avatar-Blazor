using Avatar.Core.DTOs;

namespace Avatar.Core.Interfaces;

public interface ISkillService
{
    Task<IEnumerable<SkillDto>> GetAllSkillsAsync();
    Task<SkillDto?> GetSkillByIdAsync(int id);
    Task<SkillDto> CreateSkillAsync(CreateSkillDto createSkillDto);
    Task<SkillDto> UpdateSkillAsync(int id, UpdateSkillDto updateSkillDto);
    Task DeleteSkillAsync(int id);
    Task<IEnumerable<SkillDto>> SearchSkillsAsync(string searchTerm);
    Task<bool> IsSkillNameUniqueAsync(string name, int? excludeId = null);
}
