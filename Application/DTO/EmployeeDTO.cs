using System.Collections.Generic;

namespace Application.DTO;

public class EmployeeDTO
{
    public int Id { get; init; }
    public string PhoneNumber { get; set; }
    public string Name { get; set; }
    public bool IsDisabled { get; set; }
    public IEnumerable<RepairRequestDTO>? Repairs { get; set; }
}