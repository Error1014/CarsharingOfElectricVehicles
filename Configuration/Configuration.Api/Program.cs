using Configuration.Repository.Context;
using Configuration.Repository.Interfaces;
using Configuration.Repository.Repositories;
using Configuration.Service;
using Infrastructure.DTO;
using Infrastructure.Extensions;
using Infrastructure.HelperModels;
using Infrastructure.Interfaces;
using Infrastructure.Middlewares;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.RegistrationDbContext<ConfigurationContext>(builder.Configuration);
var configuration = builder.Configuration;
builder.Host
       .ConfigureAppConfiguration((hostingContext, config) =>
       {
           config.AddEfConfiguration(
               options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
       });
builder.Services
    .AddScoped<IUnitOfWork, UnitOfWork>()
    .AddScoped<IConfigurationService, ConfigurationService>();
builder.Services.AddScoped<UserSession>();
builder.Services.AddScoped<IUserSessionGetter>(serv => serv.GetRequiredService<UserSession>());
builder.Services.AddScoped<IUserSessionSetter>(serv => serv.GetRequiredService<UserSession>());

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
app.UseMiddleware<AuthenticationMiddleware>();
app.UseMiddleware<ExceptionMiddleware>();
app.Run();
