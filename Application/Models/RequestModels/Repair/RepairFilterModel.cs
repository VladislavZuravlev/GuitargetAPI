namespace Application.Models.RequestModels.Repair;

public class RepairFilterModel
{
    public DateTime? ProvisionalDateOfReceipt  { get; set; }
    public string? InstrumentName { get; set; }
    public bool IsCase { get; set; }
    public string? Description { get; set; }
    public decimal? Price { get; set; }
    public string? ClientPhone { get; set; }
    public int? MasterId { get; set; }
    public int? EmployeeId { get; init; }
    public int? RenovationWorkId { get; set; }
}