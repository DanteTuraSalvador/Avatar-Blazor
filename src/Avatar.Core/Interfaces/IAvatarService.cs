using Avatar.Core.DTOs;

namespace Avatar.Core.Interfaces;

public interface IAvatarService
{
    Task<AvatarDto?> GetByIdAsync(int id);
    Task<IEnumerable<AvatarDto>> GetAllAsync();
    Task<IEnumerable<AvatarDto>> GetByCategoryAsync(string category);
    Task<IEnumerable<AvatarDto>> GetActiveAvatarsAsync();
    Task<IEnumerable<AvatarDto>> SearchByNameAsync(string name);
    Task<AvatarDto> CreateAsync(CreateAvatarDto createAvatarDto);
    Task<AvatarDto> UpdateAsync(int id, UpdateAvatarDto updateAvatarDto);
    Task DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}
