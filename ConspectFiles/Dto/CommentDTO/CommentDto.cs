using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConspectFiles.Dto.CommentDTO
{
    public class CommentDto
    {
        public string Id { get; set; } = string.Empty;
        public string AuthorName { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public string ConspectId { get; set; } = string.Empty;
    }
}