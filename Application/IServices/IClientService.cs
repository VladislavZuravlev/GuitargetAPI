using Application.DTO;
using Application.Models;
using Application.Models.RequestModels.Client;

namespace Application.IServices;

public interface IClientService
{
    Task<OperationResult> AddAsync(AddClientModel model);
    Task<List<ClientDTO>> GetAsync(IEnumerable<Tuple<string, string, object>>? filters = null);
}