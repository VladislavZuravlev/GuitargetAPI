using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;

namespace Domain.Entities;

public class RenovationWork
{
    [Key]
    public int Id { get; init; }
    
    [Required]
    [MaxLength(700)]
    [MinLength(3)]
    public string Name { get; private set; } = string.Empty;
    
    [MaxLength(2000)]
    public string Description { get; private set; } = string.Empty;
    
    [Required]
    public decimal Price { get; private set; }
    
    public bool IsDeleted { get; private set; }

    public ICollection<Repair> Repairs { get; set; } = new List<Repair>();


    public RenovationWork()
    {
        
    }

    private RenovationWork(string name, string description, decimal price)
    {
        SetName(name);
        SetDescription(description);
        SetPrice(price);
    }


    public static RenovationWork Create(string name, string description, decimal price)
    {
        return new RenovationWork(name, description, price);
    }
    
    private void SetName(string name)
    {
        if (string.IsNullOrEmpty(name) || name.Length < 3 || name.Length > 700) return;

        Name = name;
    }
    
    private void SetDescription(string description)
    {
        if (string.IsNullOrEmpty(description) || description.Length > 2000) return;

        Description = description;
    }
    
    private void SetPrice(decimal price)
    {
        if (price <= 0) return;

        Price = price;
    }
}