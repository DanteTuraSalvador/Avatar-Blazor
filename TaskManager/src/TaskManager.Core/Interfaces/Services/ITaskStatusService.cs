using System.Collections.Generic;
using System.Threading.Tasks;
using TaskStatus = TaskManager.Core.Entities.TaskStatus;

namespace TaskManager.Core.Interfaces.Services
{
    public interface ITaskStatusService
    {
        Task<IEnumerable<TaskStatus>> GetAllStatusesAsync();
        Task<TaskStatus> GetStatusByIdAsync(int id);
        Task<TaskStatus> GetStatusByNameAsync(string name);
        Task<TaskStatus> CreateStatusAsync(TaskStatus status);
        Task UpdateStatusAsync(TaskStatus status);
        Task DeleteStatusAsync(int id);
    }
}