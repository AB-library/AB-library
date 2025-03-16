using DataAccess.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DataAccess.Repositories;

public class DocumentRepository {
    private readonly IMongoCollection<DocumentEntity> _documents;

    public DocumentRepository(IOptions<MongoDBSettings> mongoDbSettings) {
        var settings = mongoDbSettings.Value;
        MongoClient client = new MongoClient(settings.AtlasURI);
        IMongoDatabase database = client.GetDatabase(settings.DatabaseName);
        _documents = database.GetCollection<DocumentEntity>(settings.CollectionName);

    }

    public async Task<List<DocumentEntity>> GetAllAsync() =>
        await _documents.Find(_ => true).ToListAsync();

    public async Task<DocumentEntity?> GetByIdAsync(string id) =>
        await _documents.Find(r => r.Id == id).FirstOrDefaultAsync();

    public async Task<DocumentEntity?> CreateAsync(DocumentEntity document) {
        await _documents.InsertOneAsync(document);
        return document.Id != null ? document : null;
    }

public async Task UpdateAsync(DocumentEntity document) =>
        await _documents.ReplaceOneAsync(r => r.Id == document.Id, document);

    public async Task DeleteByIdAsync(string id) =>
        await _documents.DeleteOneAsync(r => r.Id == id);
}