using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace DataAccess.Context
{
    public class TaskManagementDbContextFactory : IDesignTimeDbContextFactory<TaskManagerContext>
    {
        public TaskManagerContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName)
                .AddJsonFile("TaskManagementSystem2021_1\\TaskManagementSystem\\appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<TaskManagerContext>();
            var connectionString = configuration.GetConnectionString("TaskManagerConnection");
            builder.UseSqlServer(connectionString);

            return new TaskManagerContext(builder.Options);
        }
    }
}