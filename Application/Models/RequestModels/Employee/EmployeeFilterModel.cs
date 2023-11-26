namespace Application.Models.RequestModels.Employee;

public class EmployeeFilterModel
{
    public string PhoneNumber { get; set; }
    public string Name { get; set; }
    public bool IsDisabled { get; set; }
}