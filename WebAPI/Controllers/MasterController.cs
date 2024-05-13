using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DTO;
using Application.IServices;
using Application.Models;
using Application.Models.RequestModels.Master;
using Domain.Helpers.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Helpers;

namespace WebAPI.Controllers;

[EmployeeAuthorizeApi]
[Route("api/[controller]")]
[ApiController]
public class MasterController: ControllerBase
{
    private readonly IMasterService _masterService;

    public MasterController(IMasterService masterService)
    {
        _masterService = masterService;
    }

    [HttpPost("Add")]
    public async Task<ActionResult<OperationResult>> Add([FromQuery]AddMasterModel model)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var operationRes = await _masterService.AddAsync(model);

        return Ok(operationRes);
    }

    [HttpGet("Get")]
    public async Task<ActionResult<List<MasterDTO>>> Get()
    {
        var masters = await _masterService.GetAsync();
        
        return Ok(masters);
    }
}