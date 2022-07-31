using Denex.Domain.Common;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Denex.Domain.Entities
{
    public class PracticeResult : BaseEntity
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string PracticeSchemaId { get; set; }
        [BsonElement("publisher")]
        public string? Publisher { get; set; } = null;
        [BsonElement("title")]
        public string? Title { get; set; } = null;
        [BsonElement("note")]
        public string? Note { get; set; } = null;
        [BsonElement("duration")]
        public int Duration { get; set; }
        [BsonElement("lessonResults")]
        public List<LessonResult> LessonResults { get; set; }

       
    }

    public class LessonResult
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string LessonId { get; set; }
        [BsonElement("trueCount")]
        public int TrueCount { get; set; }
        [BsonElement("falseCount")]
        public int FalseCount { get; set; }
        [BsonElement("subjects")]
        public List<String>? Subjects { get; set; } = null;

    }
}
