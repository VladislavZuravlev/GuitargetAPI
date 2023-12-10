using Application.DTO;
using Application.IRepositories;
using Application.IServices;
using Application.Models;
using Application.Models.RequestModels.RenovationWork;
using Domain.Entities;

namespace Application.Services;

public class RenovationWorkService: IRenovationWorkService
{
    private readonly IRenovationWorkRepository _renovationWorkRepository;

    public RenovationWorkService(IRenovationWorkRepository renovationWorksRepository)
    {
        _renovationWorkRepository = renovationWorksRepository;
    }


    public async Task<OperationResult> AddAsync(AddRenovationWorkModel model)
    {
        RenovationWork newRenovationWork;

        try
        {
            newRenovationWork = RenovationWork.Create(model.Name, model.Description, model.Price);
        }
        catch (Exception e)
        {
            return new OperationResult { IsSuccess = false, ErrorMessage = $"Не удалось создать прайс. Ошибка: {e.Message}" };
        }

        return await _renovationWorkRepository.AddAsync(newRenovationWork);
    }

    public async Task<List<RenovationWorkDTO>> GetAsync(IEnumerable<Tuple<string, string, object>>? filters = null)
    {
        return await _renovationWorkRepository.GetAsync(filters);
    }
}