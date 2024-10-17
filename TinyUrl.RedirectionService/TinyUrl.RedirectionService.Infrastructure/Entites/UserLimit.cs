using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TinyUrl.RedirectionService.Infrastructure.Entites
{
    public class UserLimit
    {
        [BsonId]
        [BsonRepresentation(BsonType.Int32)]
        public int UserId { get; set; }
        public int DailyCalls { get; set; }
    }
}
