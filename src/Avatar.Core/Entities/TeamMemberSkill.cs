using System.ComponentModel.DataAnnotations;

namespace Avatar.Core.Entities;

public class TeamMemberSkill
{
    public int Id { get; set; }
    
    public int TeamMemberId { get; set; }
    
    public int SkillId { get; set; }
    
    [Required]
    [Range(1, 5)]
    public int Level { get; set; } // 1 = Beginner, 2 = Intermediate, 3 = Advanced, 4 = Expert, 5 = Master
    
    public DateTime AssignedAt { get; set; } = DateTime.UtcNow;
    
    [StringLength(100)]
    public string? AssignedBy { get; set; }
    
    public DateTime? UpdatedAt { get; set; }
    
    [StringLength(100)]
    public string? UpdatedBy { get; set; }
    
    // Navigation properties
    public virtual TeamMember TeamMember { get; set; } = null!;
    public virtual Skill Skill { get; set; } = null!;
    
    // Helper property for level display
    public string LevelName => Level switch
    {
        1 => "Beginner",
        2 => "Intermediate", 
        3 => "Advanced",
        4 => "Expert",
        5 => "Master",
        _ => "Unknown"
    };
}
