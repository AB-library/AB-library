using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConspectFiles.Dto.AppUSer
{
    public class LoginDto
    {
        [Required]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 30 characters.")]
        public string UserName {get; set;} = string.Empty;
        
        [Required]
        [MinLength(8, ErrorMessage = "Password must be least 8 characters long.")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$", ErrorMessage = "Password must contain at least one letter and one number.")]
        public string PasswordHash {get; set;} = string.Empty;
    }
}