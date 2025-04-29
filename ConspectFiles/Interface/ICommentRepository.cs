using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConspectFiles.Dto.CommentDTO;
using ConspectFiles.Model;

namespace ConspectFiles.Interface
{
    public interface ICommentRepository
    {
      Task <List<Comment>> GetAll(); 
      Task <Comment?> GetById(string id);
      Task <Comment?> Create(Comment commentModel);
      Task<Comment?> Update(string Id, UpdateCommentDto commentDto);
      Task<Comment?> Delete (string id);


    }
}