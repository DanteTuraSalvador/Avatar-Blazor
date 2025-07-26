using System.ComponentModel.DataAnnotations;

namespace Avatar.Core.DTOs;

public class TeamMemberDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Position { get; set; }
    public string? Department { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public int SkillCount { get; set; } // Number of skills assigned to this team member
    public List<TeamMemberSkillDto> Skills { get; set; } = new();
}

public class CreateTeamMemberDto
{
    [Required(ErrorMessage = "First name is required")]
    [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters")]
    public string FirstName { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Last name is required")]
    [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters")]
    public string LastName { get; set; } = string.Empty;
    
    [EmailAddress(ErrorMessage = "Invalid email format")]
    [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters")]
    public string? Email { get; set; }
    
    [StringLength(50, ErrorMessage = "Position cannot exceed 50 characters")]
    public string? Position { get; set; }
    
    [StringLength(50, ErrorMessage = "Department cannot exceed 50 characters")]
    public string? Department { get; set; }
    
    [StringLength(100)]
    public string? CreatedBy { get; set; }
}

public class UpdateTeamMemberDto
{
    [Required(ErrorMessage = "First name is required")]
    [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters")]
    public string FirstName { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Last name is required")]
    [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters")]
    public string LastName { get; set; } = string.Empty;
    
    [EmailAddress(ErrorMessage = "Invalid email format")]
    [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters")]
    public string? Email { get; set; }
    
    [StringLength(50, ErrorMessage = "Position cannot exceed 50 characters")]
    public string? Position { get; set; }
    
    [StringLength(50, ErrorMessage = "Department cannot exceed 50 characters")]
    public string? Department { get; set; }
    
    [StringLength(100)]
    public string? UpdatedBy { get; set; }
}
