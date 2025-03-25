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
        Task<AppUser?> CreateAsync(RegisterDto register, string refreshToken);
        Task<List<AppUser>> GetAllAsync();
        Task<AppUser?> DeleteAsync(string id);
        Task<AppUser?> GetByIdAsync(string id);
        Task<AppUser?> AuthenticateAsync(LoginDto loginDto);
        Task<AppUser?> GetUserByRefreshToken(RefreshTokenDto tokenModel);
        Task SaveRefreshTokenAsync(string userId, string refreshToken);
    }
}