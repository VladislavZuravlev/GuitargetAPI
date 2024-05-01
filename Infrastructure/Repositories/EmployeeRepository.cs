using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Application.DTO;
using Application.IRepositories;
using Application.Models;
using Domain.Entities;
using Infrastructure.Contexts;
using Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class EmployeeRepository: BaseRepository<Employee>, IEmployeeRepository
{
    private static string[] stringOperators = new[] { "Contains", "StartsWith", "EndsWith" };
    private static string[] dateOperators = new[] { "OnDate", "NotOnDate", "AfterDate", "BeforeDate" };
    private static string[] nullOperators = new[] { "IsNull", "IsNotNull" };

    private readonly PostgresDbContext _ctx;
    
    public EmployeeRepository(PostgresDbContext ctx): base(ctx)
    {
        _ctx = ctx;
    }


    public async Task<OperationResult> AddAsync(Employee newEmployee)
    {
        var isSuccess = await base.InsertEntityAsync(newEmployee);
        
        return isSuccess
            ? new OperationResult{ IsSuccess = true } 
            : new OperationResult{IsSuccess = false, ErrorMessage = "Не удалось сохранить данные. Пожалуйста, перезагрузите страницу и попробуйте снова."};
    }

    public async Task<List<Employee>> GetAsync(IEnumerable<Tuple<string, string, object>>? filters = null, string? includeProperties = null, Dictionary<string, string>? orderCollection = null)
    {
        return await base.GetEntityAsync(filters, includeProperties, orderCollection);
    }

    public async Task<Employee?> GetByNumberAsync(string phone) =>
        await _ctx.Employees.FirstOrDefaultAsync(e => e.PhoneNumber == phone);

    public async Task<Employee?> GetByIdAsync(int id) => 
        await _ctx.Employees.Include(e => e.Roles).FirstOrDefaultAsync(e => e.Id == id);

    public Employee? GetById(int id) => 
         _ctx.Employees.Include(e => e.Roles).FirstOrDefault(e => e.Id == id);
    
    public bool CheckEmployeeRoles(int id, byte[] roles)
    {
        var employee = _ctx.Employees.Include(e => e.Roles).FirstOrDefault(e => e.Id == id);
        
        if (employee == null || employee.IsDisabled || employee.Roles.Count == 0) return false;

        return employee.Roles.Any(r => roles.Any(i => i == r.RoleId));
    }

}