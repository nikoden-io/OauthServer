using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OauthServer.Domain.Entities;

public class ApplicationUser
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;

    [BsonElement("UserName")] public string UserName { get; set; } = string.Empty;

    [BsonElement("Password")] public string Password { get; set; } = string.Empty;

    [BsonElement("Email")] public string Email { get; set; } = string.Empty;
}