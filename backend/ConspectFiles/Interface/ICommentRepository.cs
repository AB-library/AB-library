using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConspectFiles.Model;

namespace ConspectFiles.Interface
{
    public interface ICommentRepository
    {
      Task <List<Comment>> GetAll(); 
      Task <Comment?> GetById(string id);
    }
}