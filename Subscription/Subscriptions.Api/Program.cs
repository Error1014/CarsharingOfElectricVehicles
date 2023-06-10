using Subscriptions.Repository.Context;
using Infrastructure.Extensions;
using Microsoft.OpenApi.Models;
using Infrastructure.HelperModels;
using Infrastructure.Middlewares;
using Infrastructure.DTO;
using Infrastructure.Interfaces;
using Subscriptions.Repository.Repositories;
using Subscriptions.Repository.Interfaces;
using Subscriptions.Service.Interfaces;
using Subscriptions.Service.Services;
using Subscriptions.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.RegistrationDbContext<SubscriptionContext>(builder.Configuration);

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
    .AddScoped<IClientSubscriptionService, ClientSubscriptionService>()
    .AddScoped<ISubscriptionService, SubscriptionService>();
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

app.UseAuthorization();
app.UseMiddleware<AuthenticationMiddleware>();
app.UseMiddleware<ExceptionMiddleware>();
app.MapControllers();

app.Run();
