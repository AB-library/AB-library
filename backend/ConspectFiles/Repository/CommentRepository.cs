using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConspectFiles.Data;
using ConspectFiles.Interface;
using ConspectFiles.Model;
using MongoDB.Driver;

namespace ConspectFiles.Repository
{
    public class CommentRepository: ICommentRepository
    {
        private readonly MongoDbService _database;
        private readonly ILogger<CommentRepository> _logger;
        public CommentRepository(MongoDbService database, ILogger<CommentRepository> logger)
        {
            _database = database;
            _logger = logger;
        }

        public async Task<List<Comment>> GetAll()
        {
            try
            {
                return await _database.Comments.Find(_ => true).ToListAsync();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving all comments");
                return new List<Comment>();
            }
        }

        public async Task<Comment?> GetById(string id)
        {
            try
            {
                return await _database.Comments.Find(c=>c.Id == id).FirstOrDefaultAsync();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving comment by id");
                return null;
            }
        }
    }
}