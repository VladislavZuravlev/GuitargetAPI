using System;

namespace Application.DTO;

public class RenovationWorkRepairRequestDTO
{
    public int RepairRequestId { get; set; }

    public int RenovationWorkId { get; set; }

    public DateTime DateAdded { get; set; }

    public decimal Amount { get; set; }
    
    public RepairRequestDTO? RepairRequest { get; set; }
    
    public RenovationWorkDTO? RenovationWork { get; set; }
}