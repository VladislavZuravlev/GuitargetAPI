using Application.DTO;
using Application.Models;
using Domain.Entities;

namespace Application.IRepositories;

public interface IRepairsRepository
{
    Task<OperationResult> AddRepairAsync(Repair newRepair);
    Task<List<RepairDTO>> GetRepairsAsync(IEnumerable<Tuple<string, string, object>>? filters = null, string? includeProperties = null, Dictionary<string, string>? orderCollection = null);

    Task<OperationResult> AddRenovationWorkAsync(RenovationWork newRenovationWork);
    Task<List<RenovationWorkDTO>> GetRenovationWorksAsync(IEnumerable<Tuple<string, string, object>>? filters = null, string? includeProperties = null, Dictionary<string, string>? orderCollection = null);
}