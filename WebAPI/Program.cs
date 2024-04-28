using Application.IRepositories;
using Application.IServices;
using Application.Services;
using Infrastructure.Contexts;
using Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();

builder.Services.AddScoped<IRepairRequestsService, RepairRequestService>();
builder.Services.AddScoped<IRepairRequestsRepository, RepairRequestRepository>();

builder.Services.AddScoped<IRenovationWorkService, RenovationWorkService>();
builder.Services.AddScoped<IRenovationWorkRepository, RenovationWorkRepository>();

builder.Services.AddScoped<IMasterService, MasterService>();
builder.Services.AddScoped<IMasterRepository, MasterRepository>();

builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();





builder.Services.AddDbContext<PostgresDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();