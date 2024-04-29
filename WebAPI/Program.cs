using System.Text;
using System.Threading.Tasks;
using Application.Helpers;
using Application.Helpers.JWT;
using Application.IRepositories;
using Application.IServices;
using Application.Services;
using Infrastructure.Contexts;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

// Add services to the container.
services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));
services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddScoped<IClientService, ClientService>();
services.AddScoped<IClientRepository, ClientRepository>();

services.AddScoped<IRepairRequestsService, RepairRequestService>();
services.AddScoped<IRepairRequestsRepository, RepairRequestRepository>();

services.AddScoped<IRenovationWorkService, RenovationWorkService>();
services.AddScoped<IRenovationWorkRepository, RenovationWorkRepository>();

services.AddScoped<IMasterService, MasterService>();
services.AddScoped<IMasterRepository, MasterRepository>();

services.AddScoped<IEmployeeService, EmployeeService>();
services.AddScoped<IEmployeeRepository, EmployeeRepository>();

services.AddScoped<IPasswordHasher, PasswordHasher>();
services.AddScoped<IJwtProvider, JwtProvider>();

services.AddDbContext<PostgresDbContext>();

services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtOptions:SecretKey"]))
        };

        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                context.Token = context.Request.Cookies["Guitarget"];
                return Task.CompletedTask;
            }
        };
    });

// services.AddAuthorization(options =>
// {
//     options.AddPolicy("AddminPolicy", policy =>
//     {
//         
//     });
// });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    HttpOnly = HttpOnlyPolicy.Always,
    Secure = CookieSecurePolicy.Always
});

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();