using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ConspectFiles.Dto
{
    public class UpdateConspectDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "Title must be 5 characters")]
        [MaxLength(280, ErrorMessage ="Title can not be over 280 characters")]
        public string Title { get; set; } = string.Empty;
        [Required]
        [MinLength(5, ErrorMessage = "Content must be 5 characters")]
        [MaxLength(3000, ErrorMessage ="Content can not be over 3000 characters")]
        public string Content { get; set; } = string.Empty; 
        [Required]
        [MinLength(2, ErrorMessage = "Tag must be 2 characters")]
        [MaxLength(20, ErrorMessage ="Tag can not be over 20 characters")]
        public string Tag { get; set; } = string.Empty;
        public bool? IsDraft { get; set; }


    }
}