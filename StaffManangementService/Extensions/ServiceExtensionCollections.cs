using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StaffManagement.Service.Dtos.AppSetting;
using StaffManagement.Service.Models;
using StaffManagement.Service.Repositories.Staff;
using System.Reflection;
using System.Threading.RateLimiting;

namespace StaffManagement.Service.Extensions
{
    public static class ServiceExtensionCollections
    {
        public static IServiceCollection AddServiceCollection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMappings();
            services.AddDbContext(configuration);
            services.AddRepositories();
            services.RateLimit(configuration);
            return services;
        }

        private static void RateLimit(this IServiceCollection services, IConfiguration configuration)
        {
            var rateLimitSettings = new RateLimitSettingsDto();
            configuration.GetSection("RateLimitSettings").Bind(rateLimitSettings);

            services.AddRateLimiter(options =>
            {
                options.AddFixedWindowLimiter("Fixed", opt =>
                {
                    opt.Window = TimeSpan.FromSeconds(rateLimitSettings.FixedWindowLimiter.WindowSeconds);
                    opt.PermitLimit = rateLimitSettings.FixedWindowLimiter.PermitLimit;
                    opt.QueueProcessingOrder = Enum.Parse<QueueProcessingOrder>(rateLimitSettings.FixedWindowLimiter.QueueProcessingOrder, true);
                    opt.QueueLimit = rateLimitSettings.FixedWindowLimiter.QueueLimit;
                });
            });
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
            services.AddTransient<IStaffRepository, StaffRepository>();
        }
    }
}
