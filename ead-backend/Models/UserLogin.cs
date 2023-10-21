using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic; 

namespace ead_backend.Models
{
    public class UserLogin
    {
        [BsonElement("NIC")]
        public string NIC { get; set; }

        [BsonElement("Password")]
        public string Password { get; set; }
    }
}
