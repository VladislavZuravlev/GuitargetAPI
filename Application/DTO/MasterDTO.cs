using System.Collections.Generic;
using Domain.Entities;

namespace Application.DTO;

public class MasterDTO
{
    public int EmployeeId { get; init; }
    public EmployeeDTO Employee { get; init; }
    public decimal Percent { get; set; }
    public bool IsDisabled { get; set; }
    public int RepairsCount { get; set; }
    public IEnumerable<RepairRequestDTO>? Repairs { get; set; }
}