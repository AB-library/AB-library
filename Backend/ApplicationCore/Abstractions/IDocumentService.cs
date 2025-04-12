using System.Reflection.Metadata;
using DataAccess.Models;

namespace BusinessLogic.Abstractions;

public interface IDocumentService {
    public Task<List<DocumentEntity>> GetAll();
    public Task<DocumentEntity?> GetById(string id);
    public Task<DocumentEntity?> Create(DocumentEntity document);
    public Task Update(DocumentEntity document);
    public Task Delete(string id);
}