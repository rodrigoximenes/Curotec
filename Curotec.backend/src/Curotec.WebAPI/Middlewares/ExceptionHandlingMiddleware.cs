using Newtonsoft.Json;
using System.Net;

namespace Curotec.WebAPI.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context); 
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var statusCode = HttpStatusCode.InternalServerError;
            var response = new { message = "An unexpected error occurred", details = exception.Message };

            if (exception is KeyNotFoundException)
            {
                statusCode = HttpStatusCode.NotFound;
                response = new { message = "Resource not found", details = exception.Message };
            }
            else if (exception is ArgumentException)
            {
                statusCode = HttpStatusCode.BadRequest;
                response = new { message = "Invalid request", details = exception.Message };
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }
    }
}