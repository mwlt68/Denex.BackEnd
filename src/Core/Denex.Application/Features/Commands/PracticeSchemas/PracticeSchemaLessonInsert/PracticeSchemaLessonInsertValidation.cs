using FluentValidation;

namespace Denex.Application.Features.Commands.PracticeSchemas.PracticeSchemaLessonInsert
{
    public class PracticeSchemaLessonInsertValidation : AbstractValidator<PracticeSchemaLessonInsertCommand>
    {
        public PracticeSchemaLessonInsertValidation()
        {
            RuleFor(x=> x.QuestionCount)
                .GreaterThan(0)
                .WithMessage("Soru sayısı sıfırdan büyük olmalı !");
            RuleFor(x => x.Subjects)
                .NotNull()
                .NotEmpty()
                .WithMessage("Ders mutlaka konu içermelidir !");
        }
    }
}
