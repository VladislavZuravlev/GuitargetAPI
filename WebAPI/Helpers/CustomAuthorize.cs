

using Microsoft.AspNetCore.Mvc.Filters;

namespace WebAPI.Helpers;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
public class CustomAuthorizationFilter : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var userId = Convert.ToInt32(context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value);
        var isAuthenticated = context.HttpContext.User.Identity?.IsAuthenticated ?? false;
        
        var te = "";
    }
}