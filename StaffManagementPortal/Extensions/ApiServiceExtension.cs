using StaffManagement.Portal.Services.Staff;

namespace StaffManagement.Portal.Extensions
{

    public static class ApiServiceExtension
    {
        public static IServiceCollection AddApiClientService(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped(_ => new HttpClient { BaseAddress = new Uri("https://localhost:7168/api/") });
            services.AddTransient<IStaffService, StaffService>();
            return services;
        }
    }
}
