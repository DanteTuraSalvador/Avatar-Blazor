using Avatar.Core.Entities;
using Avatar.Core.Interfaces;
using Avatar.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Avatar.Infrastructure.Repositories;

public class SkillRepository : Repository<Skill>, ISkillRepository
{
    public SkillRepository(SkillsDbContext context) : base(context)
    {
    }

    public async Task<bool> IsSkillNameUniqueAsync(string name, int? excludeId = null)
    {
        var query = _dbSet.Where(s => s.Name.ToLower() == name.ToLower());
        
        if (excludeId.HasValue)
        {
            query = query.Where(s => s.Id != excludeId.Value);
        }
        
        return !await query.AnyAsync();
    }

    public async Task<IEnumerable<Skill>> SearchByNameAsync(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            return await GetAllAsync();
        }

        return await _dbSet
            .Where(s => s.Name.ToLower().Contains(searchTerm.ToLower()) ||
                       (s.Description != null && s.Description.ToLower().Contains(searchTerm.ToLower())))
            .OrderBy(s => s.Name)
            .ToListAsync();
    }

    public async Task<Skill?> GetSkillWithTeamMembersAsync(int id)
    {
        return await _dbSet
            .Include(s => s.TeamMemberSkills)
            .ThenInclude(tms => tms.TeamMember)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<IEnumerable<Skill>> GetSkillsWithTeamMemberCountAsync()
    {
        return await _dbSet
            .Include(s => s.TeamMemberSkills)
            .OrderBy(s => s.Name)
            .ToListAsync();
    }
}
