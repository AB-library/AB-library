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
    [Route("api/identification")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        private readonly ITokenService _tokenService;

        public UserController(IUserRepository userRepo, ITokenService tokenService)
        {
            _userRepo = userRepo;
            _tokenService = tokenService;
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


                var refreshToken = _tokenService.GenerateRefreshToken();

                var userModel = await _userRepo.CreateAsync(newUser, refreshToken);
                if (userModel == null)
                {
                    return BadRequest("User already exists.");
                }
                
                var token = _tokenService.GenerateJwtToken(userModel);
                

                return Ok(
                    new NewUserDto
                    {
                        Username = newUser.UserName,
                        Token = token,
                        RefreshToken = refreshToken
                    }
                );
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

                return Ok(
                    new NewUserDto
                    {
                        Username = userModel.UserName,
                        Token = _tokenService.GenerateJwtToken(userModel),
                        RefreshToken = userModel.RefreshToken
                        
                    }
                );
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


        

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenDto refreshToken)
        {
            var userModel = await _userRepo.GetUserByRefreshToken(refreshToken);
            if(userModel == null)
            {
                return BadRequest("Invalid refresh token.");
            }


            var newAccessToken = _tokenService.GenerateJwtToken(userModel);
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            await _userRepo.SaveRefreshTokenAsync(userModel.Id, newRefreshToken);
            
            return Ok(new{
                Token = newAccessToken,
                RefreshToken = newRefreshToken
            });

        }




    


    }
}