using Denex.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Denex.Application.Exceptions
{
    public class PracticeResultNotFoundException :NotFoundException
    {
        public PracticeResultNotFoundException(string? message = "Practice Result Not Found !") : base(message)
        {

        }
    }
}
