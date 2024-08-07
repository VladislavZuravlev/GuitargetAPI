﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTO;
using Application.IRepositories;
using Application.IServices;
using Application.Models;
using Application.Models.RequestModels.RenovationWork;
using Application.Models.RequestModels.Repair;
using Domain.Entities;

namespace Application.Services;

public class RepairRequestService: IRepairRequestsService
{
    private readonly IRepairRequestsRepository _repairRequestsRepository;
    private readonly IRenovationWorkRepository _renovationWorkRepository;

    public RepairRequestService(IRepairRequestsRepository repairRequestsRepository, IRenovationWorkRepository renovationWorkRepository)
    {
        _repairRequestsRepository = repairRequestsRepository;
        _renovationWorkRepository = renovationWorkRepository;
    }
    
    

    public async Task<OperationResult> AddRepairRequestAsync(AddRepairRequestModel model)
    {
        RepairRequest newRepairRequest;
        try
        {
            newRepairRequest = RepairRequest.Create(model.ClientId, model.MasterId, model.EmployeeId, model.ProvisionalDateOfReceipt, model.InstrumentName, model.IsCase, model.Description, model.Price);
            
            var renovationWorkFilters = new List<Tuple<string, string, object>> { new ("Id", "in", model.RenovationWorkIds) };
            var renovationWorks = await _renovationWorkRepository.GetAsync(renovationWorkFilters);

            if (renovationWorks.Count == 0)  return new OperationResult { IsSuccess = false, ErrorMessage = "Такие ремонтные работы не найдены. Проверьте правыильность заполнения причин ремонта." };

            var renovationWorkRepairRequests = renovationWorks.Select(rw => RenovationWorkRepairRequest.Create(rw.Id, newRepairRequest.Id, rw.Price)).ToList();

            newRepairRequest.RenovationWorkRepairRequests = renovationWorkRepairRequests;
        }
        catch (Exception e)
        {
            return new OperationResult { IsSuccess = false, ErrorMessage = $"Не удалось создать заказ. Ошибка: {e.Message}" };
        }

        return await _repairRequestsRepository.AddRepairRequestAsync(newRepairRequest);
    }

    public async Task<List<RepairRequestDTO>> GetRepairRequestsAsync(IEnumerable<Tuple<string, string, object>>? filters = null)
    {
        var includeProperties = "Client,Master,Employee,RenovationWorkRepairRequests,RenovationWorkRepairRequests.RenovationWork";
            
        var repairRequests = await _repairRequestsRepository.GetRepairRequestsAsync(filters, includeProperties);
        return repairRequests.Select(r => new RepairRequestDTO
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
                Master = r.Master != null ? new MasterDTO
                {
                    EmployeeId = r.Master.EmployeeId,
                    Employee = r.Master.Employee != null ? new EmployeeDTO
                    {
                        Id = r.Master.Employee.Id,
                        PhoneNumber = r.Master.Employee.PhoneNumber,
                        Name = r.Master.Employee.Name,
                        IsDisabled = r.Master.Employee.IsDisabled
                    } : null,
                    Percent = r.Master.Percent,
                    IsDisabled = r.Master.IsDisabled
                } : null,
                RepairRequestServices = r.RenovationWorkRepairRequests.Select(i => new RepairRequestServiceDTO
                {
                    RepairRequestId = i.RepairRequestId,
                    RenovationWorkId = i.RenovationWorkId,
                    DateAdded = i.DateAdded,
                    Amount = i.Amount,
                    Service = new ServiceDTO
                    {
                        Id = i.RenovationWork.Id,
                        Name = i.RenovationWork.Name,
                        Description = i.RenovationWork.Description,
                        Price = i.RenovationWork.Price,
                        IsDeleted = i.RenovationWork.IsDeleted
                    }
                }).ToList(),
                Employee = r.Employee != null ? new EmployeeDTO
                {
                    Id = r.Employee.Id,
                    PhoneNumber = r.Employee.PhoneNumber,
                    Name = r.Employee.Name,
                    IsDisabled = r.Employee.IsDisabled
                } : null
            })
            .ToList();
    }

    public async Task<RepairRequestDTO?> GetById(int id)
    {
        var repairRequest = await _repairRequestsRepository.GetById(id);

        if (repairRequest == null) return null;
        
        var dto = new RepairRequestDTO
        {
            Id = repairRequest.Id,
            CreateDateTime = repairRequest.CreateDateTime,
            ProvisionalDateOfReceipt = repairRequest.ProvisionalDateOfReceipt,
            DateOfReceipt = repairRequest.DateOfReceipt,
            InstrumentName = repairRequest.InstrumentName,
            IsCase = repairRequest.IsCase,
            Description = repairRequest.Description,
            Price = repairRequest.Price,
            StatusId = repairRequest.StatusId,
            IsDeleted = repairRequest.IsDeleted,
            ClientId = repairRequest.ClientId,
            MasterId = repairRequest.MasterId,
            EmployeeId = repairRequest.EmployeeId,
            Client = new ClientDTO
            {
                Id = repairRequest.Client.Id,
                PhoneNumber = repairRequest.Client.PhoneNumber,
                Name = repairRequest.Client.Name,
                CreateDateTime = repairRequest.Client.CreateDateTime
            },
            Master = new MasterDTO
            {
                EmployeeId = repairRequest.Master.EmployeeId,
                Employee = new EmployeeDTO
                {
                    Id = repairRequest.Master.Employee.Id,
                    PhoneNumber = repairRequest.Master.Employee.PhoneNumber,
                    Name = repairRequest.Master.Employee.Name,
                    IsDisabled = repairRequest.Master.Employee.IsDisabled
                },
                Percent = repairRequest.Master.Percent,
                IsDisabled = repairRequest.Master.IsDisabled
            },
            Employee = new EmployeeDTO
            {
                Id = repairRequest.Employee.Id,
                PhoneNumber = repairRequest.Employee.PhoneNumber,
                Name = repairRequest.Employee.Name,
                IsDisabled = repairRequest.Employee.IsDisabled
            },
            RepairRequestServices = repairRequest.RenovationWorkRepairRequests.Select(i =>
                new RepairRequestServiceDTO
                {
                    RepairRequestId = i.RepairRequestId,
                    RenovationWorkId = i.RenovationWorkId,
                    DateAdded = i.DateAdded,
                    Amount = i.Amount,
                    Service = new ServiceDTO
                    {
                        Id = i.RenovationWork.Id,
                        Name = i.RenovationWork.Name,
                        Description = i.RenovationWork.Description,
                        Price = i.RenovationWork.Price,
                        IsDeleted = i.RenovationWork.IsDeleted
                    }
                }).ToList()
        };
        return dto;
    }
}