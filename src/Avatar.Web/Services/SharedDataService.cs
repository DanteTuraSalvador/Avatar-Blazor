namespace Avatar.Web.Services;

public static class SharedDataService
{
    // Shared Skills Data
    private static List<SkillItem> _skills = new();
    
    public static List<SkillItem> Skills 
    { 
        get 
        {
            if (!_skills.Any())
            {
                InitializeDefaultSkills();
            }
            return _skills;
        }
    }

    // Shared Team Members Data
    private static List<TeamMemberItem> _teamMembers = new();
    
    public static List<TeamMemberItem> TeamMembers 
    { 
        get 
        {
            if (!_teamMembers.Any())
            {
                InitializeDefaultTeamMembers();
            }
            return _teamMembers;
        }
    }

    // Shared Skills Assignments Data
    private static Dictionary<string, Dictionary<string, int>> _skillAssignments = new();
    
    public static Dictionary<string, Dictionary<string, int>> SkillAssignments => _skillAssignments;

    private static void InitializeDefaultSkills()
    {
        _skills.AddRange(new List<SkillItem>
        {
            new SkillItem { Id = 1, Name = "C# Programming", Category = "Programming", Description = "Object-oriented programming with C# language", TeamMemberCount = 2, CreatedAt = DateTime.Today.AddDays(-30) },
            new SkillItem { Id = 2, Name = "Blazor Development", Category = "Framework", Description = "Building web applications with Blazor framework", TeamMemberCount = 1, CreatedAt = DateTime.Today.AddDays(-25) },
            new SkillItem { Id = 3, Name = "Entity Framework Core", Category = "Database", Description = "Data access with EF Core ORM", TeamMemberCount = 1, CreatedAt = DateTime.Today.AddDays(-20) },
            new SkillItem { Id = 4, Name = "SQL Server", Category = "Database", Description = "Database design and T-SQL programming", TeamMemberCount = 0, CreatedAt = DateTime.Today.AddDays(-15) },
            new SkillItem { Id = 5, Name = "JavaScript", Category = "Programming", Description = "Client-side scripting and modern JS frameworks", TeamMemberCount = 1, CreatedAt = DateTime.Today.AddDays(-10) },
            new SkillItem { Id = 6, Name = "Azure", Category = "Cloud", Description = "Microsoft Azure cloud platform services", TeamMemberCount = 0, CreatedAt = DateTime.Today.AddDays(-5) }
        });
    }

    private static void InitializeDefaultTeamMembers()
    {
        _teamMembers.AddRange(new List<TeamMemberItem>
        {
            new TeamMemberItem { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@company.com", Position = "Senior Developer", Department = "Technology", SkillCount = 2 },
            new TeamMemberItem { Id = 2, FirstName = "Jane", LastName = "Smith", Email = "jane.smith@company.com", Position = "Lead Developer", Department = "Technology", SkillCount = 3 },
            new TeamMemberItem { Id = 3, FirstName = "Mike", LastName = "Johnson", Email = "mike.johnson@company.com", Position = "Full Stack Developer", Department = "Technology", SkillCount = 1 }
        });
    }

    // Methods to add/update/delete skills
    public static void AddSkill(SkillItem skill)
    {
        _skills.Add(skill);
    }

    public static void UpdateSkill(SkillItem skill)
    {
        var existing = _skills.FirstOrDefault(s => s.Id == skill.Id);
        if (existing != null)
        {
            existing.Name = skill.Name;
            existing.Category = skill.Category;
            existing.Description = skill.Description;
        }
    }

    public static void RemoveSkill(SkillItem skill)
    {
        _skills.Remove(skill);
    }

    // Methods to add/update/delete team members
    public static void AddTeamMember(TeamMemberItem teamMember)
    {
        _teamMembers.Add(teamMember);
    }

    public static void UpdateTeamMember(TeamMemberItem teamMember)
    {
        var existing = _teamMembers.FirstOrDefault(tm => tm.Id == teamMember.Id);
        if (existing != null)
        {
            existing.FirstName = teamMember.FirstName;
            existing.LastName = teamMember.LastName;
            existing.Email = teamMember.Email;
            existing.Position = teamMember.Position;
            existing.Department = teamMember.Department;
        }
    }

    public static void RemoveTeamMember(TeamMemberItem teamMember)
    {
        _teamMembers.Remove(teamMember);
        // Also remove skill assignments for this member
        var memberKey = $"{teamMember.FirstName} {teamMember.LastName}";
        _skillAssignments.Remove(memberKey);
    }

    // Methods to manage skill assignments
    public static void AssignSkill(string memberName, string skillName, int level)
    {
        if (!_skillAssignments.ContainsKey(memberName))
        {
            _skillAssignments[memberName] = new Dictionary<string, int>();
        }
        _skillAssignments[memberName][skillName] = level;
    }

    public static void RemoveSkillAssignment(string memberName, string skillName)
    {
        if (_skillAssignments.ContainsKey(memberName))
        {
            _skillAssignments[memberName].Remove(skillName);
        }
    }

    public static Dictionary<string, int> GetMemberSkills(string memberName)
    {
        return _skillAssignments.ContainsKey(memberName) 
            ? _skillAssignments[memberName] 
            : new Dictionary<string, int>();
    }
}

// Data models (moved from individual pages)
public class SkillItem
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Category { get; set; } = "";
    public string Description { get; set; } = "";
    public int TeamMemberCount { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class TeamMemberItem
{
    public int Id { get; set; }
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string Email { get; set; } = "";
    public string Position { get; set; } = "";
    public string Department { get; set; } = "";
    public int SkillCount { get; set; }
    public string FullName => $"{FirstName} {LastName}";
}
