using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Avatar.Core.DTOs;
using Avatar.Core.Interfaces;
using Avatar.Infrastructure;
using Avatar.Infrastructure.Data;

namespace Avatar.IntegrationTests;

public class AvatarIntegrationTests : IDisposable
{
    private readonly ServiceProvider _serviceProvider;
    private readonly AvatarDbContext _context;
    private readonly IAvatarService _avatarService;

    public AvatarIntegrationTests()
    {
        var services = new ServiceCollection();
        
        // Add infrastructure services with in-memory database
        services.AddInfrastructureInMemory();
        
        _serviceProvider = services.BuildServiceProvider();
        _context = _serviceProvider.GetRequiredService<AvatarDbContext>();
        _avatarService = _serviceProvider.GetRequiredService<IAvatarService>();
        
        // Ensure database is created
        _context.Database.EnsureCreated();
    }

    [Fact]
    public async Task CreateAvatar_ValidData_ShouldCreateSuccessfully()
    {
        // Arrange
        var createDto = new CreateAvatarDto
        {
            Name = "Integration Test Avatar",
            Description = "Test avatar for integration testing",
            ImageUrl = "/test-avatar.png",
            Category = "Human",
            IsActive = true,
            CreatedBy = "Integration Test"
        };

        // Act
        var result = await _avatarService.CreateAsync(createDto);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Id > 0);
        Assert.Equal(createDto.Name, result.Name);
        Assert.Equal(createDto.Description, result.Description);
        Assert.Equal(createDto.ImageUrl, result.ImageUrl);
        Assert.Equal(createDto.Category, result.Category);
        Assert.Equal(createDto.IsActive, result.IsActive);
        Assert.Equal(createDto.CreatedBy, result.CreatedBy);
        Assert.True(result.CreatedAt > DateTime.MinValue);

