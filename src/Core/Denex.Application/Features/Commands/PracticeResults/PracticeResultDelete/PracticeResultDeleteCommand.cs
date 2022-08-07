using Denex.Application.Interfaces.Repository;
using Denex.Application.Interfaces.Service;
using Denex.Application.Wrappers;
using Denex.Domain.Entities;
using MediatR;

namespace Denex.Application.Features.Commands.PracticeResults.PracticeResultDelete
{
    public class PracticeResultDeleteCommand: IRequest<ServiceResponse<PracticeResult>>
    {
        public string Id { get; set; }
        public class PracticeResultDeleteCommandHandler : IRequestHandler<PracticeResultDeleteCommand, ServiceResponse<PracticeResult>>
        {
            private readonly IPracticeResultRepository practiceResultRepository;
            private readonly IHttpContextService httpContextService;
            public PracticeResultDeleteCommandHandler(IHttpContextService httpContextService,IPracticeResultRepository practiceResultRepository)
            {
                this.httpContextService = httpContextService;
                this.practiceResultRepository = practiceResultRepository;
            }
            public async Task<ServiceResponse<PracticeResult>> Handle(PracticeResultDeleteCommand request, CancellationToken cancellationToken)
            {
                var userId = httpContextService.GetUserIdFromClaims();
                var deletedPracticeResult = await practiceResultRepository.DeleteAsync(x=> x.Id == request.Id && x.UserId == userId);
                return new ServiceResponse<PracticeResult>(deletedPracticeResult);
            }
        }
    }
}
