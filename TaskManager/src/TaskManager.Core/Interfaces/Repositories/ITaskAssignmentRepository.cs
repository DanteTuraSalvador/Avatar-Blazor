using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManager.Core.Entities;

namespace TaskManager.Core.Interfaces.Repositories
{
    public interface ITaskAssignmentRepository : IRepository<TaskAssignment>
    {
        Task<IEnumerable<TaskAssignment>> GetByTaskIdAsync(int taskId);
        Task<IEnumerable<TaskAssignment>> GetByUserIdAsync(string userId);
        Task<bool> IsTaskAssignedToUserAsync(int taskId, string userId);
    }
}