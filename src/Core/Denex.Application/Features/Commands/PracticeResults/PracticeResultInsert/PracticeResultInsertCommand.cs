using AutoMapper;
using Denex.Application.Dto;
using Denex.Application.Exceptions;
using Denex.Application.Interfaces.Repository;
using Denex.Application.Interfaces.Service;
using Denex.Application.Wrappers;
using Denex.Domain.Entities;
using Denex.Domain.EntityExtensions;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;

namespace Denex.Application.Features.Commands.PracticeResults.PracticeResultInsert
{
    public class PracticeResultInsertCommand : IRequest<ServiceResponse<PracticeResultDetailDto>>
    {
        public string? PracticeSchemaId { get; set; } = null;
        public string? Publisher { get; set; } = null;
        public string? Title { get; set; } = null;
        public string? Note { get; set; } = null;
        public int Duration { get; set; } = 0;
        public List<LessonResult> LessonResults { get; set; }


        public class PracticeResultInsertCommandHandler : IRequestHandler<PracticeResultInsertCommand, ServiceResponse<PracticeResultDetailDto>>
        {
            private readonly IPracticeResultRepository practiceResultRepo;
            private readonly IPracticeSchemaRepository practiceSchemaRepo;
            private readonly IMapper mapper;
            private readonly string userId;
            public PracticeResultInsertCommandHandler(IMapper mapper, IHttpContextAccessor httpContextAccessor, IPracticeSchemaRepository practiceSchemaRepo,IPracticeResultRepository practiceResultRepo)
            {
                this.practiceResultRepo = practiceResultRepo;
                this.practiceSchemaRepo= practiceSchemaRepo;
                this.mapper = mapper;
                var userIdClaim = httpContextAccessor?.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);
                if (userIdClaim != null)
                {
                    userId = userIdClaim.Value;
                }
                else throw new UserNotFoundException();
            }
            public async Task<ServiceResponse<PracticeResultDetailDto>> Handle(PracticeResultInsertCommand request, CancellationToken cancellationToken)
            {
                var practiceSchema = await practiceSchemaRepo.GetAsync(x=> x.Id == request.PracticeSchemaId);
                if (practiceSchema != null)
                {
                    PracticeResult practiceResult=mapper.Map<PracticeResult>(request);
                    practiceResult.UserId = userId;
                    string? compareResult = practiceResult.LessonResults.Compare(practiceSchema.Lessons);
                    if (compareResult == null)
                    {
                        var insertResult = await practiceResultRepo.AddAsync(practiceResult);
                        if (insertResult != null)
                        {
                            var insertResultDto = mapper.Map<PracticeResultDetailDto>(insertResult);
                            return new ServiceResponse<PracticeResultDetailDto>(insertResultDto);
                        }
                        else throw new PracticeResultInsertException();
                    }
                    else throw new LessonException(compareResult);
                }
                else throw new PracticeSchemaNotFoundException();
            }
        }
    }
}
