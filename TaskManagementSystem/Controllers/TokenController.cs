using BusinessLogic.Authorization;
using BusinessLogic.Services;
using BusinessLogic.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TaskManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : Controller
    {
        private readonly UserService userService;
        private readonly ITokenService tokenService;

        public TokenController(UserService userService, ITokenService tokenService)
        {
            this.userService = userService;
            this.tokenService = tokenService;
        }

        [HttpPost]
        [Route("refresh")]
        public async Task<IActionResult> Refresh(Token token)
        {
            if (token is null)
            {
                return BadRequest("Invalid client request");
            }

            string accessToken = token.AccessToken;
            string refreshToken = token.RefreshToken;

            ClaimsPrincipal principal = tokenService.GetPrincipalFromExpiredToken(accessToken);
            string userName = principal.Identity.Name;

            User user = (await userService.GetAllUsersAsync()).
                FirstOrDefault(u => u.Login == userName);

            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                return BadRequest("Invalid client request");
            }

            string newAccessToken = tokenService.GenerateAccessToken(principal.Claims);
            string newRefreshToken = tokenService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            await userService.Update(user);

            return new ObjectResult(new
            {
                accessToken = newAccessToken,
                refreshToken = newRefreshToken
            });
        }
    }
}