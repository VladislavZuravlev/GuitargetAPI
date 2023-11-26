using Application.DTO;
using Application.Models;
using Application.Models.RequestModels.RenovationWork;
using Application.Models.RequestModels.Repair;

namespace Application.IServices;

public interface IRepairsService
{
    Task<OperationResult> AddRepairAsync(AddRepairModel model);
    Task<List<RepairDTO>> GetRepairsAsync(RepairFilterModel model);
    
    Task<OperationResult> AddRenovationWorkAsync(AddRenovationWorkModel model);
    Task<List<RenovationWorkDTO>> GetRenovationWorksAsync(RenovationWorkFilterModel model);
}