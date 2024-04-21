using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Admin
{
    [Key] 
    public int EmployeeId { get; init; }
    
    [ForeignKey("EmployeeId")]
    [Required]
    public Employee Employee { get; init; }

    public bool IsDisabled { get; private set; }
}