using Denex.Application.Interfaces.Repository;
using Denex.Application.Wrappers;
using Denex.Domain.Entities;
using MediatR;

namespace Denex.Application.Features.Commands.PracticeSchemas.PracticeSchemaDelete
{
    public class PracticeSchemaDeleteCommand : IRequest<ServiceResponse<PracticeSchema>>
    {
        public string Id { get; set; }

        public class PracticeSchemaDeleteCommandHandler : IRequestHandler<PracticeSchemaDeleteCommand, ServiceResponse<PracticeSchema>>
        {
            private readonly IPracticeSchemaRepository practiceSchemaRep;
            public PracticeSchemaDeleteCommandHandler( IPracticeSchemaRepository practiceSchemaRep)
            {
                this.practiceSchemaRep = practiceSchemaRep;
            }
            public async Task<ServiceResponse<PracticeSchema>> Handle(PracticeSchemaDeleteCommand request, CancellationToken cancellationToken)
            {
                var deletedPracticeSchema = await practiceSchemaRep.DeleteAsync(request.Id);
                return new ServiceResponse<PracticeSchema>(deletedPracticeSchema);
            }
        }
    }
}
