using Denex.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Denex.Application.Exceptions
{
    internal sealed class UserInsertException : BadRequestException
    {
        public UserInsertException() : base("User Insert Exception")
        {
        }
    }
}
