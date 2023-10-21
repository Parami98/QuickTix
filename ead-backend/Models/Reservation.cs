using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic; 

namespace ead_backend.Models
{
    public class Reservation
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("UserNIC")]
        public string UserNIC { get; set; }

        [BsonElement("BookingDate")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime BookingDate { get; set; }

        [BsonElement("ReservationDate")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime ReservationDate { get; set; }

        [BsonElement("NoOfTickets")]
        public int NoOfTickets { get; set; }

        [BsonElement("Route")]
        public string Route { get; set; }

        [BsonElement("Train")]
        public string Train { get; set; }

        [BsonElement("StartingPoint")]
        public string StartingPoint { get; set; }

        [BsonElement("Destination")]
        public string Destination { get; set; }

        [BsonElement("Time")]
        public string Time { get; set; }

        [BsonElement("AgentID")]
        public string AgentID { get; set; }
        public string UserId { get; set; }
        public string Status { get; set; }
    }
}
