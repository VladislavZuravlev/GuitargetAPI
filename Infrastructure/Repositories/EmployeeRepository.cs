using System.Linq.Dynamic.Core;
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
    
    public EmployeeRepository(PostgresDbContext ctx): base(ctx)
    {
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
    
   
}