using System.Threading.Tasks;
using TaskStatus = TaskManager.Core.Entities.TaskStatus;

namespace TaskManager.Core.Interfaces.Repositories
{
    public interface ITaskStatusRepository : IRepository<TaskStatus>
    {
        Task<TaskStatus> GetByNameAsync(string name);
    }
}