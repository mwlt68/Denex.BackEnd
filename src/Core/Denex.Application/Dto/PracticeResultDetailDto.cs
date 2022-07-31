using Denex.Domain.Entities;

namespace Denex.Application.Dto
{
    public class PracticeResultDetailDto
    {
        public string Id { get; set; }
        public string? PracticeSchemaId { get; set; }
        public string? Publisher { get; set; } = null;
        public string? Title { get; set; } = null;
        public string? Note { get; set; } = null;
        public int Duration { get; set; } = 0;
        public List<LessonResult>? LessonResults { get; set; } = null;
    }
}
