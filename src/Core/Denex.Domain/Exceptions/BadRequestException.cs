namespace Denex.Domain.Exceptions
{
    // 400
    public abstract class BadRequestException : Exception
    {
        protected BadRequestException(string message) : base(message)
        {

        }
    }
}
