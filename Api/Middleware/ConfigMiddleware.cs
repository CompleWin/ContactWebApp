namespace Api.Middleware;

public class ConfigMiddleware
{
    private readonly RequestDelegate _next;

    public ConfigMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Path == "/config.js")
        {
            string scheme = context.Request.Scheme;
            string host = context.Request.Host.Value;
            string pathBase = context.Request.PathBase.Value;

            string apiUrl = $"{scheme}://{host}{pathBase}/api/ContactManagement";

            var config = $@"window.config = {{
                apiUrl: '{apiUrl}',
            }}";
            
            context.Response.ContentType = "application/javascript";
            await context.Response.WriteAsync(config);
        }
        else
        {
            await _next(context);
        }
    }
}