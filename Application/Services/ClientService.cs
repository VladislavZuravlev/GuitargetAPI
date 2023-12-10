using Application.DTO;
using Application.IRepositories;
using Application.IServices;
using Application.Models;
using Application.Models.RequestModels.Client;
using Domain.Entities;

namespace Application.Services;

public class ClientService: IClientService
{
    private readonly IClientRepository _clientRepository;

    public ClientService(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public async Task<OperationResult> AddAsync(AddClientModel model)
    {
        Client newClient;
        try
        {
            newClient = Client.Create(model.Name, model.PhoneNumber);
        }
        catch (Exception e)
        {
            return new OperationResult
                { IsSuccess = false, ErrorMessage = $"Не удалось создать клиента. Ошибка: {e.Message}." };
        }

        return await _clientRepository.AddAsync(newClient);
    }

    public async Task<List<ClientDTO>> GetAsync(IEnumerable<Tuple<string, string, object>>? filters = null)
    {
        var test = await _clientRepository.GetAsync(filters);
        
        return test;
    }
}