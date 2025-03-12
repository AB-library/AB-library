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

        [HttpPost("register")]

        public async Task<IActionResult> Registration(RegisterDto newUser)
        {
            var UserModel = await _userRepo.CreateAsync(newUser);
            if(UserModel == null)
            {
                return NotFound();
            }
            
            return Ok(newUser);

        }
        
    }
}