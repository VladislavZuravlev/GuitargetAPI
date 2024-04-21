using Application.DTO;
using Application.Models;
using Domain.Entities;

namespace Application.IRepositories;

public interface IRepairRequestsRepository
{
    Task<OperationResult> AddRepairAsync(RepairRequest newRepairRequest);
    Task<List<RepairRequestDTO>> GetRepairsAsync(IEnumerable<Tuple<string, string, object>>? filters = null, string? includeProperties = null, Dictionary<string, string>? orderCollection = null);
}