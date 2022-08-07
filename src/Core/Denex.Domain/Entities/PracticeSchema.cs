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
        [BsonElement("exam_date")]
        public DateTime? ExamDate { get; set; } = null;
        
        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("net_calculation_rate")]
        public int? NetCalculationRate { get; set; } = null;

        [BsonElement("duration")]
        public int? Duration { get; set; } = null;

        [BsonElement("lessons")]
        public List<LessonSchema> Lessons { get; set; }
    }

    public class LessonSchema: BaseEntity
    {
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("questionCount")]
        public int QuestionCount { get; set; }
        [BsonElement("subjects")]
        public List<String> Subjects { get; set; }
    }
}
