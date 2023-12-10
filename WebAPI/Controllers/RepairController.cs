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
    private readonly IRepairsService _repairsService;

    public RepairController(IRepairsService repairsService)
    {
        _repairsService = repairsService;
    }
    
    
    [HttpGet("GetRepairs")]
    public async Task<ActionResult<List<RepairDTO>>> GetRepairs()
    {
        var repairs = await _repairsService.GetRepairsAsync();
        
        return Ok(repairs);
    }

    [HttpPost("AddRepair")]
    public async Task<ActionResult<OperationResult>> AddRepair([FromQuery] AddRepairModel model)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var operationRes = await _repairsService.AddRepairAsync(model);
        
        return Ok(operationRes);
    }
}