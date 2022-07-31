using Denex.Application.Interfaces.Repository;
using Denex.Application.Wrappers;
using Denex.Domain.Entities;
using MediatR;

namespace Denex.Application.Features.Queries.PracticeSchemaList
{
    public class PracticeSchemaListQuery : IRequest<ServiceResponse<List<PracticeSchema>>>
    {
        public class PracticeSchemaListQueryHandler : IRequestHandler<PracticeSchemaListQuery, ServiceResponse<List<PracticeSchema>>>
        {
            private readonly IPracticeSchemaRepository practiceSchemaRep;
            public PracticeSchemaListQueryHandler(IPracticeSchemaRepository practiceSchemaRep)
            {
                this.practiceSchemaRep = practiceSchemaRep;
            }
            public async Task<ServiceResponse<List<PracticeSchema>>> Handle(PracticeSchemaListQuery request, CancellationToken cancellationToken)
            {

                var schema = await practiceSchemaRep.GetListAsync(x => x.Id == "62c072cf9d140ae7d7231f34");
                var schemas = await practiceSchemaRep.GetListAsync(x => true);
                return new ServiceResponse<List<PracticeSchema>>(schemas);
            }
        }
    }
}
