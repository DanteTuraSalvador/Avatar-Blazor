using System.ComponentModel.DataAnnotations;

namespace Avatar.Core.DTOs;

public class SkillDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public int TeamMemberCount { get; set; } // Number of team members with this skill
}

public class CreateSkillDto
{
    [Required(ErrorMessage = "Skill name is required")]
    [StringLength(100, ErrorMessage = "Skill name cannot exceed 100 characters")]
    public string Name { get; set; } = string.Empty;
    
    [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
    public string? Description { get; set; }
    
    [StringLength(100)]
    public string? CreatedBy { get; set; }
}

public class UpdateSkillDto
{
    [Required(ErrorMessage = "Skill name is required")]
    [StringLength(100, ErrorMessage = "Skill name cannot exceed 100 characters")]
    public string Name { get; set; } = string.Empty;
    
    [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
    public string? Description { get; set; }
    
    [StringLength(100)]
    public string? UpdatedBy { get; set; }
}
