using System.ComponentModel.DataAnnotations;

namespace Avatar.Core.Entities;

public class TeamMember
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(50)]
    public string FirstName { get; set; } = string.Empty;
    
    [Required]
    [StringLength(50)]
    public string LastName { get; set; } = string.Empty;
    
    [StringLength(100)]
    public string? Email { get; set; }
    
    [StringLength(50)]
    public string? Position { get; set; }
    
    [StringLength(50)]
    public string? Department { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    [StringLength(100)]
    public string? CreatedBy { get; set; }
    
    public DateTime? UpdatedAt { get; set; }
    
    [StringLength(100)]
    public string? UpdatedBy { get; set; }
    
    // Computed property for full name
    public string FullName => $"{FirstName} {LastName}";
    
    // Navigation property
    public virtual ICollection<TeamMemberSkill> TeamMemberSkills { get; set; } = new List<TeamMemberSkill>();
}
