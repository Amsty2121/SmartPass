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
            appService.AddScoped<IUserService, UserService>();
            appService.AddScoped(typeof(IAccessLevelService), typeof(AccessLevelService));
            appService.AddScoped(typeof(IAccessCardService), typeof(AccessCardService));
            appService.AddScoped(typeof(ISessionService), typeof(SessionService));
            appService.AddScoped(typeof(IZoneService), typeof(ZoneService));
            appService.AddScoped(typeof(ICardReaderService), typeof(CardReaderService));

            return appService;
        }
    }
}
