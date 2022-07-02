using Denex.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Denex.Application.Exceptions
{
    public sealed class LessonSchemaNotFound : NotFoundException
    {
        public LessonSchemaNotFound() : base("Lesson Schema Not Found !")
        {
        }
    }
}
