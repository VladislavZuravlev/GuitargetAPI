using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DTO;
using Application.IServices;
using Application.Models;
using Application.Models.RequestModels.RenovationWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Helpers;

namespace WebAPI.Controllers;

[EmployeeAuthorizeApi]
public class RenovationWorkController: ControllerBase
{
    private readonly IRenovationWorkService _renovationWorkService;

    public RenovationWorkController(IRenovationWorkService renovationWorkService)
    {
        _renovationWorkService = renovationWorkService;
    }

    
    [HttpGet("GetRenovationWorks")]
    public async Task<ActionResult<List<RepairRequestDTO>>> GetRenovationWorks()
    {
        var renovationWorks = await _renovationWorkService.GetAsync();
        
        return Ok(renovationWorks);
    }

    [HttpPost("AddRenovationWork")]
    public async Task<ActionResult<OperationResult>> AddRenovationWork([FromQuery] AddRenovationWorkModel model)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var operationRes = await _renovationWorkService.AddAsync(model);
        
        return Ok(operationRes);
    }
}