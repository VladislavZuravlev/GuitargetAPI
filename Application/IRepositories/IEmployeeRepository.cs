using Application.DTO;
using Application.Models;
using Domain.Entities;

namespace Application.IRepositories;

public interface IEmployeeRepository
{
    Task<OperationResult> AddAsync(Employee newEmployee);
    Task<List<EmployeeDTO>> GetAsync(string name, string phone, bool isDisabled);
}