using Application.DTO;
using Application.Models;
using Application.Models.RequestModels.Employee;

namespace Application.IServices;

public interface IEmployeeService
{
    Task<OperationResult> AddAsync(AddEmployeeModel model);
    Task<List<EmployeeDTO>> GetAsync(IEnumerable<Tuple<string, string, object>>? filters = null);
}