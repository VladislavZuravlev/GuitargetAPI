using Application.DTO;
using Application.IRepositories;
using Application.IServices;
using Application.Models;
using Application.Models.RequestModels.Employee;
using Domain.Entities;

namespace Application.Services;

public class EmployeeService: IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeeService(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }
    
    
    
    
    public async Task<OperationResult> AddAsync(AddEmployeeModel model)
    {
        Employee newEmployee;
        try
        {
            newEmployee = Employee.Create(model.Name, model.PhoneNumber);
        }
        catch (Exception e)
        {
            return new OperationResult
                { IsSuccess = false, ErrorMessage = $"Не удалось создать сотрудника. Ошибка: {e.Message}." };
        }

        return await _employeeRepository.AddAsync(newEmployee);
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
                Repairs = i.Repairs.Select(r => new RepairRequestDTO
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
}