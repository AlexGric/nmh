using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public enum AccountStatus
    {
        Active,
        Inactive
    }

    public enum UserRole
    {
        Admin,
        Manager,
        Executor
    }

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public UserRole Role { get; set; }
        public AccountStatus AccountStatus { get; set; }
        public virtual IEnumerable<Task> Tasks { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}