using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ConspectFiles.Helpers;

namespace ConspectFiles.Dto.AppUSer
{
    public class USerUpdateDto
    {
        public string UserName {get; set;} = string.Empty;
        [Required]
        [EnumDataType(typeof(RoleEnum), ErrorMessage = "Invalid role.")]
        public string Role { get; set; } = "User";
    }
}