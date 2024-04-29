using Application.DTO;
using Application.IServices;
using Application.Models;
using Application.Models.RequestModels.RenovationWork;
using Application.Models.RequestModels.Repair;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class RepairRequestController: ControllerBase
{
    private readonly IRepairRequestsService _repairRequestsService;

    public RepairRequestController(IRepairRequestsService repairRequestsService)
    {
        _repairRequestsService = repairRequestsService;
    }
    
    
    [HttpGet("GetRepairs")]
    public async Task<ActionResult<List<RepairRequestDTO>>> GetRepairs()
    {
        var repairs = await _repairRequestsService.GetRepairRequestsAsync();
        
        return Ok(repairs);
    }

    [HttpPost("AddRepair")]
    public async Task<ActionResult<OperationResult>> AddRepair([FromQuery] AddRepairRequestModel requestModel)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var operationRes = await _repairRequestsService.AddRepairRequestAsync(requestModel);
        
        return Ok(operationRes);
    }
}