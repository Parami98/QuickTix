using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ead_backend.Models
{
    public class AdminLogin
    {
        [BsonElement("Email")]
        public string Email { get; set; }

        [BsonElement("Password")]
        public string Password { get; set; }
    }
}
