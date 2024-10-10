using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace TinyUrl.RedirectionService.Infrastructure.Entites
{
    public class UrlMapping
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)] 
        public ObjectId Id { get; set; }
        public string? ShortUrl { get; set; }
        public string? LongUrl { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public int UserId { get; set; }
        public int Clicks { get; set; }
    }
}
