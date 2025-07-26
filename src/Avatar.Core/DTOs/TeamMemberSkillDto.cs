using System.ComponentModel.DataAnnotations;

namespace Avatar.Core.DTOs;

public class TeamMemberSkillDto
{
    public int Id { get; set; }
    public int TeamMemberId { get; set; }
    public int SkillId { get; set; }
    public string TeamMemberName { get; set; } = string.Empty;
    public string SkillName { get; set; } = string.Empty;
    public int Level { get; set; }
    public string LevelName { get; set; } = string.Empty;
    public DateTime AssignedAt { get; set; }
    public string? AssignedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
}

public class CreateTeamMemberSkillDto
{
    [Required(ErrorMessage = "Team member is required")]
    public int TeamMemberId { get; set; }
    
    [Required(ErrorMessage = "Skill is required")]
    public int SkillId { get; set; }
    
    [Required(ErrorMessage = "Level is required")]
    [Range(1, 5, ErrorMessage = "Level must be between 1 and 5")]
    public int Level { get; set; }
    
    [StringLength(100)]
    public string? AssignedBy { get; set; }
}

public class UpdateTeamMemberSkillDto
{
    [Required(ErrorMessage = "Level is required")]
    [Range(1, 5, ErrorMessage = "Level must be between 1 and 5")]
    public int Level { get; set; }
    
    [StringLength(100)]
    public string? UpdatedBy { get; set; }
}

public class TeamMemberSkillsBuilderDto
{
    public TeamMemberDto TeamMember { get; set; } = new();
    public List<SkillDto> AvailableSkills { get; set; } = new();
    public List<TeamMemberSkillDto> AssignedSkills { get; set; } = new();
}
