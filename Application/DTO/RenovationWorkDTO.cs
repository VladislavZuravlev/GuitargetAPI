﻿using System.Collections.Generic;

namespace Application.DTO;

public class RenovationWorkDTO
{
    public int Id { get; init; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public bool IsDeleted { get; set; }
    public List<RenovationWorkRepairRequestDTO>? RenovationWorkRepairRequests { get; set; }
}