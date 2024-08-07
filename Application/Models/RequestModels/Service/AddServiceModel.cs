﻿using System.ComponentModel.DataAnnotations;

namespace Application.Models.RequestModels.Service;

public class AddServiceModel
{
    [Required]
    [MaxLength(700)]
    public string Name { get; set; }
    
    [MaxLength(2000)]
    public string? Description { get; set; }
    
    [Required]
    public decimal Price { get; set; }
}