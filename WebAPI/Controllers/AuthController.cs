using Application.IServices;
using Application.Models.RequestModels.Employee;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController: ControllerBase
{
    private readonly IEmployeeService _employeeService;

    public AuthController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }
    
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
        
        return res.IsSuccess ? Ok() : BadRequest(res.ErrorMessage);
    }
}