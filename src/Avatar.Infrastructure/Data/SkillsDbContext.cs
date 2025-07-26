using Avatar.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Avatar.Infrastructure.Data;

public class SkillsDbContext : DbContext
{
    public SkillsDbContext(DbContextOptions<SkillsDbContext> options) : base(options)
    {
    }

    public DbSet<Skill> Skills { get; set; }
    public DbSet<TeamMember> TeamMembers { get; set; }
    public DbSet<TeamMemberSkill> TeamMemberSkills { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure Skill entity
        modelBuilder.Entity<Skill>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.UpdatedBy).HasMaxLength(100);
            
            // Create unique index on Name
            entity.HasIndex(e => e.Name).IsUnique();
        });

        // Configure TeamMember entity
        modelBuilder.Entity<TeamMember>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
            entity.Property(e => e.LastName).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Position).HasMaxLength(50);
            entity.Property(e => e.Department).HasMaxLength(50);
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.UpdatedBy).HasMaxLength(100);
            
            // Create unique index on FirstName + LastName combination
            entity.HasIndex(e => new { e.FirstName, e.LastName }).IsUnique();
            
            // Ignore computed property
            entity.Ignore(e => e.FullName);
        });

        // Configure TeamMemberSkill entity
        modelBuilder.Entity<TeamMemberSkill>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Level).IsRequired();
            entity.Property(e => e.AssignedBy).HasMaxLength(100);
            entity.Property(e => e.UpdatedBy).HasMaxLength(100);
            
            // Configure relationships
            entity.HasOne(e => e.TeamMember)
                  .WithMany(tm => tm.TeamMemberSkills)
                  .HasForeignKey(e => e.TeamMemberId)
                  .OnDelete(DeleteBehavior.Cascade);
                  
            entity.HasOne(e => e.Skill)
                  .WithMany(s => s.TeamMemberSkills)
                  .HasForeignKey(e => e.SkillId)
                  .OnDelete(DeleteBehavior.Cascade);
            
            // Create unique index to prevent duplicate skills for same team member
            entity.HasIndex(e => new { e.TeamMemberId, e.SkillId }).IsUnique();
            
            // Ignore computed property
            entity.Ignore(e => e.LevelName);
        });

        // Seed data
        SeedData(modelBuilder);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        // Seed Skills
        modelBuilder.Entity<Skill>().HasData(
            new Skill
            {
                Id = 1,
                Name = "C# Programming",
                Description = "Object-oriented programming with C# language",
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "System"
            },
            new Skill
            {
                Id = 2,
                Name = "Blazor Development",
                Description = "Building web applications with Blazor framework",
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "System"
            },
            new Skill
            {
                Id = 3,
                Name = "Entity Framework Core",
                Description = "Data access with EF Core ORM",
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "System"
            },
            new Skill
            {
                Id = 4,
                Name = "SQL Server",
                Description = "Database design and T-SQL programming",
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "System"
            },
            new Skill
            {
                Id = 5,
                Name = "JavaScript",
                Description = "Client-side scripting and modern JS frameworks",
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "System"
            }
        );

        // Seed Team Members
        modelBuilder.Entity<TeamMember>().HasData(
            new TeamMember
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@company.com",
                Position = "Senior Developer",
                Department = "Technology",
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "System"
            },
            new TeamMember
            {
                Id = 2,
                FirstName = "Jane",
                LastName = "Smith",
                Email = "jane.smith@company.com",
                Position = "Lead Developer",
                Department = "Technology",
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "System"
            },
            new TeamMember
            {
                Id = 3,
                FirstName = "Mike",
                LastName = "Johnson",
                Email = "mike.johnson@company.com",
                Position = "Full Stack Developer",
                Department = "Technology",
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "System"
            }
        );

        // Seed Team Member Skills
        modelBuilder.Entity<TeamMemberSkill>().HasData(
            new TeamMemberSkill
            {
                Id = 1,
                TeamMemberId = 1,
                SkillId = 1, // C# Programming
                Level = 4, // Expert
                AssignedAt = DateTime.UtcNow,
                AssignedBy = "System"
            },
            new TeamMemberSkill
            {
                Id = 2,
                TeamMemberId = 1,
                SkillId = 3, // Entity Framework Core
                Level = 3, // Advanced
                AssignedAt = DateTime.UtcNow,
                AssignedBy = "System"
            },
            new TeamMemberSkill
            {
                Id = 3,
                TeamMemberId = 2,
                SkillId = 1, // C# Programming
                Level = 5, // Master
                AssignedAt = DateTime.UtcNow,
                AssignedBy = "System"
            },
            new TeamMemberSkill
            {
                Id = 4,
                TeamMemberId = 2,
                SkillId = 2, // Blazor Development
                Level = 4, // Expert
                AssignedAt = DateTime.UtcNow,
                AssignedBy = "System"
            },
            new TeamMemberSkill
            {
                Id = 5,
                TeamMemberId = 3,
                SkillId = 5, // JavaScript
                Level = 4, // Expert
                AssignedAt = DateTime.UtcNow,
                AssignedBy = "System"
            }
        );
    }
}
