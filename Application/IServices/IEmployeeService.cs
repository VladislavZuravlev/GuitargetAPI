using Application.DTO;
using Application.Models;
using Application.Models.RequestModels.Employee;
using Domain.Helpers.Enums;

namespace Application.IServices;

public interface IEmployeeService
{
    Task<OperationResult> AddAsync(EmployeeRegisterModel registerModel);
    Task<OperationResult> RegisterAsync(EmployeeRegisterModel registerModel);
    Task<string> LoginAsync(LoginModel model);
    Task<List<EmployeeDTO>> GetAsync(IEnumerable<Tuple<string, string, object>>? filters = null);
    Task<EmployeeDTO?> GetById(int id);
    bool CheckEmployeeRoles(int id, RoleType[] roles);
}