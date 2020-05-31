using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.DTO;
using ToDoApp.Models;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens;
using ToDoApp.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ToDoApp.Services;

namespace ToDoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private ConfigurationService _configuration;
        private UserManager<User> _userManager;
        private AppDBContext _context;

        public AuthController(UserManager<User> userManager, AppDBContext context, ConfigurationService configuration)
        {
            _configuration = configuration;
            _userManager = userManager;
            _context = context;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] User user)
        {
            user.UserName = user.Email.Remove(user.Email.IndexOf('@'));
            var result = await _userManager.CreateAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            var createdUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
            string token = CreateJwt(createdUser);
            return Ok(token);
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] LoginDTO user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var loggedInUser = await _userManager.FindByEmailAsync(user.Email);
            if (loggedInUser == null || loggedInUser.Password != user.Password)
            {
                return Unauthorized();

            }

            string token = CreateJwt(loggedInUser);

            return Ok(token);
        }

        private string CreateJwt(User created)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, created.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Token.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var encryptingCredentials = new EncryptingCredentials(key, JwtConstants.DirectKeyUseAlg, SecurityAlgorithms.Aes256CbcHmacSha512);

            var jwtSecurityToken = new JwtSecurityTokenHandler().CreateJwtSecurityToken(
                _configuration.Token.Issuer,
                _configuration.Token.Audience,
                new ClaimsIdentity(claims),
                null,
                expires: DateTime.UtcNow.AddDays(3),
                null,
                signingCredentials: creds,
                encryptingCredentials: encryptingCredentials
                );
            var encryptedJWT = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return encryptedJWT;
        }

    }
}
