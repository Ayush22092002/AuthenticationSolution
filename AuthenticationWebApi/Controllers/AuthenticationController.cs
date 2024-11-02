using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AuthenticationDll.Models;
using AuthenticationWebApi.DataServices;
using Microsoft.AspNetCore.Cors;
using System.Threading.Tasks;
using System;

namespace AuthenticationWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("cors")]
    public class AuthenticationController : ControllerBase
    {
        private readonly AuthenticationDataServices _authenticationDataServices;

        public AuthenticationController(AuthenticationDataServices authenticationDataServices)
        {
            _authenticationDataServices = authenticationDataServices;
        }

        // POST: api/authentication
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] User user)
        {
            try
            {
                int userId = await _authenticationDataServices.AddUserAsync(user);
                return Ok(new { Message = "User added successfully.", UserId = userId });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        // GET: api/authentication/{id}
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUser(int userId)
        {
            try
            {
                var user = await _authenticationDataServices.GetUserAsync(userId);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        // DELETE: api/authentication/{id}
        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            try
            {
                await _authenticationDataServices.DeleteUserAsync(userId);
                return Ok(new { Message = "User deleted successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        // GET: api/authentication
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _authenticationDataServices.GetAllUsersAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        // PUT: api/authentication
        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] User user)
        {
            try
            {
                await _authenticationDataServices.UpdateUserAsync(user);
                return Ok(new { Message = "User updated successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        // POST: api/authentication/validate
        [HttpPost("validate")]
        public async Task<IActionResult> ValidateUser([FromBody] User loginUser)
        {
            try
            {
                var user = await _authenticationDataServices.ValidateUserAsync(loginUser.Username, loginUser.Password);
                if (user == null)
                {
                    return Unauthorized(new { Message = "Invalid credentials." });
                }
                return Ok(new { Message = "Login successful.", UserId = user.UserId });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
