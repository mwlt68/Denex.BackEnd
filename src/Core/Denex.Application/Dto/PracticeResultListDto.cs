using Denex.Domain.Entities;

namespace Denex.Application.Dto
{
    public class PracticeResultListDto
    {
        public List<PracticeResultDetailDto> practiceResults { get; set; }
        public List<PracticeSchema> practiceSchemas { get; set; }
    }
}
