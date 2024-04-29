using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DTO;
using Application.Models;
using Application.Models.RequestModels.RenovationWork;

namespace Application.IServices;

public interface IRenovationWorkService
{
    Task<OperationResult> AddAsync(AddRenovationWorkModel model);
    Task<List<RenovationWorkDTO>> GetAsync(IEnumerable<Tuple<string, string, object>>? filters = null);
}