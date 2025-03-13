using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConspectFiles.Dto.AppUSer
{
    public class RegisterDto
    {
        [Required]
        public string UserName {get; set;} = string.Empty;
        [Required]
        public string PasswordHash {get; set;} = string.Empty;

    }
}