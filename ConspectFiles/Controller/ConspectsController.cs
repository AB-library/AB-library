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
using ConspectFiles.Helpers;

namespace ConspectFiles.Controller
{
    [Route("api/conspects")]
    [ApiController]
    public class ConspectsController : ControllerBase
    {
        private readonly IConspectRepository _conspectRepo;

        public ConspectsController(IConspectRepository conspectRepo)
        {
            _conspectRepo = conspectRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            var conspect = await _conspectRepo.GetAll(query);
            var conspectDto = conspect.Select(c=>c.ToConspectDto()); 
            return Ok(conspectDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] string id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var conspect = await _conspectRepo.GetById(id);
            if(conspect == null)
            {
                return NotFound();
            }
            
            return Ok(conspect.ToConspectDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateConspectDto conspectDto)
        {
            conspectDto.IsDraft = conspectDto.IsDraft;
            var conspect = conspectDto.ToConspectFromCreateDto();
            await _conspectRepo.Create(conspect);
             return CreatedAtAction(nameof(GetById), new { id = conspect.Id }, conspect.ToConspectDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] string id, [FromBody] UpdateConspectDto conspectDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var conspect = await _conspectRepo.Update(id, conspectDto);
            if(conspect == null)
            {
                return NotFound();
            }
            return Ok(conspect.ToConspectDto());
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var conspect = await _conspectRepo.Delete(id);
            if(conspect == null)
            {
                return NotFound();
            }
            return NoContent();
        }
        [HttpGet("drafts")]
        public async Task<IActionResult> GetDrafts()
        {
            var query = new QueryObject{ShowOnlyDrafts = true};
            var drafts = await _conspectRepo.GetAll(query);
            var dto = drafts.Select(d=>d.ToConspectDto());
            return Ok(dto);

        }

        
    }
}