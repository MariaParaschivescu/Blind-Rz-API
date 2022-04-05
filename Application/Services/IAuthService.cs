using Application.DTOs;
using Domain.Entities;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IAuthService
    {
        public JwtDTO GenerateToken(User user);
    }
}
