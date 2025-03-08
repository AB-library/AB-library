using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConspectFiles.Model;


namespace ConspectFiles.Interface
{
    public interface IConspectRepository
    {
        Task<List<Conspect>> GetAll();
        Task<Conspect?> GetById(int id);
    }
}