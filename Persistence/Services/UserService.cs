using Application.Common.Helpers;
using Microsoft.EntityFrameworkCore;
using Application.DTOs;
using Application.DTOs.User;
using Application.Filters;
using Application.Interfaces;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using BC = BCrypt.Net.BCrypt;
using Application.UnitOfWork;
using Microsoft.AspNet.Identity;
using System.Security.Cryptography;

namespace Persistence.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IEmailService emailServer)
        {
            _unitOfWork = unitOfWork;
            _userRepository = unitOfWork.UserRepository;
            _mapper = mapper;
            _emailService = emailServer;
        }
        public async Task<User> Authenticate(string email, string password)
        {
            var user = await _userRepository.GetAll().FirstOrDefaultAsync(user => user.Email.ToLower() == email.ToLower());
            if (user == null || !BC.Verify(password, user.Password))
            {
                return null;
            }
            return user;
        }

        public async Task DeleteUserAsync(Guid id)
        {
            await _userRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<PaginatedList<GetUserDTO>?> GetAllUsersAsync(GetUsersFilter filter)
        {
            IQueryable<User> users;
            filter ??= new GetUsersFilter();
            if (string.IsNullOrEmpty(filter.Email))
            {
                return null;
            }
            users = _userRepository.GetAll().Where(user => EF.Functions.Like(user.Email, $"%{filter.Email}"));
            return await _mapper.ProjectTo<GetUserDTO>(users).ToPaginatedListAsync(filter.CurrentPage, filter.PageSize);
        }

        public async Task<GetUserDTO> GetUserByIDAsync(Guid id)
        {
            return _mapper.Map<GetUserDTO>(await _userRepository.GetById(id));
        }

        public async Task<GetUserDTO> RegisterUser(RegisterUserDTO dto)
        {
            if (_userRepository.GetAll().Any(user => user.Email.ToLower() == dto.Email.ToLower()))
            {
                //sendAlreadyRegisteredEmail(dto.Email, origin);
                return null;
            }
            var created = _userRepository.Create(_mapper.Map<User>(dto));
            created.Password = BC.HashPassword(dto.Password);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<GetUserDTO>(created);
            //sendVerificationEmail(User userCreated);
        }

        public void ForgotPassword(ForgotPasswordDTO dto)
        {
            var account = _userRepository.GetAll().FirstOrDefault(user => user.Email == dto.Email);
            // always return ok response to prevent email enumeration
            if (account == null) return;
            account.ResetToken = randomTokenString();
            account.ResetTokenExpires = DateTime.UtcNow.AddDays(1);
            _userRepository.Update(account);
            _unitOfWork.SaveChangesAsync();

            // send email
            sendPasswordResetEmail(account);

        }

        public void ValidateResetToken(string token)
        {
            var account = _userRepository.GetAll().FirstOrDefault(x =>
                x.ResetToken == token &&
                x.ResetTokenExpires > DateTime.UtcNow);
            if (account == null)
                throw new AppException("Invalid token");
        }

        public async Task<GetUserDTO> ResetPasswordAsync(ResetPasswordDTO dto)
        {
            //var originalUser = await _userRepository.GetById(id);
            var originalUser = getAccountByResetToken(dto.Token);
            if (originalUser == null) return null;

            originalUser.Password = BC.HashPassword(dto.Password);
            originalUser.PasswordReset = DateTime.UtcNow;
            originalUser.ResetToken = "";
            originalUser.ResetTokenExpires = null;

            _userRepository.Update(originalUser);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<GetUserDTO>(originalUser);
        }

        private User getAccountByResetToken(string token)
        {
            var account = _userRepository.GetAll().FirstOrDefault(x =>
                x.ResetToken == token && x.ResetTokenExpires > DateTime.UtcNow);
            if (account == null) throw new AppException("Invalid token");
            return account;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _userRepository.Dispose();
            }
        }

        private string randomTokenString()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[40];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            // convert random bytes to hex string
            return BitConverter.ToString(randomBytes).Replace("-", "");
        }

        private void sendAlreadyRegisteredEmail(string email, string origin)
        {
            string message;
            if (!string.IsNullOrEmpty(origin))
                message = $@"<p>If you don't know your password please visit the <a href=""{origin}/account/forgot-password"">forgot password</a> page.</p>";
            else
                message = "<p>If you don't know your password you can reset it via the <code>/accounts/forgot-password</code> api route.</p>";

            _emailService.Send(
                to: email,
                subject: "Sign-up Verification API - Email Already Registered",
                html: $@"<h4>Email Already Registered</h4>
                         <p>Your email <strong>{email}</strong> is already registered.</p>
                         {message}"
            );
        }


        private void sendVerificationEmail(User account, string origin)
        {
            string message;
            if (!string.IsNullOrEmpty(origin))
            {
                var verifyUrl = $"{origin}/account/verify-email?token={account.VerificationToken}";
                message = $@"<p>Please click the below link to verify your email address:</p>
                             <p><a href=""{verifyUrl}"">{verifyUrl}</a></p>";
            }
            else
            {
                message = $@"<p>Please use the below token to verify your email address with the <code>/accounts/verify-email</code> api route:</p>
                             <p><code>{account.VerificationToken}</code></p>";
            }

            _emailService.Send(
                to: account.Email,
                subject: "Sign-up Verification API - Verify Email",
                html: $@"<h4>Verify Email</h4>
                         <p>Thanks for registering!</p>
                         {message}"
            );
        }

        private void sendPasswordResetEmail(User account)
        {
            string message;
            //if (!string.IsNullOrEmpty(origin))
            //{
            var resetUrl = $"http://localhost:8100/account/reset-password?token={account.ResetToken}";
            message = $@"<p>Please click the below link to reset your password, the link will be valid for 1 day:</p>
                        <p><a href=""{resetUrl}"">{resetUrl}</a></p>";
            //}

            //Use this with postman only
            //else
            //{
            //    message = $@"<p>Please use the below token to reset your password with the <code>/accounts/reset-password</code> api route:</p>
            //                <p><code>{account.ResetToken}</code></p>";
            //}

            _emailService.Send(
                to: account.Email,
                subject: "Sign-up Verification API - Reset Password",
                html: $@"<h4>Reset Password Email</h4>
                        {message}"
            );
        }
    }
}
