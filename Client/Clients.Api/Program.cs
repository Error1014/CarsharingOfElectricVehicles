using Clients.Repository.Context;
using Clients.Repository.Interfaces;
using Clients.Repository.Repositories;
using Clients.Service;
using Clients.Service.Interfaces;
using Clients.Service.Services;
using Infrastructure.DTO;
using Infrastructure.Extensions;
using Infrastructure.HelperModels;
using Infrastructure.Interfaces;
using Infrastructure.Middlewares;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.RegistrationDbContext<ClientContext>(builder.Configuration);

await builder.Configuration.AddConfigurationApiSource(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services
    .AddScoped<IUnitOfWork, UnitOfWork>()
    .AddScoped<IClientService, ClientService>()
    .AddScoped<IDrivingLicenseService, DrivingLicenseService>()
    .AddScoped<IPassportService, PassportService>();
builder.Services.AddScoped<UserSession>();
builder.Services.AddScoped<IUserSessionGetter>(serv => serv.GetRequiredService<UserSession>());
builder.Services.AddScoped<IUserSessionSetter>(serv => serv.GetRequiredService<UserSession>());

builder.Services.AddAutoMapper(typeof(MappingProfile));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<AuthenticationMiddleware>();
app.UseMiddleware<ExceptionMiddleware>();
app.UseStatusCodePages();
app.MapControllers();
app.Run();
