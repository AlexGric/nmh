using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public class Task
    {
        public int Id { get;  set; }
        public string Name{ get;  set; }
        public string Description { get;  set; }
        public Status Status { get;  set; }

        public DateTime Date { get;  set; }
        public string FilePath { get;  set; }

        public int ManagerId { get; set; }
        public User Manager { get; set; }

        public int ExecutorId { get; set; }
        public User Executor { get; set; }

        public IEnumerable<Comment> Comments { get;  set; }

        public Task()
        {
            Comments = new List<Comment>();
        }
    }

    public enum Status
    {
        NEW,
        IN_PROGRESS,
        ON_HOLD,
        DONE
    }
}