using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Core.Entities;
using TaskManager.Core.Interfaces.Repositories;
using TaskManager.Infrastructure.Data;

namespace TaskManager.Infrastructure.Repositories
{
    public class TaskItemRepository : Repository<TaskItem>, ITaskItemRepository
    {
        public TaskItemRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<TaskItem>> GetTasksByProjectIdAsync(int projectId)
        {
            return await _dbSet
                .Where(t => t.ProjectId == projectId)
                .Include(t => t.Status)
                .Include(t => t.Assignments)
                .ThenInclude(a => a.User)
                .ToListAsync();
        }

        public async Task<IEnumerable<TaskItem>> GetTasksByUserIdAsync(string userId)
        {
            return await _dbSet
                .Where(t => t.CreatedById == userId || t.Assignments.Any(a => a.UserId == userId))
                .Include(t => t.Project)
                .Include(t => t.Status)
                .ToListAsync();
        }

        public async Task<IEnumerable<TaskItem>> GetTasksByStatusIdAsync(int statusId)
        {
            return await _dbSet
                .Where(t => t.StatusId == statusId)
                .Include(t => t.Project)
                .ToListAsync();
        }

        public async Task<TaskItem> GetTaskWithDetailsAsync(int taskId)
        {
            return await _dbSet
                .Include(t => t.Project)
                .Include(t => t.Status)
                .Include(t => t.CreatedBy)
                .Include(t => t.Assignments)
                .ThenInclude(a => a.User)
                .FirstOrDefaultAsync(t => t.Id == taskId);
        }
    }
}