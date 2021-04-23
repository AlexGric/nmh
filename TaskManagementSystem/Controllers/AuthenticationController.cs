using BusinessLogic.Responses;
using BusinessLogic.Services;
using BusinessLogic.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TaskManagementSystem.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly IConfiguration config;
        private readonly UserService userService;
        private readonly ITokenService tokenService;
        private readonly ILogger<AuthenticationController> logger;

        public AuthenticationController(UserService userService, ITokenService tokenService, 
            IConfiguration config, ILogger<AuthenticationController> logger)
        {
            this.userService = userService;
            this.tokenService = tokenService;
            this.config = config;
            this.logger = logger;
        }

        [HttpPost, Route("login")]
        public async Task<IActionResult> Login(Login login)
        {
            if (login == null)
            {
                return BadRequest(new { errorText = "Invalid login or password." });
            }

            User user = (await userService.GetAllUsersAsync()).
                FirstOrDefault(u => u.Login == login.UserName && BCrypt.Net.BCrypt.Verify(login.Password, u.Password));

            if (user == null)
            {
                return Unauthorized();
            }

            if(user.AccountStatus == AccountStatus.Inactive)
            {
                return BadRequest("Your account is deactivated");
            }

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, login.UserName),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            string accessToken = tokenService.GenerateAccessToken(claims);
            string refreshToken = tokenService.GenerateRefreshToken();
            string UserLogin = user.Login;

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);

            await userService.Update(user);

            return Ok(new
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                UserLogin
            });
        }

        [HttpPost, Route("register")]
        public async Task<IActionResult> Register(User user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            user.Role = UserRole.Executor;
            user.AccountStatus = AccountStatus.Inactive;

            Response<SecureUser> response = await userService.Create(user);

            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            try
            {
                EmailSenderService emailSender = new EmailSenderService(config);
                emailSender.SendEmail(user.Email, "Registration successful", "Wait for account activation");                
            }
            catch
            {
                logger.LogInformation("Invalid email settings");
            }

            return Ok("Wait for account activation.");
        }
    }
}