using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[PrimaryKey(nameof(RepairRequestId), nameof(RenovationWorkId))]
public class RenovationWorkRepairRequest
{
    public int RepairRequestId { get; init; }

    public int RenovationWorkId { get; init; }

    public DateTime DateAdded { get; init; }

    public decimal Amount { get; private set; }

    [Required]
    [ForeignKey("RepairRequestId")]
    public RepairRequest RepairRequest { get; set; }
    
    [Required]
    [ForeignKey("RenovationWorkId")]
    public RenovationWork RenovationWork { get; set; }

    public RenovationWorkRepairRequest() { }

    public RenovationWorkRepairRequest(int renovationWorkId, int repairRequestId, decimal amount)
    {
        RepairRequestId = repairRequestId;
        RenovationWorkId = renovationWorkId;
        DateAdded = DateTime.UtcNow;

        SetAmount(amount);
    }

    public static RenovationWorkRepairRequest Create(int renovationWorkId, int repairRequestId, decimal amount)
    {
        return new RenovationWorkRepairRequest(renovationWorkId, repairRequestId, amount);
    }

    private void SetAmount(decimal amount)
    {
        if (amount < 0)
            throw new ArgumentException("Сумма не может быть меньше 0.");

        Amount = amount;
    }
}