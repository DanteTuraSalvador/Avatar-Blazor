using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManager.Core.Interfaces.Repositories;
using TaskManager.Core.Interfaces.Services;
using TaskStatus = TaskManager.Core.Entities.TaskStatus;

namespace TaskManager.Infrastructure.Services
{
    public class TaskStatusService : ITaskStatusService
    {
        private readonly ITaskStatusRepository _statusRepository;

        public TaskStatusService(ITaskStatusRepository statusRepository)
        {
            _statusRepository = statusRepository;
        }

        public async Task<IEnumerable<TaskStatus>> GetAllStatusesAsync()
        {
            return await _statusRepository.GetAllAsync();
        }

        public async Task<TaskStatus> GetStatusByIdAsync(int id)
        {
            return await _statusRepository.GetByIdAsync(id);
        }

        public async Task<TaskStatus> GetStatusByNameAsync(string name)
        {
            return await _statusRepository.GetByNameAsync(name);
        }

        public async Task<TaskStatus> CreateStatusAsync(TaskStatus status)
        {
            return await _statusRepository.AddAsync(status);
        }

        public async Task UpdateStatusAsync(TaskStatus status)
        {
            await _statusRepository.UpdateAsync(status);
        }

        public async Task DeleteStatusAsync(int id)
        {
            await _statusRepository.DeleteAsync(id);
        }
    }
}