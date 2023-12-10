using Application.DTO;
using Application.IServices;
using Application.Models;
using Application.Models.RequestModels.Employee;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController: ControllerBase
{
    private readonly IEmployeeService _employeeService;

    public EmployeeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }


    [HttpGet("Get")]
    public async Task<ActionResult<List<OperationResult>>> Get()
    {
        var employees = await _employeeService.GetAsync();
         
        return Ok(employees);
    }

    [HttpPost("Add")]
    public async Task<ActionResult<EmployeeDTO>> Add([FromQuery]AddEmployeeModel model)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var operationRes = await _employeeService.AddAsync(model);
        
        return Ok(operationRes);
    }
}