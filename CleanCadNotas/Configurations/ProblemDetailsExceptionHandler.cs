using Azure.Core;
using Microsoft.AspNetCore.Diagnostics;

namespace CleanCadNotas.Configurations
{
    public class ProblemDetailsExceptionHandler(ILogger<ProblemDetailsExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            logger.LogError(exception, "Unhandled exception. RequestId: {RequestId}", httpContext.TraceIdentifier);

            string action;
            int statusCode;

            switch (exception)
            {
                //case NotFoundException:
                //    statusCode = StatusCodes.Status404NotFound;
                //    action = "NotFound";
                //    break;

                case UnauthorizedAccessException:
                    statusCode = StatusCodes.Status401Unauthorized;
                    action = "Unauthorized";
                    break;

                //case ForbiddenAccessException:
                //    statusCode = StatusCodes.Status403Forbidden;
                //    action = "Forbidden";
                //    break;

                default:
                    statusCode = StatusCodes.Status500InternalServerError;
                    action = "Error";
                    break;
            }

            httpContext.Response.StatusCode = statusCode;
            var requestId = httpContext.TraceIdentifier;

            var wantsJson = httpContext.Request.Headers.Accept.Any(x => x?.Contains("application/json") == true);
            if (wantsJson)
            {
                await httpContext.Response.WriteAsJsonAsync(new
                {
                    Type = statusCode == StatusCodes.Status401Unauthorized ? "Unauthorized" : "Unexpected",
                    Message = statusCode == StatusCodes.Status401Unauthorized ? "Unauthorized access." : "An unexpected error occurred.",
                    RequestId = requestId
                });

                return true;
            }

            var url =
            $"/Home/{action}" +
            $"?statusCode={statusCode}" +
            $"&requestId={Uri.EscapeDataString(requestId)}";

            httpContext.Response.Redirect(url);

            return true;

        }
    }
}
