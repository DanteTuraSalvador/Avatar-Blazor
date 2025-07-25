using Avatar.Core.DTOs;
using Avatar.Core.Interfaces;

namespace Avatar.Infrastructure.Services;

public class AvatarService : IAvatarService
{
    private readonly IAvatarRepository _avatarRepository;

    public AvatarService(IAvatarRepository avatarRepository)
    {
        _avatarRepository = avatarRepository;
    }

    public async Task<AvatarDto?> GetByIdAsync(int id)
    {
        var avatar = await _avatarRepository.GetByIdAsync(id);
        return avatar == null ? null : MapToDto(avatar);
    }

    public async Task<IEnumerable<AvatarDto>> GetAllAsync()
    {
        var avatars = await _avatarRepository.GetAllAsync();
        return avatars.Select(MapToDto);
    }

    public async Task<IEnumerable<AvatarDto>> GetByCategoryAsync(string category)
    {
        var avatars = await _avatarRepository.GetByCategory(category);
        return avatars.Select(MapToDto);
    }

    public async Task<IEnumerable<AvatarDto>> GetActiveAvatarsAsync()
    {
        var avatars = await _avatarRepository.GetActiveAvatars();
        return avatars.Select(MapToDto);
    }

    public async Task<IEnumerable<AvatarDto>> SearchByNameAsync(string name)
    {
        var avatars = await _avatarRepository.SearchByName(name);
        return avatars.Select(MapToDto);
    }

    public async Task<AvatarDto> CreateAsync(CreateAvatarDto createAvatarDto)
    {
        var avatar = new Core.Entities.Avatar
        {
            Name = createAvatarDto.Name,
            Description = createAvatarDto.Description,
            ImageUrl = createAvatarDto.ImageUrl,
            Category = createAvatarDto.Category,
            IsActive = createAvatarDto.IsActive,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = createAvatarDto.CreatedBy
        };

        var createdAvatar = await _avatarRepository.AddAsync(avatar);
        return MapToDto(createdAvatar);
    }

    public async Task<AvatarDto> UpdateAsync(int id, UpdateAvatarDto updateAvatarDto)
    {
        var existingAvatar = await _avatarRepository.GetByIdAsync(id);
        if (existingAvatar == null)
        {
            throw new ArgumentException($"Avatar with ID {id} not found.");
        }

        existingAvatar.Name = updateAvatarDto.Name;
        existingAvatar.Description = updateAvatarDto.Description;
        existingAvatar.ImageUrl = updateAvatarDto.ImageUrl;
        existingAvatar.Category = updateAvatarDto.Category;
        existingAvatar.IsActive = updateAvatarDto.IsActive;
        existingAvatar.UpdatedAt = DateTime.UtcNow;
        existingAvatar.UpdatedBy = updateAvatarDto.UpdatedBy;

        var updatedAvatar = await _avatarRepository.UpdateAsync(existingAvatar);
        return MapToDto(updatedAvatar);
    }

    public async Task DeleteAsync(int id)
    {
        if (!await _avatarRepository.ExistsAsync(id))
        {
            throw new ArgumentException($"Avatar with ID {id} not found.");
        }

        await _avatarRepository.DeleteAsync(id);
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _avatarRepository.ExistsAsync(id);
    }

    private static AvatarDto MapToDto(Core.Entities.Avatar avatar)
    {
        return new AvatarDto
        {
            Id = avatar.Id,
            Name = avatar.Name,
            Description = avatar.Description,
            ImageUrl = avatar.ImageUrl,
            Category = avatar.Category,
            IsActive = avatar.IsActive,
            CreatedAt = avatar.CreatedAt,
            UpdatedAt = avatar.UpdatedAt,
            CreatedBy = avatar.CreatedBy,
            UpdatedBy = avatar.UpdatedBy
        };
    }
}
