using DataAccess.Context;
using DataAccess.Repository;
using DataAccess.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.Infrastructure
{
    public static class DataAccessConfiguration
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient(typeof(ITaskRepository), typeof(TaskRepository));
            services.AddTransient(typeof(IUserRepository), typeof(UserRepository));
            services.AddTransient(typeof(ICommentRepository), typeof(CommentRepository));

            services.AddDbContext<TaskManagerContext>(option =>
                option.UseSqlServer(configuration.GetConnectionString("TaskManagerConnection")));
        }
    }
}