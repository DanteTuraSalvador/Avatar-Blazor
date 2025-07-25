using Xunit;
using Moq;
using Avatar.Core.Interfaces;
using Avatar.Core.DTOs;
using Avatar.Infrastructure.Services;

namespace Avatar.UnitTests.Services;

public class AvatarServiceTests
{
    private readonly Mock<IAvatarRepository> _mockRepository;
    private readonly AvatarService _service;

    public AvatarServiceTests()
    {
        _mockRepository = new Mock<IAvatarRepository>();
        _service = new AvatarService(_mockRepository.Object);
    }

    [Fact]
    public async Task GetByIdAsync_ExistingId_ReturnsAvatarDto()
    {
        // Arrange
        var avatarId = 1;
        var avatar = new Core.Entities.Avatar
        {
            Id = avatarId,
            Name = "Test Avatar",
            Description = "Test Description",
            ImageUrl = "/test.png",
            Category = "Human",
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = "Test User"
        };

        _mockRepository.Setup(r => r.GetByIdAsync(avatarId))
                      .ReturnsAsync(avatar);

        // Act
        var result = await _service.GetByIdAsync(avatarId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(avatar.Id, result.Id);
        Assert.Equal(avatar.Name, result.Name);
        Assert.Equal(avatar.Description, result.Description);
        Assert.Equal(avatar.ImageUrl, result.ImageUrl);
        Assert.Equal(avatar.Category, result.Category);
        Assert.Equal(avatar.IsActive, result.IsActive);
    }

    [Fact]
    public async Task GetByIdAsync_NonExistingId_ReturnsNull()
    {
        // Arrange
        var avatarId = 999;
        _mockRepository.Setup(r => r.GetByIdAsync(avatarId))
                      .ReturnsAsync((Core.Entities.Avatar?)null);

        // Act
        var result = await _service.GetByIdAsync(avatarId);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task CreateAsync_ValidDto_ReturnsCreatedAvatarDto()
    {
        // Arrange
        var createDto = new CreateAvatarDto
        {
            Name = "New Avatar",
            Description = "New Description",
            ImageUrl = "/new.png",
            Category = "Robot",
            IsActive = true,
            CreatedBy = "Test User"
        };

        var createdAvatar = new Core.Entities.Avatar
        {
            Id = 1,
            Name = createDto.Name,
            Description = createDto.Description,
            ImageUrl = createDto.ImageUrl,
            Category = createDto.Category,
            IsActive = createDto.IsActive,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = createDto.CreatedBy
        };

        _mockRepository.Setup(r => r.AddAsync(It.IsAny<Core.Entities.Avatar>()))
                      .ReturnsAsync(createdAvatar);

        // Act
        var result = await _service.CreateAsync(createDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(createdAvatar.Id, result.Id);
        Assert.Equal(createDto.Name, result.Name);
        Assert.Equal(createDto.Description, result.Description);
        Assert.Equal(createDto.ImageUrl, result.ImageUrl);
        Assert.Equal(createDto.Category, result.Category);
        Assert.Equal(createDto.IsActive, result.IsActive);
        
        _mockRepository.Verify(r => r.AddAsync(It.IsAny<Core.Entities.Avatar>()), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ExistingAvatar_ReturnsUpdatedAvatarDto()
    {
        // Arrange
        var avatarId = 1;
        var updateDto = new UpdateAvatarDto
        {
            Name = "Updated Avatar",
            Description = "Updated Description",
            ImageUrl = "/updated.png",
            Category = "Fantasy",
            IsActive = false,
            UpdatedBy = "Test User"
        };

        var existingAvatar = new Core.Entities.Avatar
        {
            Id = avatarId,
            Name = "Original Avatar",
            Description = "Original Description",
            ImageUrl = "/original.png",
            Category = "Human",
            IsActive = true,
            CreatedAt = DateTime.UtcNow.AddDays(-1),
            CreatedBy = "Original User"
        };

        var updatedAvatar = new Core.Entities.Avatar
        {
            Id = avatarId,
            Name = updateDto.Name,
            Description = updateDto.Description,
            ImageUrl = updateDto.ImageUrl,
            Category = updateDto.Category,
            IsActive = updateDto.IsActive,
            CreatedAt = existingAvatar.CreatedAt,
            CreatedBy = existingAvatar.CreatedBy,
            UpdatedAt = DateTime.UtcNow,
            UpdatedBy = updateDto.UpdatedBy
        };

        _mockRepository.Setup(r => r.GetByIdAsync(avatarId))
                      .ReturnsAsync(existingAvatar);
        _mockRepository.Setup(r => r.UpdateAsync(It.IsAny<Core.Entities.Avatar>()))
                      .ReturnsAsync(updatedAvatar);

        // Act
        var result = await _service.UpdateAsync(avatarId, updateDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(updateDto.Name, result.Name);
        Assert.Equal(updateDto.Description, result.Description);
        Assert.Equal(updateDto.ImageUrl, result.ImageUrl);
        Assert.Equal(updateDto.Category, result.Category);
        Assert.Equal(updateDto.IsActive, result.IsActive);
        Assert.Equal(updateDto.UpdatedBy, result.UpdatedBy);
        
        _mockRepository.Verify(r => r.GetByIdAsync(avatarId), Times.Once);
        _mockRepository.Verify(r => r.UpdateAsync(It.IsAny<Core.Entities.Avatar>()), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_NonExistingAvatar_ThrowsArgumentException()
    {
        // Arrange
        var avatarId = 999;
        var updateDto = new UpdateAvatarDto
        {
            Name = "Updated Avatar",
            Description = "Updated Description",
            ImageUrl = "/updated.png",
            Category = "Fantasy",
            IsActive = false,
            UpdatedBy = "Test User"
        };

        _mockRepository.Setup(r => r.GetByIdAsync(avatarId))
                      .ReturnsAsync((Core.Entities.Avatar?)null);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(
            () => _service.UpdateAsync(avatarId, updateDto));
        
        Assert.Contains($"Avatar with ID {avatarId} not found", exception.Message);
    }

    [Fact]
    public async Task DeleteAsync_ExistingAvatar_CallsRepositoryDelete()
    {
        // Arrange
        var avatarId = 1;
        _mockRepository.Setup(r => r.ExistsAsync(avatarId))
                      .ReturnsAsync(true);

        // Act
        await _service.DeleteAsync(avatarId);

        // Assert
        _mockRepository.Verify(r => r.ExistsAsync(avatarId), Times.Once);
        _mockRepository.Verify(r => r.DeleteAsync(avatarId), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_NonExistingAvatar_ThrowsArgumentException()
    {
        // Arrange
        var avatarId = 999;
        _mockRepository.Setup(r => r.ExistsAsync(avatarId))
                      .ReturnsAsync(false);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(
            () => _service.DeleteAsync(avatarId));
        
        Assert.Contains($"Avatar with ID {avatarId} not found", exception.Message);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsAllAvatars()
    {
        // Arrange
        var avatars = new List<Core.Entities.Avatar>
        {
            new() { Id = 1, Name = "Avatar 1", ImageUrl = "/1.png", IsActive = true, CreatedAt = DateTime.UtcNow },
            new() { Id = 2, Name = "Avatar 2", ImageUrl = "/2.png", IsActive = true, CreatedAt = DateTime.UtcNow }
        };

        _mockRepository.Setup(r => r.GetAllAsync())
                      .ReturnsAsync(avatars);

        // Act
        var result = await _service.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.Contains(result, a => a.Name == "Avatar 1");
        Assert.Contains(result, a => a.Name == "Avatar 2");
    }

    [Fact]
    public async Task GetActiveAvatarsAsync_ReturnsOnlyActiveAvatars()
    {
        // Arrange
        var activeAvatars = new List<Core.Entities.Avatar>
        {
            new() { Id = 1, Name = "Active Avatar", ImageUrl = "/1.png", IsActive = true, CreatedAt = DateTime.UtcNow }
        };

        _mockRepository.Setup(r => r.GetActiveAvatars())
                      .ReturnsAsync(activeAvatars);

        // Act
        var result = await _service.GetActiveAvatarsAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.All(result, a => Assert.True(a.IsActive));
    }
}
