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

    public async Task EnsureIndexesAsync() {
        var indexKeysDefinition = Builders<DocumentEntity>.IndexKeys
            .Text(d => d.Title)
            .Text(d => d.Content)
            .Text(d => d.Categories);

        var indexModel = new CreateIndexModel<DocumentEntity>(indexKeysDefinition);
        await _documents.Indexes.CreateOneAsync(indexModel);
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
    
    public async Task<List<DocumentEntity>> SearchAsync(string query) {
        var filter = Builders<DocumentEntity>.Filter.Text(query);
        return await _documents.Find(filter).ToListAsync();
    }

    public async Task<List<DocumentEntity>> FilterSearchAsync(
        string? query, 
        List<string>? categories,
        DateTime? createdAfter, 
        DateTime? createdBefore
        ) {
        var filterBuilder = Builders<DocumentEntity>.Filter;
        var filters = new List<FilterDefinition<DocumentEntity>>();

        if (!string.IsNullOrEmpty(query)) {
            filters.Add(filterBuilder.Text(query));
        }

        if (categories != null && categories.Count > 0) {
            filters.Add(filterBuilder.AnyIn(d => d.Categories, categories));
        }

        if (createdAfter.HasValue) {
            filters.Add(filterBuilder.Gte(d => d.CreatedOn, createdAfter.Value));
        }

        if (createdBefore.HasValue) {
            filters.Add(filterBuilder.Lte(d => d.CreatedOn, createdBefore.Value));
        }

        var combinedFilter = filters.Count > 0 ? filterBuilder.And(filters) : filterBuilder.Empty;
        return await _documents.Find(combinedFilter).ToListAsync();
    }
}