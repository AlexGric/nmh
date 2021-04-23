using BusinessLogic.Services;
using DataAccess.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLogic.Infrastructure
{
    public static class BusinessConfiguration
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            //services
            services.AddTransient(typeof(TaskService));
            services.AddTransient(typeof(UserService));
            services.AddTransient(typeof(CommentService));
            services.AddTransient<ITokenService, TokenService>();

            DataAccessConfiguration.ConfigureServices(services, configuration);
        }
    }
}
