using System.ComponentModel.DataAnnotations;

namespace Application.Models.RequestModels.Repair;

public class AddRepairModel
{
    [Required]
    public DateTime ProvisionalDateOfReceipt  { get; set; }
    
    [MinLength(3)]
    [MaxLength(500)]
    [Required]
    public string InstrumentName { get; set; }
    public bool IsCase { get; set; }
    
    [Required]
    [MaxLength(1000)]
    public string Description { get; set; }
    
    [Required]
    public decimal Price { get; set; }
    
    [Required]
    public int ClientId { get; set; }
    
    [Required]
    public int MasterId { get; set; }
    
    [Required]
    public int EmployeeId { get; init; }
    
    [Required]
    public int RenovationWorkId { get; set; }
}