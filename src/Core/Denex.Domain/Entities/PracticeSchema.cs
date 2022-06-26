using Denex.Domain.Common;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Denex.Domain.Entities
{
    public class PracticeSchema: BaseEntity
    {
        public DateTime? ExamDate { get; set; } = null;
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("lessons")]
        public List<LessonSchema> Lessons { get; set; } = null;
    }

    public class LessonSchema: BaseEntity
    {
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("questionCount")]
        public int QuestionCount { get; set; }
        [BsonElement("subjects")]
        public List<String> Subjects { get; set; } = null;
    }
}
