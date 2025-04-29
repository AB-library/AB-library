using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConspectFiles.Dto.AppUSer;
using ConspectFiles.Model;

namespace ConspectFiles.Mapper.UserMappers
{
    public static class UserMappers
    {
        public static UserDto ToUserDto(this AppUser appUser)
        {
            return new UserDto
            {
                Id = appUser.Id,
                UserName = appUser.UserName,
                Email = appUser.Email,
                Role = appUser.Role
            };
        }
    }
}