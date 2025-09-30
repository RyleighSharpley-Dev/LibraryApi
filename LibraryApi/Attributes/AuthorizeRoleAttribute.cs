using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
public class AuthorizeRoleAttribute : Attribute, IAsyncActionFilter
{
    private readonly string _role;

    public AuthorizeRoleAttribute(string role)
    {
        _role = role;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var httpContext = context.HttpContext;

        if (_role.Equals("Admin", StringComparison.OrdinalIgnoreCase))
        {
            if (!httpContext.Items.ContainsKey("Admin"))
            {
                context.Result = new ContentResult
                {
                    StatusCode = 403,
                    Content = "Forbidden: Admins only"
                };
                return;
            }
        }
        else if (_role.Equals("Member", StringComparison.OrdinalIgnoreCase))
        {
            if (!httpContext.Items.ContainsKey("Member"))
            {
                context.Result = new ContentResult
                {
                    StatusCode = 403,
                    Content = "Forbidden: Members only"
                };
                return;
            }
        }

        await next();
    }
}
