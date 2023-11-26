using System.ComponentModel.DataAnnotations;

namespace Application.Models.RequestModels.Client;

public class AddClientModel
{
    [MaxLength(18)]
    [MinLength(18)]
    [Required]
    public string PhoneNumber { get; set; }
    
    [MaxLength(300)]
    [MinLength(3)]
    [Required]
    public string Name { get; set; }
}