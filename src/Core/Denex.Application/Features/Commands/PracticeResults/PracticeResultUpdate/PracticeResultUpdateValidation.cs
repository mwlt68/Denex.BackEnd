using FluentValidation;

namespace Denex.Application.Features.Commands.PracticeResults.PracticeResultUpdate
{
    public class PracticeResultUpdateValidation : AbstractValidator<PracticeResultUpdateCommand>
    {
        public PracticeResultUpdateValidation()
        {
            // Eger Duration null değil ise 0 eşit yada  büyük olmalıdır !

            RuleFor(x => x.Duration)
                .Must(val => val >= 0)
                .When(x => x.Duration != null)
                .WithMessage("Duration must not be negative value");

            RuleFor(x => x.LessonResults)
                .NotNull()
                .NotEmpty()
                .WithMessage("Practice result must contain lesson !");
        }
    }
}
