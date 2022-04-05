using Application.Common.Helpers;
using Application.DTOs;
using Application.DTOs.User;
using Application.Filters;
using Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IUserService
    {
        Task<User> Authenticate(string email, string password);
        Task<GetUserDTO> RegisterUser(RegisterUserDTO dto);
        Task DeleteUserAsync(Guid id);
        Task<GetUserDTO> ResetPasswordAsync(ResetPasswordDTO dto);
        Task<PaginatedList<GetUserDTO>?> GetAllUsersAsync(GetUsersFilter filter);

        void ValidateResetToken(string token);

        void ForgotPassword(ForgotPasswordDTO dto);

        Task<GetUserDTO> GetUserByIDAsync(Guid id);
    }
}
