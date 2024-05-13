using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DTO;
using Application.IServices;
using Application.Models;
using Application.Models.RequestModels.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Helpers;

namespace WebAPI.Controllers;

[EmployeeAuthorizeApi]
[Route("api/[controller]")]
[ApiController]
public class ClientController: ControllerBase
{

    private readonly IClientService _clientService;

    public ClientController(IClientService clientService)
    {
        _clientService = clientService;
    }

    
    [HttpPost("Add")]
    public async Task<ActionResult<OperationResult>> Add([FromQuery] AddClientModel model)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        var client = await _clientService.AddAsync(model);

        return Ok(client);
    }

    [HttpGet("Get")]
    public async Task<ActionResult<List<ClientDTO>>> Get()
    {
        var clients = await _clientService.GetAsync();
        
        return Ok(clients);
    }
}