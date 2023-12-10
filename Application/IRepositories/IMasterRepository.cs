using Application.DTO;
using Application.Models;
using Domain.Entities;

namespace Application.IRepositories;

public interface IMasterRepository
{
    Task<OperationResult> AddAsync(EmployeeMaster newMaster);
    Task<List<MasterDTO>> GetAsync(IEnumerable<Tuple<string, string, object>>? filters = null, string? includeProperties = null, Dictionary<string, string>? orderCollection = null);
}