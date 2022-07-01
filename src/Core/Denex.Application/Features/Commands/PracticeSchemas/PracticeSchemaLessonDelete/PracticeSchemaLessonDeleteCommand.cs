using Denex.Application.Interfaces.Repository;
using Denex.Application.Wrappers;
using Denex.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Denex.Application.Features.Commands.PracticeSchemas.PracticeSchemaLessonDelete
{
    public class PracticeSchemaLessonDeleteCommand: IRequest <ServiceResponse<LessonSchema>>
    {
        public string Id { get; set; }

        public class PracticeSchemaLessonDeleteCommandHandler : IRequestHandler<PracticeSchemaLessonDeleteCommand, ServiceResponse<LessonSchema>>
        {
            private readonly IPracticeSchemaRepository practiceSchemaRepo;
            public PracticeSchemaLessonDeleteCommandHandler(IPracticeSchemaRepository practiceSchemaRepo)
            {
                this.practiceSchemaRepo = practiceSchemaRepo;
            }
            public async Task<ServiceResponse<LessonSchema>> Handle(PracticeSchemaLessonDeleteCommand request, CancellationToken cancellationToken)
            {
                var practiceSchema =await practiceSchemaRepo.GetAsync(x => x.Lessons != null && x.Lessons.Any(y => y.Id == request.Id));
                if (practiceSchema != null)
                {
                    var lesson = practiceSchema.Lessons.FirstOrDefault(x => x.Id == request.Id);
                    if (lesson != null)
                    {
                        practiceSchema.Lessons.Remove(lesson);
                        var updatedPracticeSchema = await practiceSchemaRepo.UpdateAsync(practiceSchema);
                        if (updatedPracticeSchema != null)
                        {
                            return new ServiceResponse<LessonSchema>(lesson);
                        }
                        else return new ServiceResponse<LessonSchema>("Delete operation error !");
                    }
                    else return new ServiceResponse<LessonSchema>("Lesson not found !");
                }
                else return new ServiceResponse<LessonSchema>("Practice schema not found !");

            }
        }
    }
}
