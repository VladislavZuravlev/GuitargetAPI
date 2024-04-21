using Application.DTO;
using Application.IRepositories;
using Application.IServices;
using Application.Models;
using Application.Models.RequestModels.RenovationWork;
using Application.Models.RequestModels.Repair;
using Domain.Entities;

namespace Application.Services;

public class RepairRequestService: IRepairRequestsService
{
    private readonly IRepairRequestsRepository _repairRequestsRepository;

    public RepairRequestService(IRepairRequestsRepository repairRequestsRepository)
    {
        _repairRequestsRepository = repairRequestsRepository;
    }
    
    

    public async Task<OperationResult> AddRepairAsync(AddRepairModel model)
    {
        RepairRequest newRepairRequest;
        try
        {
            newRepairRequest = RepairRequest.Create(model.ClientId, model.MasterId, model.EmployeeId, model.ProvisionalDateOfReceipt, model.InstrumentName, model.IsCase, model.Description, model.Price, model.RenovationWorkId);
        }
        catch (Exception e)
        {
            return new OperationResult { IsSuccess = false, ErrorMessage = $"Не удалось создать заказ. Ошибка: {e.Message}" };
        }

        return await _repairRequestsRepository.AddRepairAsync(newRepairRequest);
    }

    public async Task<List<RepairRequestDTO>> GetRepairsAsync(IEnumerable<Tuple<string, string, object>>? filters = null)
    {
        var includeProperties = "Client,Master,Employee,RenovationWork";
            
        return await _repairRequestsRepository.GetRepairsAsync(filters, includeProperties);
    }
}