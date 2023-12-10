using Application.DTO;
using Application.IRepositories;
using Application.Models;
using Domain.Entities;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class RepairRepository: BaseRepository<Repair>, IRepairsRepository
{
    public RepairRepository(PostgresDbContext ctx): base(ctx)
    {
    }


    public async Task<OperationResult> AddRepairAsync(Repair newRepair)
    {
        await _ctx.Repairs.AddAsync(newRepair);
        
        var savedCount = await _ctx.SaveChangesAsync();
        return savedCount > 0 
            ? new OperationResult{ IsSuccess = true } 
            : new OperationResult{IsSuccess = false, ErrorMessage = "Не удалось сохранить данные. Пожалуйста, перезагрузите страницу и попробуйте снова."};

    }

    public async Task<List<RepairDTO>> GetRepairsAsync(IEnumerable<Tuple<string, string, object>>? filters = null, string? includeProperties = null, Dictionary<string, string>? orderCollection = null)
    {
        var repairs = await base.GetEntityAsync(filters, includeProperties, orderCollection);
        
        
        return repairs.Select(r => new RepairDTO
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
                },
                Employee = new EmployeeDTO
                {
                    Id = r.Employee.Id,
                    PhoneNumber = r.Employee.PhoneNumber,
                    Name = r.Employee.Name,
                    IsDisabled = r.Employee.IsDisabled
                }
            })
            .ToList();
    }
}