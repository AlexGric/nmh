using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.ViewModels
{
    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }

        public int ManagerId { get; set; }
        public SecureUser Manager { get; set; }
        public int ExecutorId { get; set; }
        public SecureUser Executor { get; set; }

        public DateTime Date { get; set; }
        public string FilePath { get; set; }
        public string Comments { get; set; }

    }
    public enum Status
    {
        NEW,
        IN_PROGRESS,
        ON_HOLD,
        DONE
    }
}
