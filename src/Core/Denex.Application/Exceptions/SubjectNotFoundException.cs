using Denex.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Denex.Application.Exceptions
{
    public class SubjectNotFoundException : NotFoundException
    {
        public SubjectNotFoundException(string? message = "Subject Not Found") : base(message)
        {

        }
    }
}
