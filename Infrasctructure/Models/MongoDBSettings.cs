namespace DataAccess.Models;

public class MongoDBSettings {
    public string AtlasURI { get; set; } = string.Empty;
    public string DatabaseName { get; set; } = string.Empty;
    public string CollectionName { get; set; } = string.Empty;
}