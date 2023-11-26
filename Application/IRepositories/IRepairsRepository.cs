using Application.DTO;
using Application.Models;
using Domain.Entities;

namespace Application.IRepositories;

public interface IRepairsRepository
{
    Task<OperationResult> AddRepairAsync(Repair newRepair);
    Task<List<RepairDTO>> GetRepairsAsync(DateTime? provisionalDateOfReceipt, string? instrumentName, bool isCase, string? description, decimal? price, string? clientPhone, int? masterId, int? employeeId, int? renovationWorkId);

    Task<OperationResult> AddRenovationWorkAsync(RenovationWork newRenovationWork);
    Task<List<RenovationWorkDTO>> GetRenovationWorksAsync(string? name, string? description, decimal? price);
}