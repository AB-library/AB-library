using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using ConspectFiles.Data;
using ConspectFiles.Dto.CommentDTO;
using ConspectFiles.Interface;
using ConspectFiles.Model;
using Microsoft.AspNetCore.Http.HttpResults;
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

        public async Task<Comment?> Create(Comment commentModel)
        {
            try
            {
                await _database.Comments.InsertOneAsync(commentModel);
                return commentModel;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error while creating comment");
                return null;
            }
        }

        public async Task<Comment?> Delete(string id)
        {
            try
            {
                var comment = await _database.Comments.Find(c=>c.Id == id).FirstOrDefaultAsync();
                if (comment == null)
                {
                    return null;
                }
                await _database.Comments.DeleteOneAsync(c=>c.Id == id);
                return comment;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error while deleting comment");
                return null;
            }
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

        public async Task<Comment?> Update(string Id, UpdateCommentDto commentDto)
        {
            try
            {
               var comment = await _database.Comments.Find(c=>c.Id == Id).FirstOrDefaultAsync();
               if(comment == null)
               {
                    return null;
               }
               comment.AuthorName = commentDto.AuthorName;
               comment.Content = commentDto.Content;
               await _database.Comments.ReplaceOneAsync(c=>c.Id == Id, comment);
               return comment;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error while updating comment by Id");
                return null;
            }
        }
    }
}