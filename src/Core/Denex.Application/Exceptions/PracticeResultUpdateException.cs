using Denex.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Denex.Application.Exceptions
{
    public class PracticeResultUpdateException : InternalServerException
    {
        public PracticeResultUpdateException(string message = "Practice Result Update !") : base(message)
        {
        }
    }
}
