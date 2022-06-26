using AutoMapper;
using Denex.Application.Dto;
using Denex.Application.Interfaces.Repository;
using Denex.Application.Wrappers;
using Denex.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Denex.Application.Features.Commands.PracticeSchemas.PracticeSchemaUpdate
{
    public class PracticeSchemaUpdateCommand : IRequest<ServiceResponse<PracticeSchema>>
    {
        public string Id { get; set; }
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
                    practiceSchema.Name = request.Name;
                    practiceSchema.ExamDate = request.ExamDate;
                    var updatedPracticeSchema = await practiceSchemaRep.UpdateAsync(practiceSchema);
                    return new ServiceResponse<PracticeSchema>(updatedPracticeSchema);
                }
                else return new ServiceResponse<PracticeSchema>(true, "Sınav türü bulunamadı !");
            }
        }
    }
}
