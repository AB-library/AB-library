using Microsoft.AspNetCore.Mvc;
using ConspectFiles.Interface;
using ConspectFiles.Dto.AppUSer;
using ConspectFiles.Mapper.UserMappers;
using Microsoft.AspNetCore.Authorization;

namespace ConspectFiles.Controller
{
    [Route("api/identification")]
    [ApiController]
    [Authorize]
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
        [AllowAnonymous]
        public async Task<IActionResult> Registration([FromBody] UserRegisterDto newUser)
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
                        Email = newUser.Email,
                        RefreshToken = refreshToken,
                        Role = newUser.Role
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
        // [Authorize(Roles = "Admin")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userRepo.GetAllAsync();
            var usersModels = users.Select(u => u.ToUserDto());
            return Ok(usersModels);
        }


        //Видалення
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
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

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] USerUpdateDto updateDto, [FromRoute] string id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(updateDto);
            }
            var userModel = await _userRepo.UpdateAsync(updateDto, id);
            if(userModel == null)
            {
                return NotFound("Invalid user id");
            }
            return Ok(userModel.ToUserDto());
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
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] UserLoginDto loginDto)
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
                        Email = userModel.Email,
                        Token = _tokenService.GenerateJwtToken(userModel),
                        RefreshToken = userModel.RefreshToken,
                        Role = userModel.Role
                        
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
        [Authorize]
        public IActionResult Logout() //Не знаю як зробити вихід
        {
            return Ok(new { Message = "Loggout successful" });
        }




        [HttpPost("refresh-token")]
        [AllowAnonymous]
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
                userModel.UserName,
                userModel.Email,
                Token = newAccessToken,
                RefreshToken = newRefreshToken,
                userModel.Role
            });

        }
    }
}