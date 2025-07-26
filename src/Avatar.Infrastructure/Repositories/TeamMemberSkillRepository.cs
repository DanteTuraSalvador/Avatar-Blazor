using Avatar.Core.Entities;
using Avatar.Core.Interfaces;
using Avatar.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Avatar.Infrastructure.Repositories;

public class TeamMemberSkillRepository : Repository<TeamMemberSkill>, ITeamMemberSkillRepository
{
    public TeamMemberSkillRepository(SkillsDbContext context) : base(context)
    {
    }

    public async Task<bool> TeamMemberHasSkillAsync(int teamMemberId, int skillId)
    {
        return await _dbSet.AnyAsync(tms => tms.TeamMemberId == teamMemberId && tms.SkillId == skillId);
    }

    public async Task<IEnumerable<TeamMemberSkill>> GetSkillsByTeamMemberIdAsync(int teamMemberId)
    {
        return await _dbSet
            .Include(tms => tms.Skill)
            .Include(tms => tms.TeamMember)
            .Where(tms => tms.TeamMemberId == teamMemberId)
            .OrderBy(tms => tms.Skill.Name)
            .ToListAsync();
    }

    public async Task<IEnumerable<TeamMemberSkill>> GetTeamMembersBySkillIdAsync(int skillId)
    {
        return await _dbSet
            .Include(tms => tms.Skill)
            .Include(tms => tms.TeamMember)
            .Where(tms => tms.SkillId == skillId)
            .OrderBy(tms => tms.TeamMember.FirstName)
            .ThenBy(tms => tms.TeamMember.LastName)
            .ToListAsync();
    }

    public async Task<TeamMemberSkill?> GetTeamMemberSkillAsync(int teamMemberId, int skillId)
    {
        return await _dbSet
            .Include(tms => tms.Skill)
            .Include(tms => tms.TeamMember)
            .FirstOrDefaultAsync(tms => tms.TeamMemberId == teamMemberId && tms.SkillId == skillId);
    }

    public async Task<IEnumerable<TeamMemberSkill>> GetAllWithDetailsAsync()
    {
        return await _dbSet
            .Include(tms => tms.Skill)
            .Include(tms => tms.TeamMember)
            .OrderBy(tms => tms.TeamMember.FirstName)
            .ThenBy(tms => tms.TeamMember.LastName)
            .ThenBy(tms => tms.Skill.Name)
            .ToListAsync();
    }
}
