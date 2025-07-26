using System.ComponentModel.DataAnnotations;

namespace Avatar.Core.Entities;

public class Skill
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
    
    [StringLength(500)]
    public string? Description { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    [StringLength(100)]
    public string? CreatedBy { get; set; }
    
    public DateTime? UpdatedAt { get; set; }
    
    [StringLength(100)]
    public string? UpdatedBy { get; set; }
    
    // Navigation property
    public virtual ICollection<TeamMemberSkill> TeamMemberSkills { get; set; } = new List<TeamMemberSkill>();
}
