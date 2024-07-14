using System;
using System.Collections.Generic;
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
    public string? Description { get; private set; }
    
    [Required]
    public decimal Price { get; private set; }
    
    public bool IsDeleted { get; set; }

    public ICollection<RenovationWorkRepairRequest> RenovationWorkRepairRequests { get; set; } = new List<RenovationWorkRepairRequest>();


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
        const int nameMaxLength = 700;
        const int nameMinLength = 3;

        if (string.IsNullOrEmpty(name) || name.Length < nameMinLength || name.Length > nameMaxLength)
            throw new ArgumentException($"Название инструмента должно быть от {nameMinLength} до {nameMaxLength}.");

        Name = name;
    }
    
    private void SetDescription(string description)
    {
        const int descriptionMaxLength = 2000;

        if (description.Length > descriptionMaxLength)
            throw new ArgumentException($"Комментарий не может быть больше {descriptionMaxLength}.");

        Description = description;
    }
    
    private void SetPrice(decimal price)
    {
        if (price <= 0)
            throw new ArgumentException($"Цена не может быть меньше 0.");

        Price = price;
    }
}