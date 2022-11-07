using Microsoft.AspNetCore.Mvc;
using System.Text;
using TwoeterApi.DTO;
using TwoeterApi.DTO.Security;
using TwoeterApi.Exceptions;
using TwoeterApi.Model.Entity;
using TwoeterApi.Model.Repository.Interfaces;

namespace TwoeterApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public SecurityController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        
        [HttpPost("/login")]
        public ActionResult Login(LoginRequest loginRequest)
        {
            User? user;
            try
            {
                user = _userRepository.Login(loginRequest.UserName, loginRequest.Password);
            }
            catch (UserNotFoundException e)
            {
                return StatusCode(403, new ErrorResponse() { Code = 403, Message = "Invalid data" });
            }
            
            var payload = new Dictionary<string, object>()
            {
                { "user", user }
            };

            var token = Jose.JWT.Encode(payload, Encoding.UTF8.GetBytes("mYq3t6v9y$B&E)H@McQfTjWnZr4u7x!z"), Jose.JwsAlgorithm.HS256);

            user.LastLogin = DateTime.Now;

            _userRepository.Update(user);

            return StatusCode(201, new Response<LoginResponse>()
            {
                Code = 200,
                Message = "Login success",
                Data = new LoginResponse()
                {
                    Token = token,
                }
            });
        }

        [HttpPost("/register")]
        public ActionResult Create(RegisterRequest registerRequest)
        {
            if (_userRepository.CheckUsername(registerRequest.UserName) != null || _userRepository.CheckEmail(registerRequest.Email) != null)
            {
                return StatusCode(422, new ErrorResponse { Code = 422, Message = "Account already exists" });
            }

            var user = new User()
            {
                Username = registerRequest.UserName,
                Password = BCrypt.Net.BCrypt.HashPassword(registerRequest.Password),
                Email = registerRequest.Email,
            };

            _userRepository.Create(user);

            var response = new Response<string>() { 
                Code = 201,
                Message = "User has been created"
            };

            return StatusCode(201, response);
        }
    }
}
