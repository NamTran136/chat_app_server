using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Server_proj1.Datalayer;
using Server_proj1.Models.DTOs.UserDtos;
using Server_proj1.Models.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Server_proj1.Controllers
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DataContext _db;
        private readonly IConfiguration _configuration;
        public AuthController(DataContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }
        [HttpPost("register")]
        public ActionResult Register(UserDto request)
        {
            if(String.IsNullOrEmpty(request.Email))
            {
                return BadRequest("Email is required");
            }
            if (String.IsNullOrEmpty(request.Name))
            {
                return BadRequest("Name is required");
            }
            if (String.IsNullOrEmpty(request.Password))
            {
                return BadRequest("Password is required");
            }
            try
            {
                var existingUser = _db.Users.FirstOrDefault(u => u.Email == request.Email);
                if (existingUser != null) {
                    return BadRequest("Email is already in use");
                }
                string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
                var newUser = new User(request.Email, request.Name, passwordHash);
                newUser.ImageUrl = !String.IsNullOrEmpty(request.ImageUrl) ? request.ImageUrl : newUser.ImageUrl;
                _db.Users.Add(newUser);
                _db.SaveChanges();
                string token = CreateToken(newUser);
                var response = new
                {
                    token,
                    message = "Sign up successfully",
                    user = new
                    {
                        email = newUser.Email,
                        id = newUser.Id,
                        name = newUser.Name,
                        imageUrl = newUser.ImageUrl
                    }
                };
                return Ok(response);
            }
            catch (Exception ex) {
                return BadRequest($"Error: {ex.Message}");
            }
        }
        [HttpPost("login")]
        public ActionResult Login(UserToLoginDto request)
        {
            if (String.IsNullOrEmpty(request.Email))
            {
                return BadRequest("Email is required");
            }
            if (String.IsNullOrEmpty(request.Password))
            {
                return BadRequest("Password is required");
            }
            try
            {
                var existingUser = _db.Users.FirstOrDefault(u => u.Email == request.Email);
                if (existingUser == null)
                {
                    return BadRequest("Couldn't find the user");
                }
                if (!BCrypt.Net.BCrypt.Verify(request.Password, existingUser.Password)) 
                {
                    return BadRequest("Wrong password");
                }
                string token = CreateToken(existingUser);
                var response = new
                {
                    token,
                    message = "Logged in successfully",
                    user = new UserToGetDto
                    {
                        Email = existingUser.Email,
                        Id = existingUser.Id,
                        Name = existingUser.Name,
                        ImageUrl = existingUser.ImageUrl
                    }
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("email", user.Email),
                new Claim("id", user.Id),
                new Claim("name", user.Name),
                new Claim("imageUrl", user.ImageUrl),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}

