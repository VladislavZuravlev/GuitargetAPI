using Application.Models;
using Domain.Entities;

namespace Application.IRepositories;

public interface IEmployeeRepository
{
    Task<OperationResult> AddAsync(Employee newEmployee);
    Task<List<Employee>> GetAsync(IEnumerable<Tuple<string, string, object>>? filters = null, string? includeProperties = null, Dictionary<string, string>? orderCollection = null);
    Task<Employee?> GetByNumber(string phone);
    Task<Employee?> GetById(int id);
}