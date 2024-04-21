using Domain.Helpers;
using Domain.Helpers.Enums;

namespace Application.DTO;

public class RepairRequestDTO
{
    public int Id { get; init; }
    public DateTime CreateDateTime { get; init; }
    public DateTime ProvisionalDateOfReceipt  { get; set; }
    public DateTime? DateOfReceipt { get; set; }
    public string? InstrumentName { get; set; }
    public bool IsCase { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public byte StatusId { get; set; }
    public string? StatusName => ((RepairStatusType)StatusId).GetDisplayName();
    public bool IsDeleted { get; set; }
    public int ClientId { get; set; }
    public int MasterId { get; set; }
    public int EmployeeId { get; init; }
    
    public ClientDTO? Client { get; set; }
    public MasterDTO? Master { get; set; }
    public EmployeeDTO? Employee { get; set; }
    public List<RenovationWorkDTO>? RenovationWorks { get; set; }

}