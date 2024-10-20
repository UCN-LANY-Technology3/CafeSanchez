namespace CafeSanchez.API.Middleware;

internal class KeyAuthorizationMiddleware(RequestDelegate next)
{
    private const string KEY_HEADER_NAME = "Client-Authorization-Key";
    
    private readonly RequestDelegate _next = next;

    public async Task Invoke(HttpContext context)
    {
        // First, check if the http header is provided
        if (!context.Request.Headers.TryGetValue(KEY_HEADER_NAME, out var providedAuthorizationKey))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("No authorization header");
            return;
        }

        // Next, check if keys are defined in configuration (appsettings.json)
        var configuration = context.RequestServices.GetService<IConfiguration>();
        string[]? authorizedKeys = configuration?.GetSection("ClientAuthorizationKeys").Get<string[]>();

        if(configuration == null || authorizedKeys == null)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            return;
        }

        // Last, check if the provided key is allowed
        if (!authorizedKeys.Any(k => k.Equals(providedAuthorizationKey, StringComparison.InvariantCultureIgnoreCase)))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Invalid authorization key");
            return;
        }

        await _next(context);
    }
}
