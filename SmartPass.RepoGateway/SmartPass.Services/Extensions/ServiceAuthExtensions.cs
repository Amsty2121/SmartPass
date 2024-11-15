using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SmartPass.Repository.Models.Entities;
using SmartPass.Services.AuthConfig;

namespace SmartPass.Services.Extensions
{
    public static class ServiceAuthExtensions
    {
        public static void AddJwtAuthentication(this IServiceCollection services, AuthOptions authOptions)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = authOptions.Issuer,

                    ValidateAudience = true,
                    ValidAudience = authOptions.Audience,

                    ValidateLifetime = true,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = authOptions.GetSymmetricSecurityKey(),

                    RoleClaimType = "Roles"
                };
            });
        }

        public static void AddJwtAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder()
                                        .RequireAuthenticatedUser()
                                        .RequireClaim(nameof(User.Id))
                                        .Build();

                //options.AddPolicy(Policies.SupervizerPolicy, pb => pb.RequireAuthenticatedUser().RequireClaim("iss", Policies.SupervizerPolicy).Build());
                
                //options.AddPolicy(Policies.AdminService, pb => pb.RequireAuthenticatedUser().RequireClaim("iss", Policies.AdminService).Build());
            });
        }

        public static AuthOptions ConfigureAuthOptions(this IServiceCollection services, IConfiguration configuration)
        {
            var authOptionsConfigurationSection = configuration.GetSection("AuthOptions");
            services.Configure<AuthOptions>(authOptionsConfigurationSection);
            var authOptions = authOptionsConfigurationSection.Get<AuthOptions>();

            return authOptions;
        }

        /*public static SyncServiceSettings ConfigureSyncServiceOptions(this IServiceCollection services, IConfiguration configuration)
        {
            var syncServiceConfigurationSection = configuration.GetSection("SyncServiceRoute");
            services.Configure<SyncServiceSettings>(syncServiceConfigurationSection);
            var syncServiceSettings = syncServiceConfigurationSection.Get<SyncServiceSettings>();

            return syncServiceSettings;
        }*/
    }
}