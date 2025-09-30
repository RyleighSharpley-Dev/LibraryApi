using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

public class ApiKeyMiddleware
{
    private readonly RequestDelegate _next;
    private const string API_KEY_HEADER = "X-API-KEY";

    public ApiKeyMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ApplicationDbContext db)
    {
        if (!context.Request.Headers.TryGetValue(API_KEY_HEADER, out var extractedApiKey))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("API Key missing");
            return;
        }

        var apiKey = extractedApiKey.ToString();

        // Check both Admin and Member tables
        var admin = await db.Admins.FirstOrDefaultAsync(a => a.ApiKey == apiKey);
        if (admin != null)
        {
            context.Items["Admin"] = admin;
            await _next(context);
            return;
        }

        var member = await db.Members.FirstOrDefaultAsync(m => m.ApiKey == apiKey);
        if (member != null)
        {
            context.Items["Member"] = member;
            await _next(context);
            return;
        }

        context.Response.StatusCode = 403;
        await context.Response.WriteAsync("Invalid API Key");
    }
}
