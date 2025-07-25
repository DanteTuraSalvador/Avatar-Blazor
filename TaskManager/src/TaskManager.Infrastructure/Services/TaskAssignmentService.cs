using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Core.Entities;
using TaskManager.Core.Interfaces.Repositories;
using TaskManager.Core.Interfaces.Services;

namespace TaskManager.Infrastructure.Services
{
    public class TaskAssignmentService : ITaskAssignmentService
    {
        private readonly ITaskAssignmentRepository _assignmentRepository;
        private readonly ITaskItemRepository _taskRepository;

        public TaskAssignmentService(
            ITaskAssignmentRepository assignmentRepository,
            ITaskItemRepository taskRepository)
        {
            _assignmentRepository = assignmentRepository;
            _taskRepository = taskRepository;
        }

        public async Task<IEnumerable<TaskAssignment>> GetAssignmentsByTaskIdAsync(int taskId)
        {
            return await _assignmentRepository.GetByTaskIdAsync(taskId);
        }

        public async Task<IEnumerable<TaskAssignment>> GetAssignmentsByUserIdAsync(string userId)
        {
            return await _assignmentRepository.GetByUserIdAsync(userId);
        }

        public async Task<TaskAssignment> GetAssignmentByIdAsync(int id)
        {
            return await _assignmentRepository.GetByIdAsync(id);
        }

        public async Task<TaskAssignment> AssignTaskToUserAsync(int taskId, string userId)
        {
            // Check if task exists
            var task = await _taskRepository.GetByIdAsync(taskId);
            if (task == null)
                throw new ArgumentException($"Task with ID {taskId} not found");

            // Check if task is already assigned to user
            var isAssigned = await _assignmentRepository.IsTaskAssignedToUserAsync(taskId, userId);
            if (isAssigned)
                throw new InvalidOperationException($"Task with ID {taskId} is already assigned to user with ID {userId}");

            // Create new assignment
            var assignment = new TaskAssignment
            {
                TaskId = taskId,
                UserId = userId,
                AssignedAt = DateTime.UtcNow
            };

            return await _assignmentRepository.AddAsync(assignment);
        }

        public async Task<bool> UnassignTaskFromUserAsync(int taskId, string userId)
        {
            var assignments = await _assignmentRepository.GetByTaskIdAsync(taskId);
            var assignment = assignments.FirstOrDefault(a => a.UserId == userId);

            if (assignment == null)
                return false;

            await _assignmentRepository.DeleteAsync(assignment.Id);
            return true;
        }

        public async Task<bool> IsTaskAssignedToUserAsync(int taskId, string userId)
        {
            return await _assignmentRepository.IsTaskAssignedToUserAsync(taskId, userId);
        }
    }
}