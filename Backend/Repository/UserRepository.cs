using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConspectFiles.Interface;
using ConspectFiles.Data;
using ConspectFiles.Model;
using MongoDB.Driver;
using ConspectFiles.Dto.AppUSer;
using MongoDB.Bson;
using System.Security.Authentication;
using System.Security.Cryptography;
using BCrypt.Net;
using System.Runtime.CompilerServices;


namespace ConspectFiles.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly MongoDbService _database;
        private readonly IMongoCollection<AppUser> _users;

        public UserRepository(MongoDbService database, IMongoCollection<AppUser> users)
        {
            _database = database;
            _users = users;
        }

        public async Task<AppUser?> AuthenticateAsync(UserLoginDto loginDto)
        {
            var userModel = await _users.Find(u => u.UserName == loginDto.UserName).FirstOrDefaultAsync();
            
            if(userModel == null)
            {
                return null;
            }

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(loginDto.PasswordHash, userModel.PasswordHash);
            if(!isPasswordValid)
            {
                return null;
            }
            return userModel;
        }

        public async Task<AppUser?> CreateAsync(UserRegisterDto register, string refreshToken)
        {
            var existUser = await _users.Find(u => 
            u.UserName == register.UserName).FirstOrDefaultAsync();
            if(existUser != null)
            {
                return null;
            }
            string RegisterPasswordHash = BCrypt.Net.BCrypt.HashPassword(register.PasswordHash);
            var newUser = new AppUser
            {
                Id = ObjectId.GenerateNewId().ToString(),
                UserName = register.UserName,
                PasswordHash = RegisterPasswordHash,
                RefreshToken = refreshToken,
                Role = register.Role
                
            };
            await _users.InsertOneAsync(newUser);
            return newUser;
        }

        public async Task<AppUser?> DeleteAsync(string Userid)
        {
            var userModel = await _users.Find(u =>
            u.Id == Userid).FirstOrDefaultAsync();
            if(userModel == null)
            {
                return null;
            }
            await _users.DeleteOneAsync(user => user.Id == Userid);
            return userModel;
            
        }

        public async Task<List<AppUser>> GetAllAsync()
        {
            return await _users.Find(_ => true).ToListAsync();
        }

        public async Task<AppUser?> GetByIdAsync(string id)
        {
            var userModel = await _users.Find(u => u.Id == id).FirstOrDefaultAsync();
            if(userModel == null) return null;
            return userModel;
        }

        public async Task<AppUser?> GetUserByRefreshToken(RefreshTokenDto tokenModel)
        {
            return await _users.Find(u => u.RefreshToken == tokenModel.RefreshToken).FirstOrDefaultAsync();
        }

        public async Task SaveRefreshTokenAsync(string userId, string refreshToken)
        {
            var userModel = await _users.Find(u => u.Id == userId).FirstOrDefaultAsync();
            if(userModel != null)
            {
                userModel.RefreshToken = refreshToken;
                await _users.ReplaceOneAsync(u => u.Id == userId, userModel);
            }
        }

        public async Task<AppUser?> UpdateAsync(USerUpdateDto updateDto, string id)
        {
            var userModel = await _users.Find(u => u.Id == id).FirstOrDefaultAsync();
            if(userModel == null)
            {
                return null;
            }
            userModel.UserName = updateDto.UserName;
            userModel.Role = updateDto.Role;
            await _users.ReplaceOneAsync(u => u.Id == id, userModel);
            
            return userModel;


            
            
        }
    }
}