using AutoMapper;
using Denex.Application.Dto;
using Denex.Application.Exceptions;
using Denex.Application.Interfaces.Repository;
using Denex.Application.Interfaces.Service;
using Denex.Application.Wrappers;
using MediatR;

namespace Denex.Application.Features.Queries.PracticeResult.PracticeResultList
{
    public class PracticeResultListQuery : IRequest<ServiceResponse<PracticeResultListDto>>
    {

        public class PracticeResultListQueryHandler : IRequestHandler<PracticeResultListQuery, ServiceResponse<PracticeResultListDto>>
        {
            private readonly IPracticeSchemaRepository practiceSchemaRepository;
            private readonly IPracticeResultRepository practiceResultRepository;
            private readonly IHttpContextService httpContextService;
            private readonly IMapper mapper; 
            public PracticeResultListQueryHandler(IMapper mapper,IHttpContextService httpContextService,IPracticeSchemaRepository practiceSchemaRepository, IPracticeResultRepository practiceResultRepository)
            {
                this.httpContextService = httpContextService;
                this.practiceSchemaRepository = practiceSchemaRepository;
                this.practiceResultRepository = practiceResultRepository;
                this.mapper = mapper;
            }
            public async Task<ServiceResponse<PracticeResultListDto>> Handle(PracticeResultListQuery request, CancellationToken cancellationToken)
            {
                string? userId = httpContextService.GetUserIdFromClaims();
                if (userId != null)
                {
                    var practiceResults = await practiceResultRepository.GetListAsync(x => x.UserId == userId);
                    var practiceSchemaIdList = practiceResults.Select(x => x.PracticeSchemaId).ToList();
                    var practiceSchemas = await practiceSchemaRepository.GetListAsync(x => practiceSchemaIdList.Contains(x.Id));
                    var practiceResultDetailDtos = mapper.Map<List<PracticeResultDetailDto>>(practiceResults);
                    PracticeResultListDto practiceResultListDto = new PracticeResultListDto()
                    {
                        practiceResults = practiceResultDetailDtos,
                        practiceSchemas = practiceSchemas
                    };
                    return new ServiceResponse<PracticeResultListDto>(practiceResultListDto);
                }
                else throw new UserNotFoundException();
            }
        }
    }
}
