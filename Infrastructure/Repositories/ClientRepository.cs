using Application.DTO;
using Application.IRepositories;
using Application.Models;
using Domain.Entities;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ClientRepository: IClientRepository
{
    private readonly PostgresDbContext _ctx;

    public ClientRepository(PostgresDbContext postgresDbContext)
    {
        _ctx = postgresDbContext;
    }
    
    
    public async Task<OperationResult> AddAsync(Client newClient)
    {
        await _ctx.Clients.AddAsync(newClient);

        var savedCount = await _ctx.SaveChangesAsync();
        
        
        return savedCount > 0 
            ? new OperationResult{ IsSuccess = true } 
            : new OperationResult{IsSuccess = false, ErrorMessage = "Не удалось сохранить данные. Пожалуйста, перезагрузите страницу и попробуйте снова."};
        
    }

    public async Task<List<ClientDTO>> GetAsync(string name, string phone, DateTime? periodStartDate, DateTime? periodEndDate)
    {
        return await _ctx.Clients.Include(c => c.Repairs)
                                                .Where(c => (string.IsNullOrEmpty(name) || c.Name.Contains(name))
                                                    && (string.IsNullOrEmpty(phone) || c.PhoneNumber == phone)
                                                    && (periodStartDate == null || c.CreateDateTime.Date >= periodStartDate.Value.Date)
                                                    && (periodEndDate == null || c.CreateDateTime.Date <= periodEndDate.Value.Date))
                                                .Select(i => new ClientDTO
                                                {
                                                    Id = i.Id,
                                                    PhoneNumber = i.PhoneNumber,
                                                    Name = i.Name,
                                                    CreateDateTime = i.CreateDateTime,
                                                    Repairs = i.Repairs.Select(s => new RepairDTO
                                                    {
                                                        Id = s.Id,
                                                        CreateDateTime = s.CreateDateTime,
                                                        ProvisionalDateOfReceipt = s.ProvisionalDateOfReceipt,
                                                        DateOfReceipt = s.DateOfReceipt,
                                                        InstrumentName = s.InstrumentName,
                                                        IsCase = s.IsCase,
                                                        Description = s.Description,
                                                        Price = s.Price,
                                                        StatusId = s.StatusId,
                                                        IsDeleted = s.IsDeleted,
                                                        MasterId = s.MasterId,
                                                        EmployeeId = s.EmployeeId,
                                                        RenovationWorkId = s.RenovationWorkId
                                                    }).ToList()
                                                })
                                                .ToListAsync();
    }
}