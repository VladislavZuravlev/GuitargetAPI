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

    public string PasswordHash { get; private set; }
    
    public bool IsDisabled { get; private set; }

    public ICollection<RepairRequest> Repairs { get; set; } = new List<RepairRequest>();

    public ICollection<EmployeeRoles> Roles { get; set; } = new List<EmployeeRoles>();

    
    public Employee()
    {
        
    }
    
    private Employee(string name, string phoneNumber, string passwordHash)
    {
        SetName(name);
        SetPhoneNUmber(phoneNumber);
        PasswordHash = passwordHash;
        IsDisabled = false;
    }

    public static Employee Create(string name, string phoneNumber, string passwordHash)
    {
        return new Employee(name, phoneNumber, passwordHash);
    }
    
    private void SetPhoneNUmber(string phone)
    {
        const int requiredPhoneLength = 18;
        
        if (string.IsNullOrEmpty(phone) || phone.Length != requiredPhoneLength) 
            throw new ArgumentException("Телфефон должен быть указан в формате \"+7 (000) 000-00-00\".");
        
        PhoneNumber = phone;
    }

    private void SetName(string name)
    {
        const int nameMaxLength = 300;
        
        if (string.IsNullOrEmpty(name) || name.Length > nameMaxLength)
            throw new ArgumentException($"Имя не может быть пустым и содержать более {nameMaxLength} символов.");

        Name = name;
    }

}