using Cars.Repository.Context;
using Cars.Repository.Interfaces;
using Cars.Repository.Repositories;
using Cars.Service;
using Cars.Service.Interfaces;
using Cars.Service.Services;
using Infrastructure.DTO;
using Infrastructure.Extensions;
using Infrastructure.HelperModels;
using Infrastructure.Interfaces;
using Infrastructure.Middlewares;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.RegistrationDbContext<CarContext>(builder.Configuration);
await builder.Configuration.AddConfigurationApiSource(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services
    .AddScoped<IUnitOfWork, UnitOfWork>()
    .AddScoped<IBrandModelService, BrandModelService>()
    .AddScoped<ICarService, CarService>()
    .AddScoped<ICharacteristicService, CharacteristicService>();
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

app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<AuthentificationMiddleware>();
app.UseMiddleware<ExceptionMiddleware>();
app.UseStatusCodePages();
app.MapControllers();
app.Run();
