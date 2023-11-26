using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Contexts;

public class PostgresDbContext: DbContext
{
    private readonly IConfiguration _configuration;


    public DbSet<Client> Clients { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Repair> Repairs { get; set; }
    public DbSet<EmployeeMaster> Masters { get; set; }
    public DbSet<RenovationWork> RenovationWorks { get; set; }
    
    
    
    

    public PostgresDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        
        options.UseNpgsql("Host=localhost;Port=5432;Database=Guitarget;Username=postgres;Password=Moscow74!;", b => b.MigrationsAssembly("WebAPI"));
    }
    
    
        
}