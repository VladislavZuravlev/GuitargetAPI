using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DTO;
using Application.IRepositories;
using Application.Models;
using Domain.Entities;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class RenovationWorkRepository: BaseRepository<RenovationWork>, IRenovationWorkRepository
{
    public RenovationWorkRepository(PostgresDbContext ctx): base(ctx)
    {
    }
    
    public async Task<OperationResult> AddAsync(RenovationWork newRenovationWork)
    {
        var isSuccess = await base.InsertEntityAsync(newRenovationWork);
        
        return isSuccess
            ? new OperationResult{ IsSuccess = true } 
            : new OperationResult{IsSuccess = false, ErrorMessage = "Не удалось сохранить данные. Пожалуйста, перезагрузите страницу и попробуйте снова."};

    }

    public async Task<List<RenovationWork>> GetAsync(IEnumerable<Tuple<string, string, object>>? filters = null, string? includeProperties = null, Dictionary<string, string>? orderCollection = null)
    {
        return await base.GetEntityAsync(filters, includeProperties, orderCollection);
    }

    public async Task<OperationResult> RemoveRestoreAsync(int id, bool isRemove)
    {
        var service = await _ctx.RenovationWorks.FirstOrDefaultAsync(s => s.Id == id);

        if (service == null) return new OperationResult { ErrorMessage = "Услуга не найдена" };

        service.IsDeleted = isRemove;

        var savedCount = await _ctx.SaveChangesAsync();

        return savedCount > 0
            ? new OperationResult { IsSuccess = true }
            : new OperationResult
            {
                IsSuccess = false,
                ErrorMessage = "Не удалось сохранить данные. Пожалуйста, перезагрузите страницу и попробуйте снова."
            };
    }
}