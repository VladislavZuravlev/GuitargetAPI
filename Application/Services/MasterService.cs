using Application.DTO;
using Application.IRepositories;
using Application.IServices;
using Application.Models;
using Application.Models.RequestModels.Master;
using Domain.Entities;

namespace Application.Services;

public class MasterService: IMasterService
{
    private readonly IMasterRepository _masterRepository;

    public MasterService(IMasterRepository masterRepository)
    {
        _masterRepository = masterRepository;
    }
    
    
    
    public async Task<OperationResult> AddAsync(AddMasterModel model)
    {
        Master newMaster;
        try
        {
            newMaster = Master.Create(model.EmployeeId, model.Percent);
        }
        catch (Exception e)
        {
            return new OperationResult { IsSuccess = false, ErrorMessage = $"Не удалось создать мастера. Ошибка: {e.Message}" };
        }

        return await _masterRepository.AddAsync(newMaster);
    }

    public async Task<List<MasterDTO>> GetAsync(IEnumerable<Tuple<string, string, object>>? filters = null)
    {
        var includeProperties = "Employee";
        
        var res = await _masterRepository.GetAsync(filters, includeProperties);

        return res;
    }
}