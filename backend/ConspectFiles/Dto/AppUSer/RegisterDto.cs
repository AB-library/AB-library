using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConspectFiles.Dto.AppUSer
{
    public class RegisterDto
    {
        public string UserName {get; set;} = string.Empty;
        public string PasswordHash {get; set;} = string.Empty;

    }
}