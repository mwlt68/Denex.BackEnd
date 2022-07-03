using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Denex.Application.Features.Commands.Users.UserInsert
{
    public class UserInsertCommandValidator : AbstractValidator<UserInsertCommand>
    {
        public UserInsertCommandValidator()
        {
            // TODO create const class for numeric values
            RuleFor(u => u.Username)
                .NotNull()
                .Length(7, 30)
                .WithErrorCode("Kullanıcı adı 7-30 karakter arasında olmalıdır !");
            RuleFor(u => u.Password)
                .NotNull()
                .Length(7, 30)
                .WithMessage("Parolanız adı 7-30 karakter arasında olmalıdır !")
                .Matches(@"[A-Z]+").WithMessage("Parolanız en az bir büyük harf içermelidir !")
                .Matches(@"[a-z]+").WithMessage("Parolanız en az bir küçük harf içermelidir !")
                .Matches(@"[0-9]+").WithMessage("Parolanız en az bir rakam içermelidir !");
        }
    }
}
