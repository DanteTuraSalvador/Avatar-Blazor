using System.ComponentModel.DataAnnotations;

namespace Avatar.Core.DTOs;

public class UpdateAvatarDto
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
    
    [StringLength(500)]
    public string? Description { get; set; }
    
    [Required]
    [StringLength(255)]
    public string ImageUrl { get; set; } = string.Empty;
    
    [StringLength(50)]
    public string? Category { get; set; }
    
    public bool IsActive { get; set; }
    
    [StringLength(100)]
    public string? UpdatedBy { get; set; }
}
