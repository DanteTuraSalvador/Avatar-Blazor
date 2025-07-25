using System;
using System.Collections.Generic;

namespace TaskManager.Core.Entities
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int ProjectId { get; set; }
        public int StatusId { get; set; }
        public string CreatedById { get; set; }
        public DateTime DueDate { get; set; }
        public int Priority { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // Navigation properties
        public virtual Project Project { get; set; }
        public virtual TaskStatus Status { get; set; }
        public virtual ApplicationUser CreatedBy { get; set; }
        public virtual ICollection<TaskAssignment> Assignments { get; set; } = new List<TaskAssignment>();
    }
}