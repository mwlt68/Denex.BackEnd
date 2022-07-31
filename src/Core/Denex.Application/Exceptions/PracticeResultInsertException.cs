using Denex.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Denex.Application.Exceptions
{
    public class PracticeResultInsertException : BadRequestException
    {
        public PracticeResultInsertException(string message= "Practice result insert exception !") : base(message)
        {
        }
    }
}
