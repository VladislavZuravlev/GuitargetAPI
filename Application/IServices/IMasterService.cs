using Application.DTO;
using Application.Models;
using Application.Models.RequestModels.Master;

namespace Application.IServices;

public interface IMasterService
{
    Task<OperationResult> AddAsync(AddMasterModel model);
    Task<List<MasterDTO>> GetAsync(IEnumerable<Tuple<string, string, object>>? filters = null);
}