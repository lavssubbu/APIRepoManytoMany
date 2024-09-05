using APIRepoPattern.Interface;
using APIRepoPattern.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace APIRepoPattern.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly StudCourseDbContext _con;
      
        private readonly ITokenGenerate _tokenService;

        public TokenController(StudCourseDbContext con, ITokenGenerate tokenService)
        {
            _con = con;
           _tokenService = tokenService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(User userData)
        {
            if (userData != null && !string.IsNullOrEmpty(userData.Email) && !string.IsNullOrEmpty(userData.Password))
            {
                var user = await GetUser(userData.Email, userData.Password);

                if (user != null)
                {
                    var token = _tokenService.GenerateToken(user);
                    return Ok(new { token });
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest("Invalid request data");
            }
           
        }

        private async Task<User> GetUser(string email, string password)
        {
            return await _con.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password)?? new Models.User();
        }
    }
}
