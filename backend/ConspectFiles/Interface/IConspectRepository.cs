using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConspectFiles.Dto;
using ConspectFiles.Model;


namespace ConspectFiles.Interface
{
    public interface IConspectRepository
    {
        Task<List<Conspect>> GetAll();
        Task<Conspect?> GetById(string id);
        Task<Conspect?> Create(Conspect conspectModel);
        Task<Conspect?> Update(string id, UpdateConspectDto conspectDto);
        Task<Conspect?> Delete(string id);
    }
}