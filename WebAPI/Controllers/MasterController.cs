using Application.DTO;
using Application.Models;
using Application.Models.RequestModels.Master;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MasterController: ControllerBase
{
    
    
    [HttpPost("Add")]
    public async Task<ActionResult<OperationResult>> Add([FromQuery]AddMasterModel model)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        return Ok();
    }

    [HttpPost("Get")]
    public async Task<ActionResult<List<MasterDTO>>> Get([FromQuery]MasterFilterModel model)
    {
        
        
        return Ok();
    }
}