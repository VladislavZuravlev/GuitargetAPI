using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[PrimaryKey(nameof(EmployeeId), nameof(RoleId))]
public class EmployeeRole
{
    [Required]
    public int EmployeeId { get; init; }
    
    [Required]
    public byte RoleId { get; set; }

    [Required]
    [ForeignKey("EmployeeId")]
    public Employee Employee { get; set; }
}