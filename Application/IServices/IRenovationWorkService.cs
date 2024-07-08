using Application.DTO;
using Application.Models;
using Application.Models.RequestModels.Service;

namespace Application.IServices;

public interface IRenovationWorkService
{
    Task<OperationResult> AddAsync(AddServiceModel model);
    Task<List<ServiceDTO>> GetAsync(IEnumerable<Tuple<string, string, object>>? filters = null);
}