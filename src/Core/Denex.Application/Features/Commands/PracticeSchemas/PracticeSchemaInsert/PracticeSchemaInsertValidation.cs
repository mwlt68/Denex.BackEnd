using FluentValidation;

namespace Denex.Application.Features.Commands.PracticeSchemas.PracticeSchemaInsert
{
    public class PracticeSchemaInsertValidation : AbstractValidator<PracticeSchemaInsertCommand>
    {
        public PracticeSchemaInsertValidation()
        {
            RuleFor(x => x.Name).NotNull();

            // Eger NetCalculationRate null değil ise 0 dan büyük olmalıdır !

            RuleFor(x => x.NetCalculationRate)
                .Must(val=> val > 0)
                .When(x=> x.NetCalculationRate != null)
                .WithMessage("Net calculation rate must not be negative and zero !");


            // Eger Duration null değil ise 0 eşit yada  büyük olmalıdır !

            RuleFor(x=> x.Duration)
                .Must(val => val >= 0)
                .When(x => x.Duration != null)
                .WithMessage("Duration must not be negative value !");

            RuleFor(x => x.Lessons)
                .NotNull()
                .NotEmpty()
                .WithMessage("Practice schema must contain lesson !");

            RuleForEach(x => x.Lessons).ChildRules(y =>
            {
                y.RuleFor(x=> x.Name)
                .NotNull();
                y.RuleFor(x => x.QuestionCount)
                .GreaterThan(0)
                .WithMessage("Question count must bigger then zero!");
                y.RuleFor(x => x.Subjects)
                .NotNull()
                .NotEmpty()
                .WithMessage("Lesson must contain subject !");
            });
        }
    }
}
