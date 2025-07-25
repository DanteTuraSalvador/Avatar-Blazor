using System;

namespace TaskManager.Core.Entities
{
    public class TaskAssignment
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public string UserId { get; set; }
        public DateTime AssignedAt { get; set; }
        
        // Navigation properties
        public virtual TaskItem Task { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}