using Denex.Domain.Exceptions;

namespace Denex.Application.Exceptions
{
    public class LessonSchemaEmptyException : InternalServerException
    {
        public LessonSchemaEmptyException(string? message = "Lesson Schema Empty") : base(message)
        {

        }

    }
}
