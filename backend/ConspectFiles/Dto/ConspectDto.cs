using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConspectFiles.Dto.CommentDTO;

namespace ConspectFiles.Dto
{
    public class ConspectDto
    {
        public string? Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string Tag { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public List<CommentDto> Comments{get; set;} = new();
    }
}