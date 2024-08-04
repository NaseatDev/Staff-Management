using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StaffManagement.Service.Models;
using StaffManagement.Service.Repositories.Staff;
using System.Reflection;

namespace StaffManagement.Service.Extensions
{
    public static class ServiceExtensionCollections
    {
        public static IServiceCollection AddServiceCollection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMappings();
            services.AddDbContext(configuration);
            services.AddRepositories();
            return services;
        }

        private static void AddMappings(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }

        private static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("StaffConnection");

            services.AddDbContext<StaffDbContext>(options => options.UseSqlServer(connectionString), ServiceLifetime.Transient);
        }

        private static void AddRepositories(this IServiceCollection services)
        {
            services
                .AddTransient<IStaffRepository, StaffRepository>();
        }
    }
}
