using Serilog.Context;
using UserManagerApp.Helpers;
using UserManagerApp.Interfaces;

namespace UserManagerApp.Logger
{
    public class LoggerRequestMiddleware
    {
        private readonly RequestDelegate _next;

        public LoggerRequestMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IApiKeyRepository apiKeyRepository)
        {

            // Get ip adress from HttpContext
            var ipAddress = context.Connection.RemoteIpAddress;
            if (ipAddress != null)
            {
                // If ip exists, ust GetComputerName for computer name fetching
                LogContext.PushProperty("ClientName", Http.GetComputerName(ipAddress.ToString()));
            }

            // Add request query parameters to LogContext
            LogContext.PushProperty("RequestQuery", context.Request.Query);

            // Add RemoteIpAddress to LogContext and release resources
            using (LogContext.PushProperty("RemoteIpAddress", ipAddress))
            {
                await _next(context);
            }
        }
    }
}
