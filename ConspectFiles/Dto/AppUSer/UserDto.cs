using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConspectFiles.Dto.AppUSer
{
    public class UserDto
    {   
        public string? Id {get; set;}
        public string UserName {get; set;} = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role {get; set; } = string.Empty;
    }
}