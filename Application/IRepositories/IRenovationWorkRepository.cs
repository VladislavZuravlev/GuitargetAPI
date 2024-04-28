using Application.Models;
using Domain.Entities;

namespace Application.IRepositories;

public interface IRenovationWorkRepository
{
    Task<OperationResult> AddAsync(RenovationWork newRenovationWork);
    Task<List<RenovationWork>> GetAsync(IEnumerable<Tuple<string, string, object>>? filters = null, string? includeProperties = null, Dictionary<string, string>? orderCollection = null);
}