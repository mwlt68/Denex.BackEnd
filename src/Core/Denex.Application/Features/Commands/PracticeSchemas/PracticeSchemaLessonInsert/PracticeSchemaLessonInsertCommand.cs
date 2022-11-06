using AutoMapper;
using Denex.Application.Exceptions;
using Denex.Application.Interfaces.Repository;
using Denex.Application.Wrappers;
using Denex.Domain.Entities;
using MediatR;

namespace Denex.Application.Features.Commands.PracticeSchemas.PracticeSchemaLessonInsert
{
    public class PracticeSchemaLessonInsertCommand: IRequest<ServiceResponse<LessonSchema>>
    {
        public PracticeSchemaLessonInsertCommand(string practiceSchemaId, string name, int questionCount, List<string> subjects)
        {
            PracticeSchemaId = practiceSchemaId;
            Name = name;
            QuestionCount = questionCount;
            Subjects = subjects;
        }

        public string PracticeSchemaId { get; set; }
        public string Name { get; set; }
        public int QuestionCount { get; set; }
        public List<String> Subjects { get; set; }

        public class PracticeSchemaLessonInsertCommandHandler : IRequestHandler<PracticeSchemaLessonInsertCommand, ServiceResponse<LessonSchema>>
        {
            private readonly IMapper mapper;
            private readonly IPracticeSchemaRepository practiceSchemaRep;
            public PracticeSchemaLessonInsertCommandHandler(IMapper mapper, IPracticeSchemaRepository practiceSchemaRep)
            {
                this.mapper = mapper;
                this.practiceSchemaRep = practiceSchemaRep;
            }
            public async Task<ServiceResponse<LessonSchema>> Handle(PracticeSchemaLessonInsertCommand request, CancellationToken cancellationToken)
            {
                var practiceSchema = await practiceSchemaRep.GetByIdAsync(request.PracticeSchemaId);
                if (practiceSchema != null)
                {
                    var lessonSchema = mapper.Map<LessonSchema>(request);
                    lessonSchema.Subjects = lessonSchema.Subjects.Distinct().ToList();
                    practiceSchema.Lessons.Add(lessonSchema);
                    var practiceSchemaUpdated = await practiceSchemaRep.UpdateAsync(practiceSchema);
                    if (practiceSchemaUpdated != null)
                    {
                        var lesson = practiceSchemaUpdated.Lessons.FirstOrDefault(l => l.Id == lessonSchema.Id);
                        return new ServiceResponse<LessonSchema>(lesson);
                    }
                    throw new LessonException("Lesson update error !");
                }
                throw new PracticeSchemaNotFoundException();
            }
        }
    }
}
