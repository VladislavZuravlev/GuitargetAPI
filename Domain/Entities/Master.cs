using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Master
{
    [Key] 
    public int EmployeeId { get; init; }
    
    [ForeignKey("EmployeeId")]
    [Required]
    public Employee Employee { get; init; }
    
    public decimal Percent { get; private set; }

    public bool IsDisabled { get; private set; }
    

    public ICollection<RepairRequest> Repairs { get; set; } = new List<RepairRequest>();



    public Master()
    {
        
    }

    private Master(int employeeId, decimal percent)
    {
        EmployeeId = employeeId;
        SetPercent(percent);
    }


    public static Master Create(int employeeId, decimal percent)
    {
        if (employeeId <= 0) throw new ArgumentException("Сотрудник не найден.");
        
        return new Master(employeeId, percent);
    }


    private void SetPercent(decimal percent)
    {
        if (percent <= 0)
            throw new ArgumentException("Процент не может быть меньше 0");

        Percent = percent;

    }
    
}