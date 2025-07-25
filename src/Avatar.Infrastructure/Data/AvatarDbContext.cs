using Microsoft.EntityFrameworkCore;
using Avatar.Core.Entities;

namespace Avatar.Infrastructure.Data;

public class AvatarDbContext : DbContext
{
    public AvatarDbContext(DbContextOptions<AvatarDbContext> options) : base(options)
    {
    }

    public DbSet<Core.Entities.Avatar> Avatars { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure Avatar entity
        modelBuilder.Entity<Core.Entities.Avatar>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);
                
            entity.Property(e => e.Description)
                .HasMaxLength(500);
                
            entity.Property(e => e.ImageUrl)
                .IsRequired()
                .HasMaxLength(255);
                
            entity.Property(e => e.Category)
                .HasMaxLength(50);
                
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100);
                
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(100);
                
            entity.Property(e => e.CreatedAt)
                .IsRequired();
                
            entity.HasIndex(e => e.Name);
            entity.HasIndex(e => e.Category);
            entity.HasIndex(e => e.IsActive);
        });

        // Seed data
        modelBuilder.Entity<Core.Entities.Avatar>().HasData(
            new Core.Entities.Avatar
            {
                Id = 1,
                Name = "Default Avatar",
                Description = "A default avatar for new users",
                ImageUrl = "https://via.placeholder.com/300x300/4A90E2/FFFFFF?text=Default+Avatar",
                Category = "Human",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "System"
            },
            new Core.Entities.Avatar
            {
                Id = 2,
                Name = "Robot Avatar",
                Description = "A futuristic robot avatar",
                ImageUrl = "https://via.placeholder.com/300x300/E94B3C/FFFFFF?text=Robot+Avatar",
                Category = "Robot",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "System"
            }
        );
    }
}
