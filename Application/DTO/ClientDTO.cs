﻿namespace Application.DTO;

public class ClientDTO
{
    public int Id { get; init; }
    public string PhoneNumber { get; set; }
    public string Name { get; set; }
    public DateTime CreateDateTime { get; init; }
    public List<RepairDTO> Repairs { get; set; }
}