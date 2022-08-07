using FluentValidation;

namespace Denex.Application.Features.Commands.PracticeSchemas.PracticeSchemaLessonInsert
{
    public class PracticeSchemaLessonInsertValidation : AbstractValidator<PracticeSchemaLessonInsertCommand>
    {
        public PracticeSchemaLessonInsertValidation()
        {
            RuleFor(x=> x.QuestionCount)
                .GreaterThan(0)
                .WithMessage("Question count must bigger then zero !");

            RuleFor(x => x.Subjects)
                .NotNull()
                .NotEmpty()
                .WithMessage("The lesson must contain subject !");
        }
    }
}
