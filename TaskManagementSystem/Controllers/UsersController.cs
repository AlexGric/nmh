using BusinessLogic.Responses;
using BusinessLogic.Services;
using BusinessLogic.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly UserService userService;
        private readonly TaskService taskService;

        public UsersController(UserService userService, TaskService taskService)
        {
            this.userService = userService;
            this.taskService = taskService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Get()
        {
            IEnumerable<SecureUser> users = await userService.GetAllSecureUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Get(int id)
        {
            SecureUser user = await userService.GetSecureUserById(id);
            return Ok(user);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(User user)
        {
            if (user == null)
            {
                return BadRequest("Empty user data");
            }

            if (user.Password.Length < 6 || user.Password.Length > 30)
            {
                return BadRequest("Password length must be between 6 and 30 characters");
            }

            Response<SecureUser> response = await userService.Create(user);

            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            return Ok(response.Data);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Update(User user)
        {
            if (user == null)
            {
                return BadRequest("Request body does not contain user data");
            }
            
            User existingUser = await userService.GetUserById(user.Id);
            IEnumerable<SecureUser> users = await userService.GetAllSecureUsersAsync();

            if (!users.Any(u => u.Id == user.Id))
            {
                return NotFound("User not found");
            }            

            if (user.Login != existingUser.Login)
            {
                if (users.Any(u => u.Login == user.Login))
                {
                    return BadRequest("User with this login already exists");
                }
            }

            if (user.Email != existingUser.Email)
            {
                if (users.Any(u => u.Email == user.Email))
                {
                    return BadRequest("User with this email already exists");
                }
            }

            if (string.IsNullOrWhiteSpace(user.Password))
            {
                user.Password = existingUser.Password;
            }
            else
            {
                if (user.Password.Length < 6 || user.Password.Length > 30)
                {
                    return BadRequest("Password length must be between 6 and 30 characters");
                }

                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            }

            user.RefreshToken = null;
            await userService.Update(user);
            return Ok(user);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int id)
        {
            SecureUser user = await userService.GetSecureUserById(id);

            if (user == null)
            {
                return NotFound("User not found");
            }

            // Uncomment this when task service will be implemented

            //if((await taskService.GetAllAsync()).AsEnumerable().Any(t => t.Manager.Id == id || t.Executor.Id == id))
            //{
            //    return BadRequest("You can't delete users with active or completed tasks. Try to deactivate account instead.");
            //}

            await userService.Delete(id);
            return Ok(user);
        }
    }
}