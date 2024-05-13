using System.ComponentModel.DataAnnotations;

namespace Application.Models.RequestModels.Employee;

public class LoginModel
{
    [MaxLength(18)]
    [MinLength(18)]
    [Required]
    public string PhoneNumber { get; set; } = string.Empty;
    
    [MinLength(7)]
    [Required]
    public string Password { get; set; } = string.Empty;
}