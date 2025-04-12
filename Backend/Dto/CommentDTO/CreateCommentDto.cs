using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConspectFiles.Dto.CommentDTO
{
    public class CreateCommentDto
    {
        [Required]
        [MinLength(1, ErrorMessage = "Author name must be 1 characters")]
        [MaxLength(50, ErrorMessage ="Author name can not be over 50 characters")]
        public string AuthorName { get; set; } = string.Empty;
        [Required]
        [MinLength(1, ErrorMessage = "Content must be 1 characters")]
        [MaxLength(300, ErrorMessage ="Content can not be over 300 characters")]
        public string Content { get; set; } = string.Empty;

    }
}