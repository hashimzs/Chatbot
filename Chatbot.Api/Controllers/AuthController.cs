using Chatbot.Api.Entities;
using Chatbot.Api.Repositories;
using Chatbot.Application.Shared.ReqDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chatbot.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserRepository userRepository;
        private readonly TokenManager tokenManager;

        public AuthController(UserRepository userRepository, TokenManager tokenManager)
        {
            this.userRepository = userRepository;
            this.tokenManager = tokenManager;
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login([FromBody] LoginReqDto loginReqDto)
        {
            try
            {
                var user = await this.userRepository.GetUserByEmail(loginReqDto.Email);

                if (user == null || user.Password != loginReqDto.Password)
                    return BadRequest("Invalid email or password");

                var token = tokenManager.GenerateToken(user);

                HttpContext.Response.Headers.Add("token", token);

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("An error occured while trying to login");
            }
        }

        [HttpPost("Register")]
        public async Task<ActionResult> Register([FromBody] RegisterReqDto registerReqDto)
        {
            try
            {
                var ExistingUser = await this.userRepository.GetUserByEmail(registerReqDto.Email);

                if (ExistingUser != null)
                    return BadRequest("A user with the same email already exists");

                var NewUser = new User
                {
                    Name = registerReqDto.Name,
                    Email = registerReqDto.Email,
                    Password = registerReqDto.Password,
                };

                await this.userRepository.AddUser(NewUser);

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("An error occured while trying to register user");
            }
        }
    }
}
