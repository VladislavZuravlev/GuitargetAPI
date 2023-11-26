namespace Application.DTO;

public class EmployeeDTO
{
    public int Id { get; init; }
    public string PhoneNumber { get; set; }
    public string Name { get; private set; }
    public bool IsDisabled { get; set; }
    public List<RepairDTO> Repairs { get; set; }
}