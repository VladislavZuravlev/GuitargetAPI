﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enums;

namespace Domain.Entities;

public class Repair
{
    [Key]
    public int Id { get; init; }
    
    [Required]
    public DateTime CreateDateTime { get; init; } = DateTime.UtcNow;
    
    [Required]
    public DateTime ProvisionalDateOfReceipt  { get; private set; }

    public DateTime? DateOfReceipt { get; private set; }
    
    [MinLength(3)]
    [MaxLength(500)]
    [Required]
    public string InstrumentName { get; private set; } = string.Empty;
    public bool IsCase { get; private set; }
    
    [Required]
    [MaxLength(1000)]
    public string Description { get; private set; } = string.Empty;
    public decimal Price { get; private set; }
    public byte StatusId { get; private set; }
    public bool IsDeleted { get; private set; }
    public int ClientId { get; private set; }
    public int MasterId { get; private set; }
    public int EmployeeId { get; init; }
    public int RenovationWorkId { get; set; }
    
    [Required]
    [ForeignKey("ClientId")]
    public Client Client { get; private set; }
    
    [Required]
    [ForeignKey("MasterId")]
    public EmployeeMaster Master { get; private set; }
    
    [Required]
    [ForeignKey("EmployeeId")]
    public Employee Employee { get; private set; }

    [Required]
    [ForeignKey("RenovationWorkId")]
    public RenovationWork RenovationWork { get; set; }

    

    
    
    
    
    
    
    
    public Repair()
    {
        
    }

    private Repair(int clientId, int masterId, int employeeId, DateTime workEndDate, string instrumentName, bool isCase, string description, decimal price, int renovationWorkId)
    {
        IsCase = isCase;
        StatusId = (byte)RepairStatusType.New;
        ClientId = clientId;
        EmployeeId = employeeId;
        MasterId = masterId;
        RenovationWorkId = renovationWorkId;
        
        
        SetProvisionalDateOfReceipt(workEndDate);
        SetInstrumentName(instrumentName);
        SetDescription(description);
        SetPrice(price);
    }


    public static Repair Create(int clientId, int masterId, int employeeId, DateTime workEndDate, string instrumentName, bool isCase, string description, decimal price, int renovationWorkId)
    {
        return new Repair(clientId, masterId, employeeId, workEndDate, instrumentName, isCase, description, price, renovationWorkId);
    }
    

    private void SetProvisionalDateOfReceipt(DateTime provisionalDateOfReceiptUtc)
    {
        if (DateTime.UtcNow > provisionalDateOfReceiptUtc)
            throw new ArgumentException("Предварительная дата получения не может быть меньше текущей даты.");

        ProvisionalDateOfReceipt = provisionalDateOfReceiptUtc;
    }

    private void SetDescription(string description)
    {
        const int descriptionMaxLength = 1000;
        
        if (description.Length > descriptionMaxLength)
            throw new ArgumentException($"Комментарий не может быть больше {descriptionMaxLength}.");

        Description = description;
    }
    
    private void SetInstrumentName(string instrumentName)
    {
        const int nameMaxLength = 500;
        
        if (string.IsNullOrEmpty(instrumentName) || instrumentName.Length > nameMaxLength)
            throw new ArgumentException($"Название инструмента не может быть пустым и содержать более {nameMaxLength} символов.");

        InstrumentName = instrumentName;
    }

    private void SetPrice(decimal price)
    {
        if (price <= 0) 
            throw new ArgumentException("Сумма не может быть меньше либо равной 0.");

        Price = price;
    }
    
    
    
    
}