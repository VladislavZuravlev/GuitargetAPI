using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DTO;
using Application.Models;
using Application.Models.RequestModels.Repair;

namespace Application.IServices;

public interface IRepairRequestsService
{
    Task<OperationResult> AddRepairRequestAsync(AddRepairRequestModel requestModel);
    Task<List<RepairRequestDTO>> GetRepairRequestsAsync(IEnumerable<Tuple<string, string, object>>? filters = null);
}