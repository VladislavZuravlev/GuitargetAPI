using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class EmployeeMaster
{
    [Key] 
    public int EmployeeId { get; init; }
    
    [ForeignKey("EmployeeId")]
    [Required]
    public Employee Employee { get; init; }
    
    public decimal Percent { get; private set; }

    public bool IsDisabled { get; private set; }
    

    public ICollection<Repair> Repairs { get; set; } = new List<Repair>();



    public EmployeeMaster()
    {
        
    }

    private EmployeeMaster(int employeeId, decimal percent)
    {
        EmployeeId = employeeId;
        SetPercent(percent);
    }


    public static EmployeeMaster Create(int employeeId, decimal percent)
    {
        if (employeeId <= 0) throw new ArgumentException();
        
        return new EmployeeMaster(employeeId, percent);
    }


    private void SetPercent(decimal percent)
    {
        if (percent <= 0) return;

        Percent = percent;

    }
    
}