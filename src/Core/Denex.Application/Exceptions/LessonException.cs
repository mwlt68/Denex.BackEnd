using Denex.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Denex.Application.Exceptions
{
    public sealed class LessonException : InternalServerException
    {
        public LessonException(string message) : base(message ?? "Lesson error")
        {

        }
    }
}
