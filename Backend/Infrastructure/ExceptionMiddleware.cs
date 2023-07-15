using Brewery_SCADA_System.Models;
using System.Net;

namespace Brewery_SCADA_System.Infrastructure
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly bool _isDevelopment;
        public ExceptionMiddleware(RequestDelegate next, bool isDevelopment)
        {
            _next = next;
            _isDevelopment = isDevelopment;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception exception)
            {
                if (httpContext.RequestAborted.IsCancellationRequested) return;


                ExceptionResponse exceptionResponse = new(exception);
                if (_isDevelopment)
                    exceptionResponse.StackTrace = exception.StackTrace;

                HttpStatusCode statusCode = exception.ExceptionToStatusCode();
                await Utils.WriteJsonToHttpResponseAsync(httpContext.Response, statusCode, exceptionResponse);
            }

        }
    }
}
