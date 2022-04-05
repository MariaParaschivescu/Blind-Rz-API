using Application.DTOs;
using Application.Services;
using Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Persistence.Services
{
    public class AuthService : IAuthService
    {
        private readonly TokenConfiguration _appSettings;

        public AuthService(IOptions<TokenConfiguration> appSettings)
        {
            _appSettings = appSettings.Value;

        }
        public JwtDTO GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var claims = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            });

            var expDate = DateTime.UtcNow.AddHours(1);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = expDate,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = _appSettings.Audience,
                Issuer = _appSettings.Issuer,
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new JwtDTO
            {
                Token = tokenHandler.WriteToken(token),
                ExpDate = expDate,
            };
        }
    }
}
