using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConspectFiles.Dto;
using ConspectFiles.Dto.AppUSer;
using ConspectFiles.Model;


namespace ConspectFiles.Interface
{
    public interface IUserRepository
    {
        Task<AppUser> LoginAsync(LoginDto login);
        Task<AppUser?> CreateAsync(RegisterDto register);
    }
}