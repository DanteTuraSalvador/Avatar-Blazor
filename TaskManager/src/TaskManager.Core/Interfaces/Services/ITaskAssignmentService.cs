using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManager.Core.Entities;

namespace TaskManager.Core.Interfaces.Services
{
    public interface ITaskAssignmentService
    {
        Task<IEnumerable<TaskAssignment>> GetAssignmentsByTaskIdAsync(int taskId);
        Task<IEnumerable<TaskAssignment>> GetAssignmentsByUserIdAsync(string userId);
        Task<TaskAssignment> GetAssignmentByIdAsync(int id);
        Task<TaskAssignment> AssignTaskToUserAsync(int taskId, string userId);
        Task<bool> UnassignTaskFromUserAsync(int taskId, string userId);
        Task<bool> IsTaskAssignedToUserAsync(int taskId, string userId);
    }
}