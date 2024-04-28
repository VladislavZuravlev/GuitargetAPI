using Application.Models;
using Domain.Entities;

namespace Application.IRepositories;

public interface IRepairRequestsRepository
{
    Task<OperationResult> AddRepairRequestAsync(RepairRequest newRepairRequest);
    Task<List<RepairRequest>> GetRepairRequestsAsync(IEnumerable<Tuple<string, string, object>>? filters = null, string? includeProperties = null, Dictionary<string, string>? orderCollection = null);
}