using AutoMapper;
using Denex.Application.Exceptions;
using Denex.Application.Interfaces.Repository;
using Denex.Application.Wrappers;
using Denex.Domain.Entities;
using MediatR;

namespace Denex.Application.Features.Commands.PracticeSchemas.PracticeSchemaUpdate
{
    public class PracticeSchemaUpdateCommand : IRequest<ServiceResponse<PracticeSchema>>
    {
        public PracticeSchemaUpdateCommand(string id,string name,int? netCalculationRate = null, int? duration = null, DateTime? examDate= null)
        {
            Id = id;
            NetCalculationRate = netCalculationRate;
            Duration = duration;
            ExamDate = examDate;
            Name = name;
        }

        public string Id { get; set; }
        public int? NetCalculationRate { get; set; } = null;
        public int? Duration { get; set; } = null;
        public DateTime? ExamDate { get; set; } = null;
        public string Name { get; set; }

        public class PracticeSchemaUpdateCommandHandler : IRequestHandler<PracticeSchemaUpdateCommand, ServiceResponse<PracticeSchema>>
        {
            private readonly IPracticeSchemaRepository practiceSchemaRep;
            private readonly IMapper mapper;
            public PracticeSchemaUpdateCommandHandler(IMapper mapper, IPracticeSchemaRepository practiceSchemaRep)
            {
                this.mapper = mapper;
                this.practiceSchemaRep = practiceSchemaRep;
            }
            public async Task<ServiceResponse<PracticeSchema>> Handle(PracticeSchemaUpdateCommand request, CancellationToken cancellationToken)
            {
                var practiceSchema = await practiceSchemaRep.GetByIdAsync(request.Id);
                if (practiceSchema != null)
                {
                    practiceSchema = mapper.Map(request, practiceSchema);
                    var updatedPracticeSchema = await practiceSchemaRep.UpdateAsync(practiceSchema);
                    return new ServiceResponse<PracticeSchema>(updatedPracticeSchema);
                }
                else throw new PracticeSchemaNotFoundException();
            }
        }
    }
}
