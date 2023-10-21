using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ead_backend.Models
{
    public class Admin
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("Name")]
        public string? Name { get; set; }

        [BsonElement("Email")]
        public string? Email { get; set; }

        [BsonElement("Password")]
        public string? Password { get; set; }

        [BsonElement("Role")]
        public string? Role { get; set; }
    }
}
