using Application.DTO;
using Application.Helpers;
using Application.Helpers.JWT;
using Application.IRepositories;
using Application.IServices;
using Application.Models;
using Application.Models.RequestModels.Employee;
using Domain.Entities;
using Domain.Helpers.Enums;

namespace Application.Services;

public class EmployeeService: IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtProvider _jwtProvider;

    public EmployeeService(IEmployeeRepository employeeRepository, IPasswordHasher passwordHasher, IJwtProvider jwtProvider)
    {
        _employeeRepository = employeeRepository;
        _passwordHasher = passwordHasher;
        _jwtProvider = jwtProvider;
    }


    public async Task<OperationResult> AddAsync(EmployeeRegisterModel registerModel)
    {
        Employee newEmployee;
        try
        {
            newEmployee = Employee.Create(registerModel.Name, registerModel.PhoneNumber, "Test1234");
        }
        catch (Exception e)
        {
            return new OperationResult
                { IsSuccess = false, ErrorMessage = $"Не удалось создать сотрудника. Ошибка: {e.Message}." };
        }

        return await _employeeRepository.AddAsync(newEmployee);
    }

    public async Task<string> LoginAsync(LoginModel model)
    {
        var employee = await _employeeRepository.GetByNumberAsync(model.PhoneNumber);
        
        var isPasswordValid = _passwordHasher.Verify(model.Password, employee?.PasswordHash ?? string.Empty);
        
        return isPasswordValid ? _jwtProvider.GenerateToken(employee) : string.Empty;
    }

    public async Task<OperationResult> RegisterAsync(EmployeeRegisterModel registerModel)
    {
        var hash = _passwordHasher.Generate(registerModel.Password);

        var employee = await _employeeRepository.GetByNumberAsync(registerModel.PhoneNumber);
        if (employee != null) return new OperationResult { IsSuccess = false, ErrorMessage = $"Не удалось зарегистрироваться. Ошибка: Этот номер уже занят." };
        
        try
        {
            var newEmployee = Employee.Create(registerModel.Name, registerModel.PhoneNumber, hash);
            return await _employeeRepository.AddAsync(newEmployee);
        }
        catch (Exception e)
        {
            return new OperationResult
                { IsSuccess = false, ErrorMessage = $"Не удалось зарегистрироваться. Ошибка: {e.Message}." };
        }
    }

    public async Task<List<EmployeeDTO>> GetAsync(IEnumerable<Tuple<string, string, object>>? filters = null)
    {
        var employees = await _employeeRepository.GetAsync(filters);
        
        return employees
            .Select(i => new EmployeeDTO
            {
                Id = i.Id,
                PhoneNumber = i.PhoneNumber,
                Name = i.Name,
                IsDisabled = i.IsDisabled,
                Roles = i.Roles == null ? null : i.Roles.Select(rol => new EmployeeRoleDTO
                {
                    EmployeeId = rol.EmployeeId,
                    RoleId = rol.RoleId
                }),
                Repairs = i.Repairs == null ? null : i.Repairs.Select(r => new RepairRequestDTO
                {
                    Id = r.Id,
                    CreateDateTime = r.CreateDateTime,
                    ProvisionalDateOfReceipt = r.ProvisionalDateOfReceipt,
                    DateOfReceipt = r.DateOfReceipt,
                    InstrumentName = r.InstrumentName,
                    IsCase = r.IsCase,
                    Description = r.Description,
                    Price = r.Price,
                    StatusId = r.StatusId,
                    IsDeleted = r.IsDeleted,
                    ClientId = r.ClientId,
                    MasterId = r.MasterId,
                    EmployeeId = r.EmployeeId,
                    Client = new ClientDTO
                    {
                        Id = r.Client.Id,
                        PhoneNumber = r.Client.PhoneNumber,
                        Name = r.Client.Name,
                        CreateDateTime = r.Client.CreateDateTime
                    },
                    Master = new MasterDTO
                    {
                        EmployeeId = r.Master.EmployeeId,
                        Employee = new EmployeeDTO
                        {
                            Id = r.Employee.Id,
                            PhoneNumber = r.Employee.PhoneNumber,
                            Name = r.Employee.Name,
                            IsDisabled = r.Employee.IsDisabled
                        },
                        Percent = r.Master.Percent,
                        IsDisabled = r.Master.IsDisabled
                    },
                    RenovationWorkRepairRequests = r.RenovationWorkRepairRequests.Select(i => new RenovationWorkRepairRequestDTO
                    {
                        RepairRequestId = i.RepairRequestId,
                        RenovationWorkId = i.RenovationWorkId,
                        DateAdded = i.DateAdded,
                        Amount = i.Amount,
                        RenovationWork = new RenovationWorkDTO
                        {
                            Id = i.RenovationWork.Id,
                            Name = i.RenovationWork.Name,
                            Description = i.RenovationWork.Description,
                            Price = i.RenovationWork.Price,
                            IsDeleted = i.RenovationWork.IsDeleted
                        }
                    }).ToList()
                })
            })
            .ToList();
    }

    public async Task<EmployeeDTO?> GetById(int id)
    {
        var employee = await _employeeRepository.GetByIdAsync(id);

        return employee == null ? null : new EmployeeDTO
        {
            Id = employee.Id,
            PhoneNumber = employee.PhoneNumber,
            Name = employee.Name,
            IsDisabled = employee.IsDisabled,
            Roles = employee.Roles == null ? null : employee.Roles.Select(rol => new EmployeeRoleDTO
            {
                EmployeeId = rol.EmployeeId,
                RoleId = rol.RoleId
            })
        };
    }

    public bool CheckEmployeeRoles(int id, RoleType[] roles)
    {
        Employee? employee = null;

        Task.Run(async () => employee = await _employeeRepository.GetByIdAsync(id)).Wait();

        if (employee == null || employee.IsDisabled || employee.Roles.Count == 0) return false;

        return employee.Roles.Any(r => roles.Any(i => (byte)i == r.RoleId));
    }
}