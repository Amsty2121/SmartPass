using Microsoft.Extensions.DependencyInjection;
using SmartPass.Services.Implementations;
using SmartPass.Services.Interfaces;

namespace Application.IoC
{
    public static class AddServices
    {
        public static IServiceCollection AddServicesExtension(this IServiceCollection appService)
        {
            appService.AddScoped(typeof(IUserRoleService), typeof(UserRoleService));

            return appService;
        }
    }
}
