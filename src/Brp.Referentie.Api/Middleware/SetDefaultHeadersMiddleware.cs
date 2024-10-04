namespace Brp.Referentie.Api.Middleware;

public class SetDefaultHeadersMiddleware
{
    private readonly RequestDelegate _next;

    public SetDefaultHeadersMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        if (string.IsNullOrWhiteSpace(context.Request.ContentType))
        {
            context.Request.ContentType = "application/json; charset=utf-8";
        }

        await _next(context);
    }
}
