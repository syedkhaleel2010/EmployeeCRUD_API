using EmployeeCRUD_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EmployeeCRUD_API.Controllers
{
    [Route("api/token")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly DatabaseContext _context;
        public TokenController(IConfiguration config, DatabaseContext context)
        {
            _configuration = config;
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]UserInfo _userData)
        {
            if (_userData != null && _userData.UserId != null && _userData.Password != null)
            {
                var user = await GetUser(_userData.UserId, _userData.Password);

                if (user != null)
                {
                    //create claims details based on the user information
                    var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("UserId", user.UserId.ToString()),
                        new Claim("DisplayName", user.UserId),
                        new Claim("UserName", user.UserId)
                       
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(10),
                        signingCredentials: signIn);

                    return Ok(new { accessToken = new JwtSecurityTokenHandler().WriteToken(token),displayName =user.UserId }  );
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest();
            }
        }

        private async Task<UserInfo> GetUser(string userId, string password)
        {
            return await _context.UserInfo.FirstOrDefaultAsync(u => u.UserId == userId && u.Password == password);
        }
    }
}
