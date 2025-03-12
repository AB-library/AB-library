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
            _client = new MongoClient(configuration["mongodb+srv://st9165305:23C4B3_qwerty@cluster0.ial8y.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0"]);
            _database = _client.GetDatabase(configuration["AB library(DB)"]);
            _users = _database.GetCollection<AppUser>("users");
        }

        public IMongoDatabase Database => _database;
        public IMongoCollection<Conspect> Conspects => _database.GetCollection<Conspect>("Conspects");
    }
}
