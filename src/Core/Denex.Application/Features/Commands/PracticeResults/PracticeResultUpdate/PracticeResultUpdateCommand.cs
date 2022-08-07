using AutoMapper;
using Denex.Application.Dto;
using Denex.Application.Exceptions;
using Denex.Application.Interfaces.Repository;
using Denex.Application.Interfaces.Service;
using Denex.Application.Wrappers;
using Denex.Domain.Entities;
using Denex.Domain.EntityExtensions;
using MediatR;

namespace Denex.Application.Features.Commands.PracticeResults.PracticeResultUpdate
{
    public class PracticeResultUpdateCommand : IRequest<ServiceResponse<PracticeResultDetailDto>>
    {
        public string Id { get; set; }
        public string? Publisher { get; set; } = null;
        public string? Title { get; set; } = null;
        public string? Note { get; set; } = null;
        public int? Duration { get; set; } = null;
        public List<LessonResult> LessonResults { get; set; }


        public class PracticeResultUpdateCommandHandler : IRequestHandler<PracticeResultUpdateCommand, ServiceResponse<PracticeResultDetailDto>>
        {
            private readonly IPracticeResultRepository practiceResultRepo;
            private readonly IPracticeSchemaRepository practiceSchemaRepository;
            private readonly IMapper mapper;
            private readonly IHttpContextService httpContextService;
            public PracticeResultUpdateCommandHandler(IMapper mapper, IPracticeResultRepository practiceResultRepo, IPracticeSchemaRepository practiceSchemaRepository, IHttpContextService httpContextService)
            {
                this.mapper = mapper;
                this.practiceResultRepo = practiceResultRepo;
                this.httpContextService = httpContextService;
                this.practiceSchemaRepository = practiceSchemaRepository;
            }

            public async Task<ServiceResponse<PracticeResultDetailDto>> Handle(PracticeResultUpdateCommand request, CancellationToken cancellationToken)
            {
                string currentUserId = httpContextService.GetUserIdFromClaims();
                var practiceResult = await practiceResultRepo.GetAsync(x => x.Id == request.Id && x.UserId == currentUserId);
                if (practiceResult != null)
                {
                    var practiceSchema = await practiceSchemaRepository.GetAsync(x => x.Id == practiceResult.PracticeSchemaId);
                    if (practiceSchema != null)
                    {
                        var practiceResultMapped = mapper.Map(request, practiceResult);
                        string? compareResult = practiceResultMapped.LessonResults.Compare(practiceSchema.Lessons);
                        if (compareResult == null)
                        {
                            var practiceResultUpdated = await practiceResultRepo.UpdateAsync(practiceResultMapped);
                            if (practiceResultUpdated != null)
                            {
                                var updatedResultDto = mapper.Map<PracticeResultDetailDto>(practiceResultUpdated);
                                return new ServiceResponse<PracticeResultDetailDto>(updatedResultDto);
                            }
                            else throw new PracticeResultUpdateException();
                        }
                        else throw new LessonException(compareResult);
                    }
                    else throw new PracticeSchemaNotFoundException();
                }
                else throw new PracticeResultNotFoundException();
            }
        }
    }
}
