using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using SmartPass.RepoGateway.Configurations;
using SmartPass.Repository.Models.Entities;

namespace SmartPass.RepoGateway.Extensions
{
    public static class ServiceAuthExtensions
    {
        public static void AddJwtAuth(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddJwtAuthentication(services.ConfigureAuthOptions(configuration));
            services.AddJwtAuthorization();
        }

        private static void AddJwtAuthentication(this IServiceCollection services, AuthOptions authOptions)
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

        private static void AddJwtAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(config =>
            {
                config.DefaultPolicy = new AuthorizationPolicyBuilder()
                                        .RequireAuthenticatedUser()
                                        .RequireClaim(nameof(User.Id))
                                        .Build();
            });
        }

        private static AuthOptions ConfigureAuthOptions(this IServiceCollection services, IConfiguration configuration)
        {
            var authOptionsConfigurationSection = configuration.GetSection("AuthOptions");
            services.Configure<AuthOptions>(authOptionsConfigurationSection);
            var authOptions = authOptionsConfigurationSection.Get<AuthOptions>();

            return authOptions;
        }
    }
}
