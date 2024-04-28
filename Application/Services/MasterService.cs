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
        
        var masters = await _masterRepository.GetAsync(filters, includeProperties);

        return masters
            .Select(i => new MasterDTO
            {
                EmployeeId = i.EmployeeId,
                Percent = i.Percent,
                IsDisabled = i.IsDisabled,
                RepairsCount = i.Repairs.Count,
                Employee = new EmployeeDTO
                {
                    Id = i.Employee.Id,
                    PhoneNumber = i.Employee.PhoneNumber,
                    Name = i.Employee.Name,
                    IsDisabled = i.Employee.IsDisabled
                },
                Repairs = i.Repairs.Select(r => new RepairRequestDTO
                {
                    Id = r.Id,
                    CreateDateTime = r.CreateDateTime,
                    ProvisionalDateOfReceipt = r.ProvisionalDateOfReceipt,
                    DateOfReceipt = r.DateOfReceipt,
                    InstrumentName = r.InstrumentName,
                    IsCase = r.IsCase,
                    Description = r.Description,
                    Price = r.Price,
                    StatusId = r.StatusId,
                    IsDeleted = r.IsDeleted,
                    ClientId = r.ClientId,
                    MasterId = r.MasterId,
                    EmployeeId = r.EmployeeId,
                    Client = new ClientDTO
                    {
                        Id = r.Client.Id,
                        PhoneNumber = r.Client.PhoneNumber,
                        Name = r.Client.Name,
                        CreateDateTime = r.Client.CreateDateTime
                    },
                    Employee = new EmployeeDTO
                    {
                        Id = r.Employee.Id,
                        PhoneNumber = r.Employee.PhoneNumber,
                        Name = r.Employee.Name,
                        IsDisabled = r.Employee.IsDisabled
                    },
                    RenovationWorkRepairRequests = r.RenovationWorkRepairRequests.Select(i => new RenovationWorkRepairRequestDTO
                    {
                        RepairRequestId = i.RepairRequestId,
                        RenovationWorkId = i.RenovationWorkId,
                        DateAdded = i.DateAdded,
                        Amount = i.Amount,
                        RenovationWork = new RenovationWorkDTO
                        {
                            Id = i.RenovationWork.Id,
                            Name = i.RenovationWork.Name,
                            Description = i.RenovationWork.Description,
                            Price = i.RenovationWork.Price,
                            IsDeleted = i.RenovationWork.IsDeleted
                        }
                    }).ToList()
                })
            })
            .ToList();
    }
}