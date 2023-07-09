using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebApi.Services
{
    public class JwtHelperService
    {
        private readonly static string secretKey = Program.config.GetValue<string>("SecretKeys:SecretKeyJWTHelper");
        

        public static string GenerateToken(string login, int lifeDays)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, login),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var token = new JwtSecurityToken(
                issuer: Program.config.GetValue<string>("Domains:DomainAPI"),
                audience: Program.config.GetValue<string>("Domains:DomainAPI"),
                claims: claims,
                expires: DateTime.UtcNow.AddDays(lifeDays),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        
        public static ClaimsPrincipal GetPrincipal(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                ValidIssuer = Program.config.GetValue<string>("Domains:DomainAPI"),
                ValidAudience = Program.config.GetValue<string>("Domains:DomainAPI"),
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ClockSkew = TimeSpan.Zero
            };

            var principal = tokenHandler.ValidateToken(token, validationParameters, out _);

            return principal;
        }


        public static string DecodeToken(string token)
        {
            var principal = GetPrincipal(token);
            var loginClaim = principal.FindFirst(JwtRegisteredClaimNames.Sub);
            return loginClaim?.Value;
        }
    }
}
