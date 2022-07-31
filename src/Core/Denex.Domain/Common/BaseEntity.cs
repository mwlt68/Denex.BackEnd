using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Denex.Domain.Common
{
    [BsonIgnoreExtraElements]
    public class BaseEntity : IBaseEntity<string>
    {
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonId]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

        [BsonRepresentation(BsonType.DateTime)]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
