using Authentication.Api.Domain.Interfaces;
using Authentication.Api.Domain.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Authentication.Api.Infrastructure.Services
{
    public class TokenService(IOptions<AuthOptions> _authConfig) : ITokenService
    {
        public string GetToken(string userName, string password)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, userName),
                new Claim("role", "client")
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_authConfig.Value.Key));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _authConfig.Value.Issuer,
                audience: _authConfig.Value.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
            
        }
    }
}
