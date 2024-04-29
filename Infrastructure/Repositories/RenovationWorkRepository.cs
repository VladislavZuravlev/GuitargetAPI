using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DTO;
using Application.IRepositories;
using Application.Models;
using Domain.Entities;
using Infrastructure.Contexts;

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
}