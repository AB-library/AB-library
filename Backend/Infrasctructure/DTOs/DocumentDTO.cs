using System.Text.Json.Serialization;

namespace DataAccess.Models;

public class DocumentDTO {
    
    [JsonIgnore]
    public string? Id { get; set; }
    public required string Title { get; set; }
    public required string Content { get; set; }
    public required List<string> Categories { get; set; }
    public required DateTime CreatedOn { get; set; }
    
}