using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace DataAccess.Context
{
    public class TaskManagerContext : DbContext
    {
        public TaskManagerContext(DbContextOptions<TaskManagerContext> options) : base(options)
        {

            //Database.EnsureDeleted(); // on program strart delete database
            Database.EnsureCreated(); // on program strart create database
        }

        public DbSet<Task> Tasks { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureUserTable(modelBuilder);
            ConfigureTaskTable(modelBuilder);
        }

        private void ConfigureUserTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().
                Property(u => u.Role).
                HasConversion(
                    r => r.ToString(),
                    r => (UserRole)Enum.Parse(typeof(UserRole), r)
                );

            modelBuilder.Entity<User>().
                Property(u => u.AccountStatus).
                HasConversion(
                    s => s.ToString(),
                    s => (AccountStatus)Enum.Parse(typeof(AccountStatus), s)
                );

            modelBuilder.Entity<User>().HasData(
                new User()
                {
                    Id = 1,
                    Name = "Admin",
                    Login = "admin",
                    Password = "$2a$11$xacc9mtsRv79fc0aLfjSlew9Jqiuj4WkZmKD9UCsR6Go17B7DDOiK", // admin_password
                    Email = "admin@mail.com",
                    Role = UserRole.Admin,
                    AccountStatus = AccountStatus.Active
                },
                new User()
                {
                    Id = 2,
                    Name = "Manager",
                    Login = "manager",
                    Password = "$2a$11$5SaUn9KjuE3UBzSxvtGCwuk6xfEwfL4q3hLi/yjzW8gOTnBUCwU3i", // manager_password
                    Email = "manager@mail.com",
                    Role = UserRole.Manager,
                    AccountStatus = AccountStatus.Active
                },
                new User()
                {
                    Id = 3,
                    Name = "User",
                    Login = "user",
                    Password = "$2a$11$W0dNR4IyshUJnnJl/vgpl.cPpgT/iA0Ca1C2GIRTwFXaQCxXVtrMa", // user_password
                    Email = "user@mail.com",
                    Role = UserRole.Executor,
                    AccountStatus = AccountStatus.Active
                }
            );
            modelBuilder.Entity<Task>().HasData(
                new Task
                {
                    Id = 1,
                    Name = "Task1",
                    Status = Status.NEW,
                    Description = "Test Task",
                    ManagerId = 2,
                    ExecutorId = 3
                },
                 new Task
                 {
                     Id = 2,
                     Name = "Task1",
                     Status = Status.IN_PROGRESS,
                     Description = "Test Task",
                     ManagerId = 2,
                     ExecutorId = 3
                 },
                  new Task
                  {
                      Id = 3,
                      Name = "Task1",
                      Status = Status.ON_HOLD,
                      Description = "Test Task",
                      ManagerId = 2,
                      ExecutorId = 3
                  },
                   new Task
                   {
                       Id = 4,
                       Name = "Task1",
                       Status = Status.DONE,
                       Description = "Test Task",
                       ManagerId = 2,
                       ExecutorId = 3
                   });
        }

        private void ConfigureTaskTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Task>().
                Property(t => t.Status).
                HasConversion(
                    s => s.ToString(),
                    s => (Status)Enum.Parse(typeof(Status), s)
                );

            modelBuilder.Entity<Task>().
                HasOne(t => t.Manager).
                WithMany().
                HasForeignKey(t => t.ManagerId).
                OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Task>().
                HasOne(t => t.Executor).
                WithMany().
                HasForeignKey(t => t.ExecutorId).
                OnDelete(DeleteBehavior.Restrict);
        }
    }
}