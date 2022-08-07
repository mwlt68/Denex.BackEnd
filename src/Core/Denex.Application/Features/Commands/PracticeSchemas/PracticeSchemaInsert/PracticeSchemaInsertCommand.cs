using AutoMapper;
using Denex.Application.Dto;
using Denex.Application.Interfaces.Repository;
using Denex.Application.Wrappers;
using Denex.Domain.Entities;
using MediatR;

namespace Denex.Application.Features.Commands.PracticeSchemas.PracticeSchemaInsert
{
    public class PracticeSchemaInsertCommand : IRequest<ServiceResponse<PracticeSchema>>
    {
        public DateTime? ExamDate { get; set; } = null;
        public int? NetCalculationRate { get; set; } = null;
        public int? Duration { get; set; } = null;
        public string Name { get; set; }
        public List<LessonSchemaInsertDto> Lessons { get; set; }

        public class PracticeSchemaInsertCommandHandler : IRequestHandler<PracticeSchemaInsertCommand, ServiceResponse<PracticeSchema>>
        {
            private readonly IMapper mapper;
            private readonly IPracticeSchemaRepository practiceSchemaRep;
            public PracticeSchemaInsertCommandHandler(IMapper mapper, IPracticeSchemaRepository practiceSchemaRep)
            {
                this.mapper = mapper;
                this.practiceSchemaRep = practiceSchemaRep;
            }
            public async Task<ServiceResponse<PracticeSchema>> Handle(PracticeSchemaInsertCommand request, CancellationToken cancellationToken)
            {
                var practiceSchema = mapper.Map<PracticeSchema>(request);
                if (practiceSchema?.Lessons != null)
                {
                    practiceSchema.Lessons.ForEach(x =>
                    {
                        x.Subjects= x.Subjects.Distinct().ToList();
                    });
                }
                var insertResult = await practiceSchemaRep.AddAsync(practiceSchema);
                return new ServiceResponse<PracticeSchema>(insertResult);
            }
        }
    }
}
