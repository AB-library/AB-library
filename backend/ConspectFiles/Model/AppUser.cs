using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ConspectFiles.Model
{
    public class AppUser
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        
        public string? Id {get; set;}
        
        public string UserName {get; set;} = string.Empty;
        public string PasswordHash {get; set;} = string.Empty;
        public string RefreshToken {get; set;} = string.Empty;
        public string Role { get; set; } = "User";
    }


}