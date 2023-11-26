using Application.DTO;
using Application.Models;
using Domain.Entities;

namespace Application.IRepositories;

public interface IMasterRepository
{
    Task<OperationResult> AddAsync(EmployeeMaster newMaster);
    Task<List<MasterDTO>> GetAsync(string name, string phone, bool isDisabled);
}