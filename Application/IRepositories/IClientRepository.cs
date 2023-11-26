using Application.DTO;
using Application.Models;
using Domain.Entities;

namespace Application.IRepositories;

public interface IClientRepository
{
    Task<OperationResult> AddAsync(Client newClient);
    Task<List<ClientDTO>> GetAsync(string name, string phone, DateTime? periodStartDate, DateTime? periodEndDate);
}