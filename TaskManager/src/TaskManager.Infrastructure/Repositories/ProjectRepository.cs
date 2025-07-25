using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Core.Entities;
using TaskManager.Core.Interfaces.Repositories;
using TaskManager.Infrastructure.Data;

namespace TaskManager.Infrastructure.Repositories
{
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        public ProjectRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Project>> GetProjectsByUserIdAsync(string userId)
        {
            return await _dbSet
                .Where(p => p.CreatedById == userId)
                .ToListAsync();
        }

        public async Task<Project> GetProjectWithTasksAsync(int projectId)
        {
            return await _dbSet
                .Include(p => p.Tasks)
                .ThenInclude(t => t.Status)
                .Include(p => p.Tasks)
                .ThenInclude(t => t.Assignments)
                .ThenInclude(a => a.User)
                .FirstOrDefaultAsync(p => p.Id == projectId);
        }
    }
}