using System.ComponentModel.DataAnnotations;

namespace Application.Models.RequestModels.Employee;

public class EmployeeRegisterModel
{
    [MaxLength(18)]
    [MinLength(18)]
    [Required]
    public string PhoneNumber { get; set; } = string.Empty;
    
    [MaxLength(300)]
    [Required]
    public string Name { get; set; } = string.Empty;

    [MinLength(8)]
    [Required]
    public string Password { get; set; } = string.Empty;
}