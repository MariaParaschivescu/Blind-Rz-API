using Application.DTOs;
using Application.DTOs.User;
using Application.Services;
using Domain.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ISession = Domain.Auth.ISession;

namespace BlindrzAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController: BaseController
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly ISession _session;

        public UserController(IAuthService authService, IUserService userService, ISession session)
        {
            _authService = authService;
            _userService = userService; 
            _session = session;
        }

        [HttpPost]
        [Route("authenticate")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(JwtDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> Authenticate([FromBody] LogInUserDTO logInInfo)
        {
            var user = await _userService.Authenticate(logInInfo.Email, logInInfo.Password);
            if(user == null)
            {
                return BadRequest(new { message = "Password or email is incorrect!" });
            }
            //setTokenCookie(user.RefreshToken);
            return Ok(_authService.GenerateToken(user));
        }

        [HttpPost]
        [Route("registerUser")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<ActionResult<GetUserDTO>> RegisterUser(RegisterUserDTO dto)
        {
            var newAccount = await _userService.RegisterUser(dto);
            return CreatedAtAction(nameof(RegisterUser), new { id = newAccount.Id }, newAccount);
        }

        [AllowAnonymous]
        [HttpPost("forgot-password")]
        public IActionResult ForgotPassword(ForgotPasswordDTO dto)
        {
            _userService.ForgotPassword(dto);
            return Ok(new { message = "Please check your email for password reset instructions" });
        }

        [HttpPost("validate-reset-token")]
        [AllowAnonymous]
        public IActionResult ValidateResetToken(ValidationResetDTO forgotPassword)
        {
            _userService.ValidateResetToken(forgotPassword.Token);
            return Ok(new { message = "Token is valid" });
        }

        [AllowAnonymous]
        [HttpPatch("reset-password")]
        public async Task<ActionResult> UpdatePassword([FromBody] ResetPasswordDTO dto)
        {
            //await _userService.ResetPasswordAsync(_session.UserId, dto);
            await _userService.ResetPasswordAsync(dto);
            return Ok(new { message = "Password reset successful, you can now login" });
        }

        [Authorize]
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            if(id == null)
            {
                return NoContent();
            }
            await _userService.DeleteUserAsync(id);
            return NotFound();
        }

        private void setTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }
    }
    
}
