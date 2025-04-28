using MongoDB.Driver;
using ConspectFiles.Model;

namespace ConspectFiles.Data
{
    public class MongoDbService
    {
        private readonly IMongoClient _client;
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<AppUser> _users;
        public MongoDbService(IConfiguration configuration)
        {
            _client = new MongoClient(configuration["MONGO_CONNECTION_STRING"]);        
            _database = _client.GetDatabase(configuration["MONGO_DATABASE_NAME"]);
            _users = _database.GetCollection<AppUser>("users");
        }
        
        public IMongoDatabase Database => _database;
        public IMongoCollection<Conspect> Conspects => _database.GetCollection<Conspect>("Conspects");
    }
}
