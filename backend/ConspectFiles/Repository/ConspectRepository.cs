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
using ConspectFiles.Helpers;


namespace ConspectFiles.Repository
{
    public class ConspectRepository : IConspectRepository
    {
        private readonly MongoDbService _database;
        private readonly ILogger<ConspectRepository> _logger;

        public ConspectRepository(MongoDbService database, ILogger<ConspectRepository> logger)
        {
            _database = database;
            _logger = logger;
        }

        public async Task<Conspect?> Create(Conspect conspectModel)
        {
            try
            {
                await _database.Conspects.InsertOneAsync(conspectModel);
                return conspectModel;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error while creating conspect");
                return null;
            }
        }

        public async Task<Conspect?> Delete(string id)
        {
            try
            {
                var conspect = await _database.Conspects.Find(c=>c.Id == id).FirstOrDefaultAsync();
                if(conspect == null)
                {
                    return null;
                }
                await _database.Conspects.DeleteOneAsync(c=>c.Id == id);
                return conspect;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error while deleting conspect dy Id");
                return null;
            }
        }

        public async Task<List<Conspect>> GetAll(QueryObject query)
        {
            try
            {
                var filter = Builders<Conspect>.Filter.Empty;

                if (!string.IsNullOrEmpty(query.Title))
                {
                    filter = Builders<Conspect>.Filter.Eq(c => c.Title, query.Title);
                }

                return await _database.Conspects.Find(filter).ToListAsync();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving all conspects");
                return new List<Conspect>();
            }
        }

        public async Task<Conspect?> GetById(string id)
        {
            try
            {
                return await  _database.Conspects.Find(c=>c.Id == id).FirstOrDefaultAsync();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving conspect by Id");
                return null;
            }
        }

        public async Task<Conspect?> Update(string id, UpdateConspectDto conspectDto)
        {
            try
            {
               var conspect = await _database.Conspects.Find(c=>c.Id == id).FirstOrDefaultAsync();
            if(conspect == null)
            {
                return null;
            }
            conspect.Title = conspectDto.Title;
            conspect.Content = conspectDto.Content;
            await _database.Conspects.ReplaceOneAsync(c => c.Id == id, conspect);
            return conspect; 
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error while updatind conspect by Id");
                return null;
            }
        }
    }
}