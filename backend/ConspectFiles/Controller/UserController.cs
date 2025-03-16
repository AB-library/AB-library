using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConspectFiles.Data;
using ConspectFiles.Repository;
using ConspectFiles.Interface;
using System.Net.Http.Headers;
using MongoDB.Driver;
using ConspectFiles.Mapper;
using ConspectFiles.Dto;
using ConspectFiles.Model;
using ConspectFiles.Dto.AppUSer;
using Microsoft.VisualBasic;
using Newtonsoft.Json.Serialization;
using ConspectFiles.Mapper.UserMappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ConspectFiles.Controller
{
    [Route("conspectFiles/identification")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepo;

        public UserController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }


        //регестрація
        [HttpPost("register")]
        public async Task<IActionResult> Registration([FromBody] RegisterDto newUser)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var userModel = await _userRepo.CreateAsync(newUser);
                if (userModel == null)
                {
                    return BadRequest("User already exists.");
                }

                return CreatedAtAction(nameof(GetById), new { id = userModel.Id }, userModel.ToUserDto());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred during registration.", Error = ex.Message });
            }
        }


        //Cписок користувачів
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userRepo.GetAllAsync();
            var usersModels = users.Select(u => u.ToUserDto());
            return Ok(usersModels);
        }


        //Видалення
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            try
            {
                var userModel = await _userRepo.DeleteAsync(id);
                if (userModel == null)
                {
                    return BadRequest("Wrong ID data.");
                }

                return Ok(userModel.ToUserDto());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while deleting the user.", Error = ex.Message });
            }
        }


        //Користувач по ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] string id)
        {
            var userModel = await _userRepo.GetByIdAsync(id);
            if (userModel == null)
            {
                return BadRequest("Wrong user data");
            }

            return Ok(userModel.ToUserDto());
        }


        //Вхід
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var userModel = await _userRepo.AuthenticateAsync(loginDto);
                if (userModel == null)
                {
                    return Unauthorized("Invalid username or password.");
                }

                var token = GenerateJwToken(userModel);

                return Ok(new { Token = token, User = userModel.ToUserDto() });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred during login.", Error = ex.Message });
            }
        }


        //Вихід
        [HttpPost("logout")]
        // [Authorize]
        public IActionResult Logout() //Не знаю як зробити вихід
        {
            return Ok(new { Message = "Loggout successful" });
        }




        //Cтворення JWT токена
        private string GenerateJwToken(AppUser user)
        {
            try
            {
                var jwtSecret = Environment.GetEnvironmentVariable("JWT_SECRET");
                var jwtIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER");
                var jwtAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE");

                if (string.IsNullOrEmpty(jwtSecret) || string.IsNullOrEmpty(jwtIssuer) || string.IsNullOrEmpty(jwtAudience))
                {
                    throw new InvalidOperationException("JWT environment variables are not properly configured.");
                }

                var key = Encoding.ASCII.GetBytes(jwtSecret);
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.UserName)
                };

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddHours(3),
                    Issuer = jwtIssuer,
                    Audience = jwtAudience,
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);

                return tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error generating JWT token.", ex);
            }
        }




    }
}