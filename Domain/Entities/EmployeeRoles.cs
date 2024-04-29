using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class EmployeeRoles
{
    [Required]
    public int EmployeeId { get; init; }
    
    [Required]
    public byte RoleId { get; set; }

    [Required]
    [ForeignKey("EmployeeId")]
    public Employee Employee { get; set; }
}