using Project_CSharp.DTOs.Request;
using Project_CSharp.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Project_CSharp.Services;

namespace Project_CSharp.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository repository;
        public UserController(IUserRepository repository)
        {
            this.repository = repository;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(AuthUser authUser)
        {
            var answer = await repository.CreateAsync(authUser);
            switch (answer)
            {
                case Answer.Ok:
                    return Ok("Successfully registered!");
                case Answer.UserIsAlreadyRegistered:
                    return BadRequest("User with this email is already registered!");
                default:
                    return StatusCode(500);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(AuthUser authUser)
        {
            var result = await repository.LoginAsync(authUser);
            switch (result.Answer)
            {
                case Answer.Ok:
                    return Ok(result);
                case Answer.BadPassword:
                    return Unauthorized("Entered password is wrong!");
                case Answer.UserNotFound:
                    return Unauthorized("Entered email is not found!");
                default:
                    return Unauthorized();
            }
        }

        [HttpPut("updateAccessToken/{refreshToken}")]
        public async Task<IActionResult> UpdateAccessToken([FromRoute] string refreshToken)
        {
            var result = await repository.UpdateAccessTokenAsync(refreshToken);

            switch (result.Answer)
            {
                case Answer.Ok:
                    return Ok(result);
                case Answer.RefreshTokenIsExpired:
                    return BadRequest("Refresh token is expired!");
                case Answer.UserNotFound:
                    return BadRequest("User is not found!");
                default:
                    return Unauthorized();
            }
        }
    }
}
