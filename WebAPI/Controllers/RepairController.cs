using Application.DTO;
using Application.IServices;
using Application.Models;
using Application.Models.RequestModels.RenovationWork;
using Application.Models.RequestModels.Repair;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RepairController: ControllerBase
{
    private readonly IRepairRequestsService _repairRequestsService;

    public RepairController(IRepairRequestsService repairRequestsService)
    {
        _repairRequestsService = repairRequestsService;
    }
    
    
    [HttpGet("GetRepairs")]
    public async Task<ActionResult<List<RepairRequestDTO>>> GetRepairs()
    {
        var repairs = await _repairRequestsService.GetRepairsAsync();
        
        return Ok(repairs);
    }

    [HttpPost("AddRepair")]
    public async Task<ActionResult<OperationResult>> AddRepair([FromQuery] AddRepairModel model)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var operationRes = await _repairRequestsService.AddRepairAsync(model);
        
        return Ok(operationRes);
    }
}