using Avatar.Core.Entities;
using Avatar.Core.Interfaces;
using Avatar.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Avatar.Infrastructure.Repositories;

public class TeamMemberRepository : Repository<TeamMember>, ITeamMemberRepository
{
    public TeamMemberRepository(SkillsDbContext context) : base(context)
    {
    }

    public async Task<bool> IsTeamMemberNameUniqueAsync(string firstName, string lastName, int? excludeId = null)
    {
        var query = _dbSet.Where(tm => tm.FirstName.ToLower() == firstName.ToLower() && 
                                      tm.LastName.ToLower() == lastName.ToLower());
        
        if (excludeId.HasValue)
        {
            query = query.Where(tm => tm.Id != excludeId.Value);
        }
        
        return !await query.AnyAsync();
    }

    public async Task<IEnumerable<TeamMember>> SearchByNameAsync(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            return await GetAllAsync();
        }

        return await _dbSet
            .Where(tm => tm.FirstName.ToLower().Contains(searchTerm.ToLower()) ||
                        tm.LastName.ToLower().Contains(searchTerm.ToLower()) ||
                        (tm.Email != null && tm.Email.ToLower().Contains(searchTerm.ToLower())) ||
                        (tm.Position != null && tm.Position.ToLower().Contains(searchTerm.ToLower())) ||
                        (tm.Department != null && tm.Department.ToLower().Contains(searchTerm.ToLower())))
            .OrderBy(tm => tm.FirstName)
            .ThenBy(tm => tm.LastName)
            .ToListAsync();
    }

    public async Task<TeamMember?> GetTeamMemberWithSkillsAsync(int id)
    {
        return await _dbSet
            .Include(tm => tm.TeamMemberSkills)
            .ThenInclude(tms => tms.Skill)
            .FirstOrDefaultAsync(tm => tm.Id == id);
    }

    public async Task<IEnumerable<TeamMember>> GetTeamMembersWithSkillCountAsync()
    {
        return await _dbSet
            .Include(tm => tm.TeamMemberSkills)
            .OrderBy(tm => tm.FirstName)
            .ThenBy(tm => tm.LastName)
            .ToListAsync();
    }
}
