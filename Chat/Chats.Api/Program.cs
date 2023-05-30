using Chats.Repository.Context;
using Chats.Repository.Interfaces;
using Chats.Repository.Repositories;
using Chats.Service;
using Chats.Service.Interfaces;
using Chats.Service.Service;
using Infrastructure.DTO;
using Infrastructure.Extensions;
using Infrastructure.Interfaces;
using Infrastructure.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegistrationDbContext<ChatContext>(builder.Configuration);
await builder.Configuration.AddConfigurationApiSource(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services
    .AddScoped<IUnitOfWork, UnitOfWork>()
    .AddScoped<IChatService, ChatService>()
    .AddScoped<IMessageService, MessageService>();
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

app.MapControllers();
app.UseMiddleware<AuthenticationMiddleware>();
app.UseMiddleware<ExceptionMiddleware>();
app.Run();
