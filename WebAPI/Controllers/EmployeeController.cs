using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DTO;
using Application.IServices;
using Application.Models;
using Application.Models.RequestModels.Employee;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class EmployeeController: ControllerBase
{
    private readonly IEmployeeService _employeeService;

    public EmployeeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [AllowAnonymous]
    [HttpGet("Login")]
    public async Task<ActionResult> Login([FromQuery]LoginModel model)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var token = await _employeeService.LoginAsync(model);

        if (string.IsNullOrEmpty(token))
        {
            ModelState.AddModelError("this", "Вы ввели неверный логин или пароль.");
            return BadRequest(ModelState);
        }
        
        HttpContext.Response.Cookies.Append("Guitarget", token);
        return Ok();
    }
    
    [HttpPost("Register")]
    public async Task<ActionResult> Register([FromQuery]EmployeeRegisterModel registerModel)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var res = await _employeeService.RegisterAsync(registerModel);
        
        return Ok(res);
    }
    
    [Authorize]
    [HttpGet("Get")]
    public async Task<ActionResult<List<OperationResult>>> Get()
    {
        var employees = await _employeeService.GetAsync();
         
        return Ok(employees);
    }

    [Authorize]
    [HttpPost("Add")]
    public async Task<ActionResult<EmployeeDTO>> Add([FromQuery]EmployeeRegisterModel registerModel)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var operationRes = await _employeeService.AddAsync(registerModel);
        
        return Ok(operationRes);
    }
}