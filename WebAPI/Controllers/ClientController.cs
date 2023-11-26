using Application.DTO;
using Application.IServices;
using Application.Models;
using Application.Models.RequestModels.Client;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

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
    public async Task<ActionResult<List<ClientDTO>>> Get([FromQuery] ClientFilterModel model)
    {
        var clients = await _clientService.GetAsync(model);
        
        return Ok(clients);
    }
}