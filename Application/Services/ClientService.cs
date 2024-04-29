using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        var clients = await _clientRepository.GetAsync(filters);
        
        return clients
            .Select(i => new ClientDTO
            {
                Id = i.Id,
                PhoneNumber = i.PhoneNumber,
                Name = i.Name,
                CreateDateTime = i.CreateDateTime,
                RepairRequests = i.Repairs?.Select(r => new RepairRequestDTO
                {
                    Id = r.Id,
                    ClientId = r.ClientId,
                    Client = null,
                    CreateDateTime = r.CreateDateTime,
                    ProvisionalDateOfReceipt = r.ProvisionalDateOfReceipt,
                    DateOfReceipt = r.DateOfReceipt,
                    InstrumentName = r.InstrumentName,
                    IsCase = r.IsCase,
                    Description = r.Description,
                    Price = r.Price,
                    StatusId = r.StatusId,
                    IsDeleted = r.IsDeleted,
                    MasterId = r.MasterId,
                    EmployeeId = r.EmployeeId,
                    Master = new MasterDTO
                    {
                        EmployeeId = r.Master.EmployeeId,
                        Employee = new EmployeeDTO
                        {
                            Id = r.Employee.Id,
                            PhoneNumber = r.Employee.PhoneNumber,
                            Name = r.Employee.Name,
                            IsDisabled = r.Employee.IsDisabled
                        },
                        Percent = r.Master.Percent,
                        IsDisabled = r.Master.IsDisabled
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
            }).ToList();
    }
}