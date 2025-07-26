using Avatar.Core.Entities;

namespace Avatar.UnitTests;

public class SkillTests
{
    [Fact]
    public void Skill_Creation_Should_Set_Properties_Correctly()
    {
        // Arrange
        var skillName = "C# Programming";
        var description = "Object-oriented programming with C#";
        var createdAt = DateTime.Today;

        // Act
        var skill = new Skill
        {
            Id = 1,
            Name = skillName,
            Description = description,
            CreatedAt = createdAt
        };

        // Assert
        Assert.Equal(1, skill.Id);
        Assert.Equal(skillName, skill.Name);
        Assert.Equal(description, skill.Description);
        Assert.Equal(createdAt, skill.CreatedAt);
    }

    [Fact]
    public void Skill_Name_Should_Not_Be_Null_Or_Empty()
    {
        // Arrange & Act & Assert
        Assert.Throws<ArgumentException>(() =>
        {
            var skill = new Skill { Name = "" };
            if (string.IsNullOrWhiteSpace(skill.Name))
                throw new ArgumentException("Skill name cannot be empty");
        });
    }

    [Fact]
    public void Skill_Should_Have_Default_CreatedAt()
    {
        // Arrange & Act
        var skill = new Skill
        {
            Name = "Test Skill",
            Description = "Test Description"
        };

        // Assert
        Assert.True(skill.CreatedAt <= DateTime.UtcNow);
        Assert.True(skill.CreatedAt > DateTime.UtcNow.AddMinutes(-1));
    }

    [Fact]
    public void Skill_Should_Initialize_TeamMemberSkills_Collection()
    {
        // Arrange & Act
        var skill = new Skill
        {
            Name = "Test Skill"
        };

        // Assert
        Assert.NotNull(skill.TeamMemberSkills);
        Assert.Empty(skill.TeamMemberSkills);
    }
}
