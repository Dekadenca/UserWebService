using UserManagerApp.Interfaces;

namespace UserManagerApp.Authentication
{
    // Middleware for api authorization with Api key.
    public class ApiKeyAuthMiddleware
    {
        private readonly RequestDelegate _next;

        public ApiKeyAuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IApiKeyRepository apiKeyRepository)
        {
            // Check header for Api key.
            if (!context.Request.Headers.TryGetValue(AuthConstants.API_KEY_HEADER_NAME, out var extractedApiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("API Key missing");
                return;
            }

            // Validate Api key
            var keyValidated = apiKeyRepository.ValidateApiKey(extractedApiKey);
            if (!keyValidated)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Invalid API Key");
                return;
            }

            await _next(context);
        }

    }
}
