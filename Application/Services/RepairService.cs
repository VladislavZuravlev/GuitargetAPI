﻿using Application.DTO;
using Application.IRepositories;
using Application.IServices;
using Application.Models;
using Application.Models.RequestModels.RenovationWork;
using Application.Models.RequestModels.Repair;
using Domain.Entities;

namespace Application.Services;

public class RepairService: IRepairsService
{
    private readonly IRepairsRepository _repairsRepository;

    public RepairService(IRepairsRepository repairsRepository)
    {
        _repairsRepository = repairsRepository;
    }
    
    

    public async Task<OperationResult> AddRepairAsync(AddRepairModel model)
    {
        Repair newRepair;
        try
        {
            newRepair = Repair.Create(model.ClientId, model.MasterId, model.EmployeeId, model.ProvisionalDateOfReceipt, model.InstrumentName, model.IsCase, model.Description, model.Price);
        }
        catch (Exception e)
        {
            return new OperationResult { IsSuccess = false, ErrorMessage = $"Не удалось создать заказ. Ошибка: {e.Message}" };
        }

        return await _repairsRepository.AddRepairAsync(newRepair);
    }

    public async Task<List<RepairDTO>> GetRepairsAsync(RepairFilterModel model)
    {
        return await _repairsRepository.GetRepairsAsync(model.ProvisionalDateOfReceipt, model.InstrumentName, model.IsCase,
            model.Description, model.Price, model.ClientPhone, model.MasterId, model.EmployeeId, model.RenovationWorkId);
    }

    public async Task<OperationResult> AddRenovationWorkAsync(AddRenovationWorkModel model)
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

        return await _repairsRepository.AddRenovationWorkAsync(newRenovationWork);
    }

    public async Task<List<RenovationWorkDTO>> GetRenovationWorksAsync(RenovationWorkFilterModel model)
    {
        return await _repairsRepository.GetRenovationWorksAsync(model.Name, model.Description, model.Price);
    }
}