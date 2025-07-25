using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Core.Entities;
using TaskManager.Core.Interfaces.Repositories;
using TaskManager.Infrastructure.Data;

namespace TaskManager.Infrastructure.Repositories
{
    public class TaskAssignmentRepository : Repository<TaskAssignment>, ITaskAssignmentRepository
    {
        public TaskAssignmentRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<TaskAssignment>> GetByTaskIdAsync(int taskId)
        {
            return await _dbSet
                .Where(a => a.TaskId == taskId)
                .Include(a => a.User)
                .ToListAsync();
        }

        public async Task<IEnumerable<TaskAssignment>> GetByUserIdAsync(string userId)
        {
            return await _dbSet
                .Where(a => a.UserId == userId)
                .Include(a => a.Task)
                .ThenInclude(t => t.Project)
                .Include(a => a.Task)
                .ThenInclude(t => t.Status)
                .ToListAsync();
        }

        public async Task<bool> IsTaskAssignedToUserAsync(int taskId, string userId)
        {
            return await _dbSet
                .AnyAsync(a => a.TaskId == taskId && a.UserId == userId);
        }
    }
}