using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConspectFiles.Interface;
using ConspectFiles.Data;
using ConspectFiles.Model;
using MongoDB.Driver;
using ConspectFiles.Dto;
using Microsoft.AspNetCore.Http.HttpResults;


namespace ConspectFiles.Repository
{
    public class ConspectRepository : IConspectRepository
    {
        private readonly MongoDbService _database;

        public ConspectRepository(MongoDbService database)
        {
            _database = database;
        }

        public async Task<Conspect?> Create(Conspect conspectModel)
        {
            await _database.Conspects.InsertOneAsync(conspectModel);
            return conspectModel;
        }

        public async Task<Conspect?> Delete(string id)
        {
            var conspect = await _database.Conspects.Find(c=>c.Id == id).FirstOrDefaultAsync();
            if(conspect == null)
            {
                return null;
            }
            await _database.Conspects.DeleteOneAsync(c=>c.Id == id);
            return conspect;
        }

        public async Task<List<Conspect>> GetAll()
        {
            return await _database.Conspects.Find(_ => true).ToListAsync();
        }

        public async Task<Conspect?> GetById(string id)
        {
            return await  _database.Conspects.Find(c=>c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Conspect?> Update(string id, UpdateConspectDto conspectDto)
        {
            var conspect = await _database.Conspects.Find(c=>c.Id == id).FirstOrDefaultAsync();
            if(conspect == null)
            {
                return null;
            }
            conspect.Title = conspectDto.Title;
            conspect.Content = conspectDto.Content;
            return conspect;
        }
    }
}