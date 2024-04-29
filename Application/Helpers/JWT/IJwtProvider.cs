using Domain.Entities;

namespace Application.Helpers.JWT;

public interface IJwtProvider
{
    string GenerateToken(Employee employee);
}