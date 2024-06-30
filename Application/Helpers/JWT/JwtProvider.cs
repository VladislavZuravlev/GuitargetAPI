using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Application.Helpers.JWT;

public class JwtProvider: IJwtProvider
{
    private readonly JwtOptions _options;

    public JwtProvider(IOptions<JwtOptions> options)
    {
        _options = options.Value;
    }

    public string GenerateToken(Employee employee)
    {
        var rolesArr = employee.Roles.Select(r => $"{r.RoleId},");
        var roleIdsStr = string.Empty;
        if (rolesArr != null && rolesArr.Any())
        {
            roleIdsStr = String.Concat(rolesArr);
        }
        
        Claim[] claims = new[]
        {
            new Claim("userId", employee.Id.ToString()),
            new Claim("roleIds", roleIdsStr)
        };

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)), 
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            signingCredentials: signingCredentials, 
            expires: DateTime.Now.AddHours(_options.ExpiresHours)
            );

        var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
        return tokenValue;
    }
    
}