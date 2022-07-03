using Denex.Application.Features.Commands.Users.UserInsert;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Denex.Persistance.Examples
{
    public class UserInsertCommandExample : IExamplesProvider<UserInsertCommand>                                
    {
        public UserInsertCommand GetExamples()
        {
            return new UserInsertCommand()
            {
                Username = "username",
                Password = "Password123",
            };
        }
    }
}
