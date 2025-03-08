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

namespace ConspectFiles.Controller
{
    [Route("conspect/conspects")]
    [ApiController]
    public class ConspectsController : ControllerBase
    {
        private readonly IConspectRepository _conspectRepo;

        public ConspectsController(IConspectRepository conspectRepo)
        {
            _conspectRepo = conspectRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var conspect = await _conspectRepo.GetAll();
            var conspectDto = conspect.Select(c=>c.ToConspectDto()); 
            return Ok(conspectDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var conspect = await _conspectRepo.GetById(id);
            if(conspect == null)
            {
                return NotFound();
            }
            
            return Ok(conspect.ToConspectDto());
        }


        
    }
}