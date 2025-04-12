using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataAccess.Models;

public class DocumentEntity {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [JsonIgnore]
    public string? Id { get; set; }
    
    [BsonElement("Title")]
    public string Title { get; set; } = string.Empty;
    
    [BsonElement("Content")]
    public string Content { get; set; } = string.Empty;
    
    [BsonElement("Categories")]
    public List<string> Categories { get; set; } = new List<string>();

    [BsonElement("CreatedOn")]
    public DateTime CreatedOn { get; set; }
}