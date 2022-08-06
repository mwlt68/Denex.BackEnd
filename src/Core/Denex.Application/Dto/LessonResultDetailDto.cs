using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Denex.Application.Dto
{
    public class LessonResultDetailDto
    {
        public string Id { get; set; }
        public int TrueCount { get; set; }
        public int FalseCount { get; set; }
        public List<String>? ResultSubjects { get; set; } = null;
        public string Name { get; set; }
        public int QuestionCount { get; set; }
        public List<String> Subjects { get; set; }
    }
}
