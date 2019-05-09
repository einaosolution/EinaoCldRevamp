using Microsoft.AspNetCore.Http;
using System.Net;
using System.Threading.Tasks;

public class CookieChecker
{
    private readonly RequestDelegate _next;

    public CookieChecker(RequestDelegate next)
    {
        _next = next;
    }

    public bool IsAjaxRequest(HttpContext context)
    {
        return context.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
    }

    public async Task Invoke(HttpContext context)
    {
        var name = "access_token";
        var cookie = context.Request.Cookies[name];

        if (IsAjaxRequest(context))
        {
            if (!context.Request.Headers.ContainsKey("Authorization"))
                context.Request.Headers.Append("Authorization", "Bearer " + cookie);
        }
                    
        await _next.Invoke(context);
    }
}