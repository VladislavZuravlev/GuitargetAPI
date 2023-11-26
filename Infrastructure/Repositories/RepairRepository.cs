using Application.DTO;
using Application.IRepositories;
using Application.Models;
using Domain.Entities;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class RepairRepository: IRepairsRepository
{
    private readonly PostgresDbContext _ctx;

    public RepairRepository(PostgresDbContext ctx)
    {
        _ctx = ctx;
    }


    public async Task<OperationResult> AddRepairAsync(Repair newRepair)
    {
        await _ctx.Repairs.AddAsync(newRepair);
        
        var savedCount = await _ctx.SaveChangesAsync();
        return savedCount > 0 
            ? new OperationResult{ IsSuccess = true } 
            : new OperationResult{IsSuccess = false, ErrorMessage = "Не удалось сохранить данные. Пожалуйста, перезагрузите страницу и попробуйте снова."};

    }

    public async Task<List<RepairDTO>> GetRepairsAsync(DateTime? provisionalDateOfReceipt, string? instrumentName, bool isCase, string? description,
        decimal? price, string? clientPhone, int? masterId, int? employeeId, int? renovationWorkId)
    {
        return await _ctx.Repairs.Where(r =>
                (provisionalDateOfReceipt == null || r.ProvisionalDateOfReceipt == provisionalDateOfReceipt)
                && (string.IsNullOrEmpty(instrumentName) || r.InstrumentName.Contains(instrumentName))
                && (string.IsNullOrEmpty(description) || r.Description.Contains(description))
                && (price == null || r.Price == price)
                && (string.IsNullOrEmpty(clientPhone) || r.Client.PhoneNumber == clientPhone)
                && (masterId == null || r.MasterId == masterId)
                && (employeeId == null || r.EmployeeId == employeeId))
            .Select(i => new RepairDTO
            {
                Id = i.Id,
                CreateDateTime = i.CreateDateTime,
                ProvisionalDateOfReceipt = i.ProvisionalDateOfReceipt,
                DateOfReceipt = i.DateOfReceipt,
                InstrumentName = i.InstrumentName,
                IsCase = i.IsCase,
                Description = i.Description,
                Price = i.Price,
                StatusId = i.StatusId,
                IsDeleted = i.IsDeleted,
                ClientId = i.ClientId,
                MasterId = i.MasterId,
                EmployeeId = i.EmployeeId,
                RenovationWorkId = i.RenovationWorkId,
                ClientName = i.Client.Name,
                ClientPhone = i.Client.PhoneNumber,
                MasterName = i.Master.Employee.Name,
                EmployeeName = i.Employee.Name
            })
            .ToListAsync();
    }

    public async Task<OperationResult> AddRenovationWorkAsync(RenovationWork newRenovationWork)
    {
        await _ctx.RenovationWorks.AddAsync(newRenovationWork);
        
        var savedCount = await _ctx.SaveChangesAsync();
        return savedCount > 0 
            ? new OperationResult{ IsSuccess = true } 
            : new OperationResult{IsSuccess = false, ErrorMessage = "Не удалось сохранить данные. Пожалуйста, перезагрузите страницу и попробуйте снова."};

    }

    public async Task<List<RenovationWorkDTO>> GetRenovationWorksAsync(string? name, string? description, decimal? price)
    {
        return await _ctx.RenovationWorks.Where(rw =>
                (string.IsNullOrEmpty(name) || rw.Name.Contains(name))
                && (string.IsNullOrEmpty(description) || rw.Description.Contains(description)
                    && (price == null || rw.Price == price)))
            .Select(i => new RenovationWorkDTO
            {
                Id = i.Id,
                Name = i.Name,
                Description = i.Description,
                Price = i.Price,
                IsDeleted = i.IsDeleted
            })
            .ToListAsync();
    }
}