using System.ComponentModel.DataAnnotations;

namespace Application.Models.RequestModels.Master;

public class AddMasterModel
{
    [Required]
    public int EmployeeId { get; init; }
    
    [Required]
    public decimal Percent { get; set; }
}