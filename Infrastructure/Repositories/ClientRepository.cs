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

    public async Task<List<Client>> GetAsync(IEnumerable<Tuple<string, string, object>>? filters = null, string? includeProperties = null, Dictionary<string, string>? orderCollection = null)
    {
        return await base.GetEntityAsync(filters, includeProperties, orderCollection);
    }
}