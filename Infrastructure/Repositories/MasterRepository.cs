using Application.DTO;
using Application.IRepositories;
using Application.Models;
using Domain.Entities;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class MasterRepository: IMasterRepository
{
    private readonly PostgresDbContext _ctx;

    public MasterRepository(PostgresDbContext ctx)
    {
        _ctx = ctx;
    }


    public async Task<OperationResult> AddAsync(EmployeeMaster newMaster)
    {
        await _ctx.Masters.AddAsync(newMaster);
        
        var savedCount = await _ctx.SaveChangesAsync();
        return savedCount > 0 
            ? new OperationResult{ IsSuccess = true } 
            : new OperationResult{IsSuccess = false, ErrorMessage = "Не удалось сохранить данные. Пожалуйста, перезагрузите страницу и попробуйте снова."};
            
    }

    public async Task<List<MasterDTO>> GetAsync(string name, string phone, bool isDisabled)
    {
        return await _ctx.Masters
            .Where(m => (string.IsNullOrEmpty(name) || m.Employee.Name == name)
                        && (string.IsNullOrEmpty(phone) || m.Employee.PhoneNumber == phone)
                        && m.IsDisabled == isDisabled)
            .Select(i => new MasterDTO
            {
                EmployeeId = i.EmployeeId,
                Percent = i.Percent,
                IsDisabled = i.IsDisabled,
                RepairsCount = i.Repairs.Count()
            })
            .ToListAsync();
    }
}