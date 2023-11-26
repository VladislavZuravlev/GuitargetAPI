using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Employee
{
    [Key]
    public int Id { get; init; }
    
    [MaxLength(18)]
    [MinLength(18)]
    [Required]
    public string PhoneNumber { get; private set; } = string.Empty;
    
    [MaxLength(300)]
    [Required]
    public string Name { get; private set; } = string.Empty;
    
    public bool IsDisabled { get; private set; }

    public ICollection<Repair> Repairs { get; set; } = new List<Repair>();

    
    
    
    public Employee()
    {
        
    }
    
    private Employee(string name, string phoneNumber)
    {
        SetName(name);
        SetPhoneNUmber(phoneNumber);
        IsDisabled = false;
    }

    public static Employee Create(string name, string phoneNumber)
    {
        return new Employee(name, phoneNumber);
    }
    
    private void SetPhoneNUmber(string phone)
    {
        const int requiredPhoneLength = 18;
        
        if (string.IsNullOrEmpty(phone) || phone.Length != requiredPhoneLength) return;
        
        PhoneNumber = phone;
    }

    private void SetName(string name)
    {
        if (string.IsNullOrEmpty(name)) return;

        Name = name;
    }

}