using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Denex.Application.Features.Commands.PracticeSchemas.PracticeSchemaUpdate
{
    public class PracticeSchemaUpdateValidaton: AbstractValidator<PracticeSchemaUpdateCommand>
    {
        public PracticeSchemaUpdateValidaton()
        {
            // Eger NetCalculationRate null değil ise 0 dan büyük olmalıdır !

            RuleFor(x => x.NetCalculationRate)
                .Must(val => val > 0)
                .When(x => x.NetCalculationRate != null)
                .WithMessage("Net calculation rate must not be negative and zero !");

            // Eger Duration null değil ise 0 eşit yada  büyük olmalıdır !

            RuleFor(x => x.Duration)
                .Must(val => val >= 0)
                .When(x => x.Duration != null)
                .WithMessage("Duration must not be negative value !");

            RuleFor(x => x.Name).NotNull();

        }
    }
}
