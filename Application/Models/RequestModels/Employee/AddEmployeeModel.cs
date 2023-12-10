using System.ComponentModel.DataAnnotations;

namespace Application.Models.RequestModels.Employee;

public class AddEmployeeModel
{
    [MaxLength(18)]
    [MinLength(18)]
    [Required]
    public string PhoneNumber { get; set; } = string.Empty;
    
    [MaxLength(300)]
    [Required]
    public string Name { get; set; } = string.Empty;
}