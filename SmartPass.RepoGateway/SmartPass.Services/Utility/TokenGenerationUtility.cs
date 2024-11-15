using Microsoft.IdentityModel.Tokens;
using SmartPass.Repository.Models.Entities;
using SmartPass.Services.AuthConfig;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace SmartPass.Services.Utility
{
    public static class TokenGenerationUtility
    {
        public static string GenerateJwtToken(AuthOptions authOptions, User user)
        {

            var claims = new List<Claim>()
            {
                new Claim(nameof(User.Id), user.Id.ToString()),
                new Claim(nameof(User.UserName), user.UserName),
                new Claim(nameof(User.Department), user.Department)
            };

            foreach (var role in user.UserRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
            }

            var signinCredentials = new SigningCredentials(authOptions.GetSymmetricSecurityKey(),
                                                           SecurityAlgorithms.HmacSha256);
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: authOptions.Issuer,
                audience: authOptions.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(authOptions.TokenLifetime),
                signingCredentials: signinCredentials
            );
            var tokenHandler = new JwtSecurityTokenHandler();

            return tokenHandler.WriteToken(jwtSecurityToken);
        }

        public static UserAuthData GenerateRefreshToken(UserAuthData? userData, Guid userId)
        {
            var userAuthData = userData ?? new UserAuthData { Id = Guid.NewGuid() };

            userAuthData.UserId = userId;
            userAuthData.RefreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
            userAuthData.ExpiresUtc = DateTime.UtcNow.AddDays(7);

            return userAuthData;
        }
    }
}
