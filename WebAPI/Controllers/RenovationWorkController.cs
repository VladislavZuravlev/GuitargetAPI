using Application.DTO;
using Application.Models;
using Application.Models.RequestModels.RenovationWork;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RenovationWorkController: ControllerBase
{
    [HttpGet("Get")]
    public async Task<ActionResult<List<RenovationWorkDTO>>> Get([FromQuery]RenovationWorkFilterModel model)
    {
        
        
        
        return Ok();
    }

    [HttpPost("Add")]
    public async Task<ActionResult<OperationResult>> Add([FromQuery]AddRenovationWorkModel model)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        return Ok();
    }
}