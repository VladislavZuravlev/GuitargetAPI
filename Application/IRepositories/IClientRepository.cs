using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Models;
using Domain.Entities;

namespace Application.IRepositories;

public interface IClientRepository
{
    Task<OperationResult> AddAsync(Client newClient);
    Task<List<Client>> GetAsync(IEnumerable<Tuple<string, string, object>>? filters = null, string? includeProperties = null, Dictionary<string, string>? orderCollection = null);
}