using Domain.Helpers.Enums;
using Infrastructure.Contexts;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebAPI.Helpers;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
public class EmployeeAuthorizeApiAttribute : Attribute, IAuthorizationFilter
{
    public RoleType[]? Roles { get; }

    public EmployeeAuthorizeApiAttribute( RoleType[]? roles = null)
    {
        Roles = roles;
    }
    
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var token = context.HttpContext.Request.Headers["Guitarget"];
        var isAuthenticated = context.HttpContext.User.Identity?.IsAuthenticated ?? false;
        var cookies = context.HttpContext.Request.Cookies;
        
        if (!isAuthenticated)
        {
            context.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized);
            return;
        }

        if (Roles == null) return;

        var employeeRepository = new EmployeeRepository(new PostgresDbContext(new ConfigurationManager()));
        var userId = Convert.ToInt32(context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value);

        var isContains = employeeRepository.CheckEmployeeRoles(userId, Roles.Select(r => (byte)r).ToArray());
        
        if (!isContains)
        {
            context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
        }
    }
}