using Application.DTO;
using Application.IServices;
using Application.Models;
using Application.Models.RequestModels.Service;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Helpers;

namespace WebAPI.Controllers;

[EmployeeAuthorizeApi]
[Route("api/[controller]")]
public class ServiceController: ControllerBase
{
    private readonly IRenovationWorkService _renovationWorkService;

    public ServiceController(IRenovationWorkService renovationWorkService)
    {
        _renovationWorkService = renovationWorkService;
    }

    
    [HttpGet("GetServices")]
    public async Task<ActionResult<List<ServiceDTO>>> GetServices()
    {
        var services = await _renovationWorkService.GetAsync();
        
        return Ok(services);
    }

    [HttpPost("AddService")]
    public async Task<ActionResult<OperationResult>> AddService([FromBody] AddServiceModel model)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var operationRes = await _renovationWorkService.AddAsync(model);
        
        return Ok(operationRes);
    }
}