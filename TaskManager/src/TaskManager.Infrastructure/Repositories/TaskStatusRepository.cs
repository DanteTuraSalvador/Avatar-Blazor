using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using TaskManager.Core.Interfaces.Repositories;
using TaskManager.Infrastructure.Data;
using TaskStatus = TaskManager.Core.Entities.TaskStatus;

namespace TaskManager.Infrastructure.Repositories
{
    public class TaskStatusRepository : Repository<TaskStatus>, ITaskStatusRepository
    {
        public TaskStatusRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<TaskStatus> GetByNameAsync(string name)
        {
            return await _dbSet
                .FirstOrDefaultAsync(s => s.Name.ToLower() == name.ToLower());
        }
    }
}