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

    public async Task<List<RenovationWorkDTO>> GetAsync(IEnumerable<Tuple<string, string, object>>? filters = null, string? includeProperties = null, Dictionary<string, string>? orderCollection = null)
    {
        var renovationWorks = await base.GetEntityAsync(filters, includeProperties, orderCollection);

        return renovationWorks.Select(i => new RenovationWorkDTO
        {
            Id = i.Id,
            Name = i.Name,
            Description = i.Description,
            Price = i.Price,
            IsDeleted = i.IsDeleted
        }).ToList();
    }
}