        // Verify in database
        var avatarInDb = await _context.Avatars.FindAsync(result.Id);
        Assert.NotNull(avatarInDb);
        Assert.Equal(createDto.Name, avatarInDb.Name);
    }

    [Fact]
    public async Task GetAllAvatars_WithSeededData_ShouldReturnAvatars()
    {
        // Arrange - Seed some test data
        var avatar1 = new Core.Entities.Avatar
        {
            Name = "Test Avatar 1",
            Description = "First test avatar",
            ImageUrl = "/avatar1.png",
            Category = "Human",
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = "Test"
        };

        var avatar2 = new Core.Entities.Avatar
        {
            Name = "Test Avatar 2",
            Description = "Second test avatar",
            ImageUrl = "/avatar2.png",
            Category = "Robot",
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = "Test"
        };

        _context.Avatars.AddRange(avatar1, avatar2);
        await _context.SaveChangesAsync();

        // Act
        var result = await _avatarService.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Count() >= 2);
        Assert.Contains(result, a => a.Name == "Test Avatar 1");
        Assert.Contains(result, a => a.Name == "Test Avatar 2");
    }

    [Fact]
    public async Task UpdateAvatar_ExistingAvatar_ShouldUpdateSuccessfully()
    {
        // Arrange - Create an avatar first
        var createDto = new CreateAvatarDto
        {
            Name = "Original Avatar",
            Description = "Original description",
            ImageUrl = "/original.png",
            Category = "Human",
            IsActive = true,
            CreatedBy = "Test"
        };

        var createdAvatar = await _avatarService.CreateAsync(createDto);

        var updateDto = new UpdateAvatarDto
        {
            Name = "Updated Avatar",
            Description = "Updated description",
            ImageUrl = "/updated.png",
            Category = "Robot",
            IsActive = false,
            UpdatedBy = "Test Updater"
        };

        // Act
        var result = await _avatarService.UpdateAsync(createdAvatar.Id, updateDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(createdAvatar.Id, result.Id);
        Assert.Equal(updateDto.Name, result.Name);
        Assert.Equal(updateDto.Description, result.Description);
        Assert.Equal(updateDto.ImageUrl, result.ImageUrl);
        Assert.Equal(updateDto.Category, result.Category);
        Assert.Equal(updateDto.IsActive, result.IsActive);
        Assert.Equal(updateDto.UpdatedBy, result.UpdatedBy);
        Assert.NotNull(result.UpdatedAt);

        // Verify in database
        var avatarInDb = await _context.Avatars.FindAsync(result.Id);
        Assert.NotNull(avatarInDb);
        Assert.Equal(updateDto.Name, avatarInDb.Name);
        Assert.Equal(updateDto.UpdatedBy, avatarInDb.UpdatedBy);
    }

    [Fact]
    public async Task DeleteAvatar_ExistingAvatar_ShouldDeleteSuccessfully()
    {
        // Arrange - Create an avatar first
        var createDto = new CreateAvatarDto
        {
            Name = "Avatar to Delete",
            Description = "This avatar will be deleted",
            ImageUrl = "/delete-me.png",
            Category = "Human",
            IsActive = true,
            CreatedBy = "Test"
        };

        var createdAvatar = await _avatarService.CreateAsync(createDto);

        // Act
        await _avatarService.DeleteAsync(createdAvatar.Id);

        // Assert
        var deletedAvatar = await _avatarService.GetByIdAsync(createdAvatar.Id);
        Assert.Null(deletedAvatar);

        // Verify in database
        var avatarInDb = await _context.Avatars.FindAsync(createdAvatar.Id);
        Assert.Null(avatarInDb);
    }

    [Fact]
    public async Task SearchByName_WithMatchingAvatars_ShouldReturnFilteredResults()
    {
        // Arrange - Clear existing data and seed test data
        _context.Avatars.RemoveRange(_context.Avatars);
        await _context.SaveChangesAsync();

        var avatar1 = new Core.Entities.Avatar
        {
            Name = "Superman Avatar",
            Description = "Superhero avatar",
            ImageUrl = "/superman.png",
            Category = "Fantasy",
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = "Test"
        };

        var avatar2 = new Core.Entities.Avatar
        {
            Name = "Batman Avatar",
            Description = "Dark knight avatar",
            ImageUrl = "/batman.png",
            Category = "Fantasy",
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = "Test"
        };

        var avatar3 = new Core.Entities.Avatar
        {
            Name = "Regular Person",
            Description = "Normal avatar",
            ImageUrl = "/person.png",
            Category = "Human",
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = "Test"
        };

        _context.Avatars.AddRange(avatar1, avatar2, avatar3);
        await _context.SaveChangesAsync();

        // Act
        var result = await _avatarService.SearchByNameAsync("man");

        // Assert
        Assert.NotNull(result);
        var resultList = result.ToList();
        Assert.Equal(2, resultList.Count);
        Assert.Contains(resultList, a => a.Name == "Superman Avatar");
        Assert.Contains(resultList, a => a.Name == "Batman Avatar");
        Assert.DoesNotContain(resultList, a => a.Name == "Regular Person");
    }

    [Fact]
    public async Task GetByCategory_WithMatchingAvatars_ShouldReturnFilteredResults()
    {
        // Arrange - Clear existing data and seed test data
        _context.Avatars.RemoveRange(_context.Avatars);
        await _context.SaveChangesAsync();

        var humanAvatar = new Core.Entities.Avatar
        {
            Name = "Human Avatar",
            Description = "Human category avatar",
            ImageUrl = "/human.png",
            Category = "Human",
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = "Test"
        };

        var robotAvatar = new Core.Entities.Avatar
        {
            Name = "Robot Avatar",
            Description = "Robot category avatar",
            ImageUrl = "/robot.png",
            Category = "Robot",
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = "Test"
        };

        _context.Avatars.AddRange(humanAvatar, robotAvatar);
        await _context.SaveChangesAsync();

        // Act
        var result = await _avatarService.GetByCategoryAsync("Robot");

        // Assert
        Assert.NotNull(result);
        var resultList = result.ToList();
        Assert.Single(resultList);
        Assert.Equal("Robot Avatar", resultList.First().Name);
        Assert.Equal("Robot", resultList.First().Category);
    }

    [Fact]
    public async Task GetActiveAvatars_WithMixedActiveStatus_ShouldReturnOnlyActive()
    {
        // Arrange - Seed test data
        var activeAvatar = new Core.Entities.Avatar
        {
            Name = "Active Avatar",
            Description = "This avatar is active",
            ImageUrl = "/active.png",
            Category = "Human",
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = "Test"
        };

        var inactiveAvatar = new Core.Entities.Avatar
        {
            Name = "Inactive Avatar",
            Description = "This avatar is inactive",
            ImageUrl = "/inactive.png",
            Category = "Human",
            IsActive = false,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = "Test"
        };

        _context.Avatars.AddRange(activeAvatar, inactiveAvatar);
        await _context.SaveChangesAsync();

        // Act
        var result = await _avatarService.GetActiveAvatarsAsync();

        // Assert
        Assert.NotNull(result);
        var resultList = result.ToList();
        Assert.All(resultList, a => Assert.True(a.IsActive));
        Assert.Contains(resultList, a => a.Name == "Active Avatar");
        Assert.DoesNotContain(resultList, a => a.Name == "Inactive Avatar");
    }

    public void Dispose()
    {
        _context?.Dispose();
        _serviceProvider?.Dispose();
    }
}
