using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        var includeProperties = "RenovationWorkRepairRequests,RenovationWorkRepairRequests.RepairRequest,RenovationWorkRepairRequests.RepairRequest.Client,RenovationWorkRepairRequests.RepairRequest.Master,RenovationWorkRepairRequests.RepairRequest.Employee,RenovationWorkRepairRequests.RepairRequest.Master.Employee";
        
        var renovationWorks = await _renovationWorkRepository.GetAsync(filters);
        return renovationWorks.Select(i => new RenovationWorkDTO
        {
            Id = i.Id,
            Name = i.Name,
            Description = i.Description,
            Price = i.Price,
            IsDeleted = i.IsDeleted,
            RenovationWorkRepairRequests = i.RenovationWorkRepairRequests.Select(i => new RenovationWorkRepairRequestDTO
            {
                RepairRequestId = i.RepairRequestId,
                RenovationWorkId = i.RenovationWorkId,
                DateAdded = i.DateAdded,
                Amount = i.Amount,
                RepairRequest = i.RepairRequest == null ? null : new RepairRequestDTO
                {
                    Id = i.RepairRequestId,
                    CreateDateTime = i.RepairRequest.CreateDateTime,
                    ProvisionalDateOfReceipt = i.RepairRequest.ProvisionalDateOfReceipt,
                    DateOfReceipt = i.RepairRequest.DateOfReceipt,
                    InstrumentName = i.RepairRequest.InstrumentName,
                    IsCase = i.RepairRequest.IsCase,
                    Description = i.RepairRequest.Description,
                    Price = i.RepairRequest.Price,
                    StatusId = i.RepairRequest.StatusId,
                    IsDeleted = i.RepairRequest.IsDeleted,
                    ClientId = i.RepairRequest.ClientId,
                    MasterId = i.RepairRequest.MasterId,
                    EmployeeId = i.RepairRequest.EmployeeId,
                    Client = i.RepairRequest.Client == null ? null : new ClientDTO
                    {
                        Id = i.RepairRequest.Client.Id,
                        PhoneNumber = i.RepairRequest.Client.PhoneNumber,
                        Name = i.RepairRequest.Client.Name,
                        CreateDateTime = i.RepairRequest.Client.CreateDateTime
                    },
                    Master = i.RepairRequest.Master == null ? null : new MasterDTO
                    {
                        EmployeeId = i.RepairRequest.Master.EmployeeId,
                        Employee = i.RepairRequest.Master.Employee == null ? null : new EmployeeDTO
                        {
                            Id = i.RepairRequest.Master.Employee.Id,
                            PhoneNumber = i.RepairRequest.Master.Employee.PhoneNumber,
                            Name = i.RepairRequest.Master.Employee.Name,
                            IsDisabled = i.RepairRequest.Master.Employee.IsDisabled
                        },
                        Percent = i.RepairRequest.Master.Percent,
                        IsDisabled = i.RepairRequest.Master.IsDisabled
                    },
                    Employee = i.RepairRequest.Employee == null ? null : new EmployeeDTO
                    {
                        Id = i.RepairRequest.Employee.Id,
                        PhoneNumber = i.RepairRequest.Employee.PhoneNumber,
                        Name = i.RepairRequest.Employee.Name,
                        IsDisabled = i.RepairRequest.Employee.IsDisabled
                    }
                }
            }).ToList()
        }).ToList();
    }
}