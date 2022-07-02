using Denex.Domain.Exceptions;
using System.Text.Json;

namespace Denex.WebApi.Middlewares
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(context,e);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = ex switch
            {
                BadRequestException => StatusCodes.Status400BadRequest,
                NotFoundException => StatusCodes.Status404NotFound,
                InternalServerException => StatusCodes.Status500InternalServerError,
                _ => StatusCodes.Status500InternalServerError,
            };
            var response = new
            {
                error = ex.Message
            };
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }

    }
}
