using Application.DTO;
using Application.IRepositories;
using Application.Models;
using Domain.Entities;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class EmployeeRepository: IEmployeeRepository
{
    private readonly PostgresDbContext _ctx;

    public EmployeeRepository(PostgresDbContext ctx)
    {
        _ctx = ctx;
    }


    public async Task<OperationResult> AddAsync(Employee newEmployee)
    {
        await _ctx.Employees.AddAsync(newEmployee);

        var savedCount = await _ctx.SaveChangesAsync();
        
        return savedCount > 0 
            ? new OperationResult{ IsSuccess = true } 
            : new OperationResult{IsSuccess = false, ErrorMessage = "Не удалось сохранить данные. Пожалуйста, перезагрузите страницу и попробуйте снова."};

    }

    public async Task<List<EmployeeDTO>> GetAsync(string name, string phone, bool isDisabled)
    {
        var employees = await _ctx.Employees
                                                .Where(e => (string.IsNullOrEmpty(name) || e.Name == name)
                                                                    && (string.IsNullOrEmpty(phone) || e.PhoneNumber == phone)
                                                                    && e.IsDisabled == isDisabled)
                                                .Select(i => new EmployeeDTO
                                                {
                                                    Id = i.Id,
                                                    PhoneNumber = i.PhoneNumber,
                                                    IsDisabled = i.IsDisabled
                                                })
                                                .ToListAsync();

        return employees;
    }
}