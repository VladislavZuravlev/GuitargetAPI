using Application.DTO;
using Application.IRepositories;
using Application.Models;
using Domain.Entities;
using Infrastructure.Contexts;
using Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ClientRepository: BaseRepository<Client>, IClientRepository
{
    public ClientRepository(PostgresDbContext ctx) : base(ctx)
    {
    }
    
    public async Task<OperationResult> AddAsync(Client newClient)
    {
        var isSuccess = await base.InsertEntityAsync(newClient);
        
        return isSuccess
            ? new OperationResult{ IsSuccess = true } 
            : new OperationResult{IsSuccess = false, ErrorMessage = "Не удалось сохранить данные. Пожалуйста, перезагрузите страницу и попробуйте снова."};

    }

    public async Task<List<ClientDTO>> GetAsync(IEnumerable<Tuple<string, string, object>>? filters = null, string? includeProperties = null, Dictionary<string, string>? orderCollection = null)
    {
        var clients = await base.GetEntityAsync(filters, includeProperties, orderCollection);

        return clients
            .Select(i => new ClientDTO
            {
                Id = i.Id,
                PhoneNumber = i.PhoneNumber,
                Name = i.Name,
                CreateDateTime = i.CreateDateTime,
                Repairs = i.Repairs?.Select(r => new RepairDTO
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
                    MasterId = r.MasterId,
                    EmployeeId = r.EmployeeId,
                    RenovationWorkId = r.RenovationWorkId,
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
                    Employee = new EmployeeDTO
                    {
                        Id = r.Employee.Id,
                        PhoneNumber = r.Employee.PhoneNumber,
                        Name = r.Employee.Name,
                        IsDisabled = r.Employee.IsDisabled
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
            }).ToList();
    }
}