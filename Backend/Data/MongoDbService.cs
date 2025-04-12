using MongoDB.Driver;
using ConspectFiles.Model;

namespace ConspectFiles.Data
{
    public class MongoDbService
    {
        private readonly IMongoClient _client;
        private readonly IMongoDatabase _database;
        public MongoDbService(IConfiguration configuration)
        {
            _client = new MongoClient(configuration["MONGO_CONNECTION_STRING"]);
            _database = _client.GetDatabase(configuration["MONGO_DATABASE_NAME"]);
        }

        public IMongoDatabase Database => _database;
        public IMongoCollection<Conspect> Conspects => _database.GetCollection<Conspect>("Conspects");
        public IMongoCollection<Comment> Comments => _database.GetCollection<Comment>("Comments");
    }
}
