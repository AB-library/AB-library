using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ConspectFiles.Model
{
    public class Comment
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;
        
        
        [BsonElement("authorName")]
        public string AuthorName { get; set; } = string.Empty;
        
        [BsonElement("content")]
        public string Content { get; set; } = string.Empty;
        
        [BsonElement("createdOn")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        
        [BsonElement("conspectId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ConspectId { get; set; } = string.Empty;
        
    }
}