using System.Text;
using Application.Helpers;
using Application.Helpers.JWT;
using Application.IRepositories;
using Application.IServices;
using Application.Services;
using Infrastructure.Contexts;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;


services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));
services.AddControllers();

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

services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
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
                context.Token = context.Request.Headers["Guitarget"];
                return Task.CompletedTask;
            }
        };
    });

//services.AddAuthorization(policy => policy.AddPolicy(CustomAuthorizationFilter));

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

app.UseCors(x =>
{
    x.WithHeaders().AllowAnyHeader().AllowCredentials();
    x.WithOrigins("http://localhost:3000");
    x.WithMethods().AllowAnyMethod();
});

app.Run();