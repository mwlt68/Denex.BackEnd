using AutoMapper;
using Denex.Application.Dto;
using Denex.Application.Exceptions;
using Denex.Application.Interfaces.Repository;
using Denex.Application.Interfaces.Service;
using Denex.Application.Wrappers;
using Denex.Domain.Entities;
using Denex.Domain.Exceptions;
using MediatR;

namespace Denex.Application.Features.Commands.PracticeResults.PracticeResultUpdate
{
    public class PracticeResultUpdateCommand : IRequest<ServiceResponse<PracticeResultDetailDto>>
    {
        public string Id { get; set; }
        public string? Publisher { get; set; } = null;
        public string? Title { get; set; } = null;
        public string? Note { get; set; } = null;
        public int Duration { get; set; } = 0;
        public List<LessonResult> LessonResults { get; set; }


        public class PracticeResultUpdateCommandHandler : IRequestHandler<PracticeResultUpdateCommand, ServiceResponse<PracticeResultDetailDto>>
        {
            private readonly IPracticeResultRepository practiceResultRepo;
            private readonly IMapper mapper;
            private readonly IHttpContextService httpContextService;
            public PracticeResultUpdateCommandHandler(IMapper mapper,IPracticeResultRepository practiceResultRepo,IHttpContextService httpContextService)
            {
                this.mapper = mapper;
                this.practiceResultRepo = practiceResultRepo;
                this.httpContextService = httpContextService;
            }

            public async Task<ServiceResponse<PracticeResultDetailDto>> Handle(PracticeResultUpdateCommand request, CancellationToken cancellationToken)
            {
                string? currentUserId = httpContextService.GetUserIdFromClaims();
                bool currentUserCanAccess = await practiceResultRepo.CheckCurrentUserCanAccess(currentUserId,request.Id);
                if (currentUserCanAccess)
                {
                    var practiceResultMapped = mapper.Map<PracticeResult>(request);
                    var practiceResultUpdated = await practiceResultRepo.UpdateAsync(practiceResultMapped);
                    if (practiceResultUpdated != null)
                    {
                        var updatedResultDto = mapper.Map<PracticeResultDetailDto>(practiceResultUpdated);
                        return new ServiceResponse<PracticeResultDetailDto>(updatedResultDto);
                    }
                    else throw new PracticeResultUpdateException();
                }
                else throw new InternalServerException();
            }
        }
    }
}
