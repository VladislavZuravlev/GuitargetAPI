using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Application.DTO;

namespace Application.Models.RequestModels.Repair;

public class AddRepairRequestModel
{
    [Required]
    public DateTime ProvisionalDateOfReceipt  { get; set; }
    
    [MinLength(3)]
    [MaxLength(500)]
    [Required]
    public string InstrumentName { get; set; }
    public bool IsCase { get; set; }
    
    
    [MaxLength(1000)]
    public string Description { get; set; } = string.Empty;
    
    [Required]
    public decimal Price { get; set; }
    
    [Required]
    public int ClientId { get; set; }
    
    [Required]
    public int MasterId { get; set; }
    
    [Required]
    public int EmployeeId { get; init; }
    
    [Required] 
    public IEnumerable<int> RenovationWorkIds { get; set; }
}