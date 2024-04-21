using Application.DTO;
using Application.IRepositories;
using Application.Models;
using Domain.Entities;
using Infrastructure.Contexts;
using Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class MasterRepository: BaseRepository<Master>, IMasterRepository
{
    public MasterRepository(PostgresDbContext ctx): base(ctx)
    {
    }


    public async Task<OperationResult> AddAsync(Master newMaster)
    {
        var isSuccess = await base.InsertEntityAsync(newMaster);
        
        return isSuccess
            ? new OperationResult{ IsSuccess = true } 
            : new OperationResult{IsSuccess = false, ErrorMessage = "Не удалось сохранить данные. Пожалуйста, перезагрузите страницу и попробуйте снова."};
   
    }

    public async Task<List<MasterDTO>> GetAsync(IEnumerable<Tuple<string, string, object>>? filters = null, string? includeProperties = null, Dictionary<string, string>? orderCollection = null)
    {
        var masters = await base.GetEntityAsync(filters, includeProperties, orderCollection);
        
        return masters
            .Select(i => new MasterDTO
            {
                EmployeeId = i.EmployeeId,
                Percent = i.Percent,
                IsDisabled = i.IsDisabled,
                RepairsCount = i.Repairs.Count,
                Employee = new EmployeeDTO
                {
                    Id = i.Employee.Id,
                    PhoneNumber = i.Employee.PhoneNumber,
                    Name = i.Employee.Name,
                    IsDisabled = i.Employee.IsDisabled
                },
                Repairs = i.Repairs.Select(r => new RepairRequestDTO
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
                    Client = new ClientDTO
                    {
                        Id = r.Client.Id,
                        PhoneNumber = r.Client.PhoneNumber,
                        Name = r.Client.Name,
                        CreateDateTime = r.Client.CreateDateTime
                    },
                    Employee = new EmployeeDTO
                    {
                        Id = r.Employee.Id,
                        PhoneNumber = r.Employee.PhoneNumber,
                        Name = r.Employee.Name,
                        IsDisabled = r.Employee.IsDisabled
                    },
                    RenovationWorks = r.RenovationWorks.Select(rw => new RenovationWorkDTO
                    {
                        Id = rw.Id,
                        Name = rw.Name,
                        Description = rw.Description,
                        Price = rw.Price,
                        IsDeleted = rw.IsDeleted
                    }).ToList()
                })
            })
            .ToList();
    }
}