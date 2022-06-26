using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Denex.Application.Dto
{
    public class LessonSchemaUpdateDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int QuestionCount { get; set; }
        public List<String> Subjects { get; set; } = null;
    }
}
