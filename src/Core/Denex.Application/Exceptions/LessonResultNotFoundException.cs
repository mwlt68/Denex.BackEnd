using Denex.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Denex.Application.Exceptions
{
    public class LessonResultNotFoundException : NotFoundException
    {
        public LessonResultNotFoundException(string? message = "Lesson Result Not Found !") : base(message)
        {

        }
    }
}
