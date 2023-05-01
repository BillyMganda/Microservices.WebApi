using Newtonsoft.Json;
using System.Net;

namespace User.Microservice.Exceptions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exeption)
        {
            context.Response.ContentType = "application/json";
            HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;
            string result = JsonConvert.SerializeObject(new ErrorDetails { ErrorMessage = exeption.Message, ErrorType = "Failure" });

            switch(exeption)
            {
                case NotFoundException notFoundException:
                    httpStatusCode = HttpStatusCode.NotFound;
                    break;
                case UserExistsException userExistsException:
                    httpStatusCode = HttpStatusCode.BadRequest;
                    break;
                case InvalidCredentialsException invalidCredentialsException:
                    httpStatusCode = HttpStatusCode.BadRequest;
                    break;
                default:
                    break;
            }

            context.Response.StatusCode = (int)httpStatusCode;
            return context.Response.WriteAsync(result);
        }
    }
}
