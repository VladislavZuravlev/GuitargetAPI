using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain.Helpers.Enums;

public enum RepairStatusType: byte
{
    [Display(Name = "Новый")]
    New = 0,
    [Display(Name = "В работе")]    
    InProgress = 1,
    [Display(Name = "Готов")]
    Done = 2,
    [Display(Name = "Выдан")]
    Issued = 3
}