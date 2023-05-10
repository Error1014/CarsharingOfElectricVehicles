using Infrastructure.DTO;
using Infrastructure.Extensions;
using Infrastructure.HelperModels;
using Infrastructure.Interfaces;
using Infrastructure.Middlewares;
using Microsoft.OpenApi.Models;
using Rents.Repository.Context;
using Rents.Repository.Interfaces;
using Rents.Repository.Repositories;
using Rents.Service;
using Rents.Service.Interfaces;
using Rents.Service.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegistrationDbContext<RentContext>(builder.Configuration);
//await builder.Configuration.AddConfigurationApiSource(builder.Configuration);
//builder.Services.Configure<UriEndPoint>(
//    builder.Configuration.GetSection("AuthorizationService"));
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
builder.Services.AddControllers();

builder.Services
    .AddScoped<IUnitOfWork, UnitOfWork>()
    .AddScoped<ITariffService, TariffService>()
    .AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<UserSession>();
builder.Services.AddScoped<IUserSessionGetter>(serv => serv.GetRequiredService<UserSession>());
builder.Services.AddScoped<IUserSessionSetter>(serv => serv.GetRequiredService<UserSession>());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(MappingProfile));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
//app.UseMiddleware<AuthenticationMiddleware>();
//app.UseMiddleware<ExceptionMiddleware>();
app.MapControllers();

app.Run();
