using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConspectFiles.Dto.CommentDTO;
using ConspectFiles.Interface;
using ConspectFiles.Mapper;
using Microsoft.AspNetCore.Mvc;

namespace ConspectFiles.Controller
{
    [Route("api/comments")]
    [ApiController]
    public class CommentController: ControllerBase
    {
        private readonly ICommentRepository _commentRepo;
        public CommentController(ICommentRepository commentRepo)
        {
            _commentRepo = commentRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var comments = await _commentRepo.GetAll();
            if(comments == null)
            {
                return NotFound();
            }
            return Ok(comments.Select(c=>c.ToCommentDto()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] string id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comments = await _commentRepo.GetById(id);
            if(comments == null)
            {
                return NotFound();
            }
            return Ok(comments.ToCommentDto());
        }
        [HttpPost("{conspectId}")]
        public async Task<IActionResult> Create([FromRoute] string conspectId, [FromBody] CreateCommentDto commentModel)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comment = commentModel.ToCommentFromCreateDto(conspectId);
            await _commentRepo.Create(comment);
            return CreatedAtAction(nameof(GetById), new { id = comment.Id }, comment.ToCommentDto());
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Update([FromRoute] string Id, [FromBody] UpdateCommentDto commentDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comment = await _commentRepo.Update(Id, commentDto);
            if(comment == null)
            {
                return NotFound();
            }
            return Ok(comment.ToCommentDto());
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var conspect = await _commentRepo.Delete(id);
            if(conspect == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}