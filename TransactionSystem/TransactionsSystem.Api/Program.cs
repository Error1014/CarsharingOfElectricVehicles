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

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegistrationDbContext<TransactionContext>(builder.Configuration);
await builder.Configuration.AddConfigurationApiSource(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
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
app.UseMiddleware<AuthenticationMiddleware>();
app.UseMiddleware<ExceptionMiddleware>();
app.MapControllers();

app.Run();
