using capstone_backend.Models;
using capstone_backend.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace capstone_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        private IConfiguration _configuration;

        public UserController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.EmailId),
                new Claim(ClaimTypes.Role, user.Access)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpGet]
        public ActionResult<List<User>> GetAllUsers()
        {
            return _userService.GetAllUsers();
        }

        [HttpGet("{id}")]
        public ActionResult<User> GetUserById(int id)
        {
            return _userService.GetUserById(id);
        }

        [HttpPost]
        public void AddUser([FromBody] User user)
        {
            _userService.AddUser(user);
        }

        [HttpPut("{id}")]
        public void UpdateUser(int id, User user)
        {
            user.UserId = id;
            _userService.UpdateUser(user);
        }

        [HttpDelete("{id}")]
        public void DeleteUser(int id)
        {
            _userService.DeleteUser(id);
        }

        [HttpPost("/login")]
        public IActionResult LoginUser([FromBody] User users)
        {
            string res = _userService.LoginUser(users);
            if (res == "Logged In!")
            {
                var authToken = GenerateJwtToken(users);
                return Ok(new { message = res, email = users.EmailId, authToken = authToken});
            }
            else
            {
                return Ok(new { message = res });
            }
        }
    }
}
