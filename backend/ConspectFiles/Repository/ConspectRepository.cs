using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConspectFiles.Interface;
using ConspectFiles.Data;
using ConspectFiles.Model;
using MongoDB.Driver;


namespace ConspectFiles.Repository
{
    public class ConspectRepository : IConspectRepository
    {
        private readonly MongoDbService _database;

        public ConspectRepository(MongoDbService database)
        {
            _database = database;
        }

        public async Task<List<Conspect>> GetAll()
        {
            return await _database.Conspects.Find(_ => true).ToListAsync();
        }

        public async Task<Conspect?> GetById(int id)
        {
            var filter = Builders<Conspect>.Filter.Eq(c=>c.Id, id);
            return await  _database.Conspects.Find(filter).FirstOrDefaultAsync();
        }
    }
}