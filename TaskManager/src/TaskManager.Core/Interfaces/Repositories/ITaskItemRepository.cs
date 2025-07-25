using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManager.Core.Entities;

namespace TaskManager.Core.Interfaces.Repositories
{
    public interface ITaskItemRepository : IRepository<TaskItem>
    {
        Task<IEnumerable<TaskItem>> GetTasksByProjectIdAsync(int projectId);
        Task<IEnumerable<TaskItem>> GetTasksByUserIdAsync(string userId);
        Task<IEnumerable<TaskItem>> GetTasksByStatusIdAsync(int statusId);
        Task<TaskItem> GetTaskWithDetailsAsync(int taskId);
    }
}