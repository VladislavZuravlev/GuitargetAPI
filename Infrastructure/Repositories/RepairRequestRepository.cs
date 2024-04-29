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

public class RepairRequestRepository: BaseRepository<RepairRequest>, IRepairRequestsRepository
{
    public RepairRequestRepository(PostgresDbContext ctx): base(ctx)
    {
    }


    public async Task<OperationResult> AddRepairRequestAsync(RepairRequest newRepairRequest)
    {
        var isSuccess = await base.InsertEntityAsync(newRepairRequest);
        
        return isSuccess
            ? new OperationResult{ IsSuccess = true } 
            : new OperationResult{IsSuccess = false, ErrorMessage = "Не удалось сохранить данные. Пожалуйста, перезагрузите страницу и попробуйте снова."};

    }

    public async Task<List<RepairRequest>> GetRepairRequestsAsync(IEnumerable<Tuple<string, string, object>>? filters = null, string? includeProperties = null, Dictionary<string, string>? orderCollection = null)
    {
        return await base.GetEntityAsync(filters, includeProperties, orderCollection);
    }
}