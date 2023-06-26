using TransactionsSystem.Repository.Context;
using Infrastructure.Extensions;
using Infrastructure.DTO;
using Infrastructure.Interfaces;
using TransactionsSystem.Repository.Interfaces;
using TransactionsSystem.Repository.Repositories;
using TransactionsSystem.Service.Interfaces;
using TransactionsSystem.Service.Service;
using TransactionsSystem.Service;
using Infrastructure.Middlewares;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegistrationDbContext<TransactionContext>(builder.Configuration);
await builder.Configuration.AddConfigurationApiSource(builder.Configuration);
builder.Services.AddControllers();
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
    .AddScoped<ITypeTransactionService, TypeTransactionService>()
    .AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<UserSession>();
builder.Services.AddScoped<IUserSessionGetter>(serv => serv.GetRequiredService<UserSession>());
builder.Services.AddScoped<IUserSessionSetter>(serv => serv.GetRequiredService<UserSession>());

builder.Services.AddAutoMapper(typeof(MappingProfile));
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseMiddleware<AuthentificationMiddleware>();
app.UseMiddleware<ExceptionMiddleware>();
app.MapControllers();

app.Run();
