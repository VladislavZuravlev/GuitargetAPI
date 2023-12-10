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

    public async Task<List<EmployeeDTO>> GetAsync(IEnumerable<Tuple<string, string, object>>? filters = null, string? includeProperties = null, Dictionary<string, string>? orderCollection = null)
    {
        var employees = await base.GetEntityAsync(filters, includeProperties, orderCollection);

        return employees
            .Select(i => new EmployeeDTO
            {
                Id = i.Id,
                PhoneNumber = i.PhoneNumber,
                Name = i.Name,
                IsDisabled = i.IsDisabled,
                Repairs = i.Repairs.Select(r => new RepairDTO
                {
                    Id = r.Id,
                    CreateDateTime = r.CreateDateTime,
                    ProvisionalDateOfReceipt = r.ProvisionalDateOfReceipt,
                    DateOfReceipt = r.DateOfReceipt,
                    InstrumentName = r.InstrumentName,
                    IsCase = r.IsCase,
                    Description = r.Description,
                    Price = r.Price,
                    StatusId = r.StatusId,
                    IsDeleted = r.IsDeleted,
                    ClientId = r.ClientId,
                    MasterId = r.MasterId,
                    EmployeeId = r.EmployeeId,
                    RenovationWorkId = r.RenovationWorkId,
                    Client = new ClientDTO
                    {
                        Id = r.Client.Id,
                        PhoneNumber = r.Client.PhoneNumber,
                        Name = r.Client.Name,
                        CreateDateTime = r.Client.CreateDateTime
                    },
                    Master = new MasterDTO
                    {
                        EmployeeId = r.Master.EmployeeId,
                        Employee = new EmployeeDTO
                        {
                            Id = r.Employee.Id,
                            PhoneNumber = r.Employee.PhoneNumber,
                            Name = r.Employee.Name,
                            IsDisabled = r.Employee.IsDisabled
                        },
                        Percent = r.Master.Percent,
                        IsDisabled = r.Master.IsDisabled
                    },
                    RenovationWork = new RenovationWorkDTO
                    {
                        Id = r.RenovationWork.Id,
                        Name = r.RenovationWork.Name,
                        Description = r.RenovationWork.Description,
                        Price = r.RenovationWork.Price,
                        IsDeleted = r.RenovationWork.IsDeleted
                    }
                })
            })
            .ToList();
    }
    
   
}