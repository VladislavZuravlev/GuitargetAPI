using Application.Models;
using Domain.Entities;

namespace Application.IRepositories;

public interface IEmployeeRepository
{
    Task<OperationResult> AddAsync(Employee newEmployee);
    Task<List<Employee>> GetAsync(IEnumerable<Tuple<string, string, object>>? filters = null, string? includeProperties = null, Dictionary<string, string>? orderCollection = null);
    Task<Employee?> GetByNumberAsync(string phone);
    Task<Employee?> GetByIdAsync(int id);
    bool CheckEmployeeRoles(int id, byte[] roles);
    Employee? GetById(int id);
}