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

    
    [HttpGet("GetAllServices")]
    public async Task<ActionResult<List<ServiceDTO>>> GetAllServices()
    {
        var services = await _renovationWorkService.GetAsync();
        
        return Ok(services);
    }

    [HttpGet("GetCurrentServices")]
    public async Task<ActionResult<List<ServiceDTO>>> GetCurrentServices()
    {
        var filters = new List<Tuple<string, string, object>>
        {
            new Tuple<string, string, object>("IsDeleted", "==", false)
        };
        var services = await _renovationWorkService.GetAsync(filters);
        
        return Ok(services);
    }

    [HttpPost("AddService")]
    public async Task<ActionResult<OperationResult>> AddService([FromBody] AddServiceModel model)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var operationRes = await _renovationWorkService.AddAsync(model);
        
        return Ok(operationRes);
    }

    [HttpDelete("RemoveService")]
    public async Task<ActionResult> RemoveService(int id)
    {
        var res = await _renovationWorkService.RemoveAsync(id);

        return res.IsSuccess ? Ok() : BadRequest(res.ErrorMessage);
    }
    
    [HttpPost("RestoreService")]
    public async Task<ActionResult> RestoreService(int id)
    {
        var res = await _renovationWorkService.RestoreAsync(id);
        
        return res.IsSuccess ? Ok() : BadRequest(res.ErrorMessage);
    }
}