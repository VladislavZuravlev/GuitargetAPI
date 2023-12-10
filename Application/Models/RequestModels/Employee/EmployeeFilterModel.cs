namespace Application.Models.RequestModels.Employee;

public class EmployeeFilterModel
{
    public string PhoneNumber { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public bool IsDisabled { get; set; }
}