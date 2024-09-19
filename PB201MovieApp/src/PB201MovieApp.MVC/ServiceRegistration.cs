using PB201MovieApp.MVC.Services.Implementations;
using PB201MovieApp.MVC.Services.Interfaces;

namespace PB201MovieApp.MVC
{
    public static class ServiceRegistration
    {
        public static void AddRegisterService(this IServiceCollection services)
        {
            services.AddScoped<ICrudService, CrudService>();
            services.AddScoped<IAuthService, AuthService>();
        }
    }
}
