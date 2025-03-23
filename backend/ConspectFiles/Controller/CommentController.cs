using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConspectFiles.Interface;
using ConspectFiles.Mapper;
using Microsoft.AspNetCore.Mvc;

namespace ConspectFiles.Controller
{
    [Route("conspectFiles/comments")]
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
            var comments = await _commentRepo.GetById(id);
            if(comments == null)
            {
                return NotFound();
            }
            return Ok(comments.ToCommentDto());
        }
    }
}