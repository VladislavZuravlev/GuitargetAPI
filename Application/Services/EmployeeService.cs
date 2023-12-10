using Application.DTO;
using Application.IRepositories;
using Application.IServices;
using Application.Models;
using Application.Models.RequestModels.Employee;
using Domain.Entities;

namespace Application.Services;

public class EmployeeService: IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeeService(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }
    
    
    
    
    public async Task<OperationResult> AddAsync(AddEmployeeModel model)
    {
        Employee newEmployee;
        try
        {
            newEmployee = Employee.Create(model.Name, model.PhoneNumber);
        }
        catch (Exception e)
        {
            return new OperationResult
                { IsSuccess = false, ErrorMessage = $"Не удалось создать сотрудника. Ошибка: {e.Message}." };
        }

        return await _employeeRepository.AddAsync(newEmployee);
    }

    public async Task<List<EmployeeDTO>> GetAsync(IEnumerable<Tuple<string, string, object>>? filters = null)
    {
        return await _employeeRepository.GetAsync(filters);
    }
}