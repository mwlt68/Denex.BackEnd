using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Denex.Application.Features.Commands.PracticeResults.PracticeResultInsert
{
    public class PracticeResultInsertCommandValidation : AbstractValidator<PracticeResultInsertCommand>
    {
        public PracticeResultInsertCommandValidation()
        {

            /*
            RuleForEach(x => x.Lessons).ChildRules(y =>
            {
                y.RuleFor(x => x.QuestionCount)
                .GreaterThan(0)
                .WithMessage("Soru sayısı sıfırdan büyük olmalı !");
                y.RuleFor(x => x.Subjects)
                .NotNull()
                .NotEmpty()
                .WithMessage("Ders mutlaka konu içermelidir !");
            });
            */
        }
    }
}
