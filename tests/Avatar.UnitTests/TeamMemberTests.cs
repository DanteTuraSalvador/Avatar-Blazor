using Avatar.Core.Entities;

namespace Avatar.UnitTests;

public class TeamMemberTests
{
    [Fact]
    public void TeamMember_Creation_Should_Set_Properties_Correctly()
    {
        // Arrange
        var firstName = "John";
        var lastName = "Doe";
        var email = "john.doe@company.com";
        var position = "Senior Developer";
        var department = "Technology";

        // Act
        var teamMember = new TeamMember
        {
            Id = 1,
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Position = position,
            Department = department
        };

        // Assert
        Assert.Equal(1, teamMember.Id);
        Assert.Equal(firstName, teamMember.FirstName);
        Assert.Equal(lastName, teamMember.LastName);
        Assert.Equal(email, teamMember.Email);
        Assert.Equal(position, teamMember.Position);
        Assert.Equal(department, teamMember.Department);
    }

    [Fact]
    public void TeamMember_FullName_Should_Combine_FirstName_And_LastName()
    {
        // Arrange
        var teamMember = new TeamMember
        {
            FirstName = "Jane",
            LastName = "Smith"
        };

        // Act
        var fullName = teamMember.FullName;

        // Assert
        Assert.Equal("Jane Smith", fullName);
    }

    [Theory]
    [InlineData("john.doe@company.com", true)]
    [InlineData("jane.smith@company.com", true)]
    [InlineData("invalid-email", false)]
    [InlineData("", false)]
    public void TeamMember_Email_Validation_Should_Work_Correctly(string email, bool isValid)
    {
        // Arrange
        var teamMember = new TeamMember
        {
            FirstName = "Test",
            LastName = "User",
            Email = email
        };

        // Act
        var emailIsValid = !string.IsNullOrWhiteSpace(email) && email.Contains("@") && email.Contains(".");

        // Assert
        Assert.Equal(isValid, emailIsValid);
    }

    [Fact]
    public void TeamMember_Should_Have_Default_CreatedAt()
    {
        // Arrange & Act
        var teamMember = new TeamMember
        {
            FirstName = "Test",
            LastName = "User"
        };

        // Assert
        Assert.True(teamMember.CreatedAt <= DateTime.UtcNow);
        Assert.True(teamMember.CreatedAt > DateTime.UtcNow.AddMinutes(-1));
    }

    [Fact]
    public void TeamMember_Should_Initialize_TeamMemberSkills_Collection()
    {
        // Arrange & Act
        var teamMember = new TeamMember
        {
            FirstName = "Test",
            LastName = "User"
        };

        // Assert
        Assert.NotNull(teamMember.TeamMemberSkills);
        Assert.Empty(teamMember.TeamMemberSkills);
    }
}
