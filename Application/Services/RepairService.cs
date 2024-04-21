using Application.DTO;
using Application.IRepositories;
using Application.IServices;
using Application.Models;
using Application.Models.RequestModels.RenovationWork;
using Application.Models.RequestModels.Repair;
using Domain.Entities;

namespace Application.Services;

public class RepairService: IRepairsService
{
    private readonly IRepairsRepository _repairsRepository;

    public RepairService(IRepairsRepository repairsRepository)
    {
        _repairsRepository = repairsRepository;
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

        return await _repairsRepository.AddRepairAsync(newRepairRequest);
    }

    public async Task<List<RepairRequestDTO>> GetRepairsAsync(IEnumerable<Tuple<string, string, object>>? filters = null)
    {
        var includeProperties = "Client,Master,Employee,RenovationWork";
            
        return await _repairsRepository.GetRepairsAsync(filters, includeProperties);
    }
}