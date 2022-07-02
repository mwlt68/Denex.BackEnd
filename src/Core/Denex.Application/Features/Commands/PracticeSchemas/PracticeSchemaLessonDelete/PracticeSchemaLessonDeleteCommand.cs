using Denex.Application.Exceptions;
using Denex.Application.Interfaces.Repository;
using Denex.Application.Wrappers;
using MediatR;

namespace Denex.Application.Features.Commands.PracticeSchemas.PracticeSchemaLessonDelete
{
    public class PracticeSchemaLessonDeleteCommand: IRequest <VoidServiceResponse>
    {
        public string Id { get; set; }

        public class PracticeSchemaLessonDeleteCommandHandler : IRequestHandler<PracticeSchemaLessonDeleteCommand, VoidServiceResponse>
        {
            private readonly IPracticeSchemaRepository practiceSchemaRepo;
            public PracticeSchemaLessonDeleteCommandHandler(IPracticeSchemaRepository practiceSchemaRepo)
            {
                this.practiceSchemaRepo = practiceSchemaRepo;
            }
            public async Task<VoidServiceResponse> Handle(PracticeSchemaLessonDeleteCommand request, CancellationToken cancellationToken)
            {
                var practiceSchema =await practiceSchemaRepo.GetAsync(x => x.Lessons != null && x.Lessons.Any(y => y.Id == request.Id));
                if (practiceSchema != null)
                {
                    var lesson = practiceSchema.Lessons.FirstOrDefault(x => x.Id == request.Id);
                    if (lesson != null)
                    {
                        practiceSchema.Lessons.Remove(lesson);
                        await practiceSchemaRepo.UpdateAsync(practiceSchema);
                        return new VoidServiceResponse();
                    }
                    else throw new LessonSchemaNotFound();
                }
                else throw new PracticeSchemaNotFoundException();

            }

        }
    }
}
