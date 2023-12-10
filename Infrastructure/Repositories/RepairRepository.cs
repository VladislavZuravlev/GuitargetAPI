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
        var isSuccess = await base.InsertEntityAsync(newRepair);
        
        return isSuccess
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
                Client = r.Client != null ? new ClientDTO
                {
                    Id = r.Client.Id,
                    PhoneNumber = r.Client.PhoneNumber,
                    Name = r.Client.Name,
                    CreateDateTime = r.Client.CreateDateTime
                } : null,
                Master = r.Master != null ? new MasterDTO
                {
                    EmployeeId = r.Master.EmployeeId,
                    Employee = r.Master.Employee != null ? new EmployeeDTO
                    {
                        Id = r.Master.Employee.Id,
                        PhoneNumber = r.Master.Employee.PhoneNumber,
                        Name = r.Master.Employee.Name,
                        IsDisabled = r.Master.Employee.IsDisabled
                    } : null,
                    Percent = r.Master.Percent,
                    IsDisabled = r.Master.IsDisabled
                } : null,
                RenovationWork = r.RenovationWork != null ? new RenovationWorkDTO
                {
                    Id = r.RenovationWork.Id,
                    Name = r.RenovationWork.Name,
                    Description = r.RenovationWork.Description,
                    Price = r.RenovationWork.Price,
                    IsDeleted = r.RenovationWork.IsDeleted
                } : null,
                Employee = r.Employee != null ? new EmployeeDTO
                {
                    Id = r.Employee.Id,
                    PhoneNumber = r.Employee.PhoneNumber,
                    Name = r.Employee.Name,
                    IsDisabled = r.Employee.IsDisabled
                } : null
            })
            .ToList();
    }
}