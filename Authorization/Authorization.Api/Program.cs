using Authorization.Repository.Context;
using Authorization.Repository.Interfaces;
using Authorization.Repository.Repositories;
using Authorization.Service;
using Authorization.Service.Interfaces;
using Authorization.Service.Services;
using Infrastructure.DTO;
using Infrastructure.Extensions;
using Infrastructure.HelperModels;
using Infrastructure.Interfaces;
using Infrastructure.Middlewares;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegistrationDbContext<UserContext>(builder.Configuration);
await builder.Configuration.AddConfigurationApiSource(builder.Configuration);
builder.Services.Configure<JwtOptions>(
    builder.Configuration.GetSection("JwtOptions"));
builder.Services.SetJwtOptions(builder.Configuration);
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
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services
    .AddScoped<IUnitOfWork, UnitOfWork>()
    .AddScoped<IUserService, UserService>();
builder.Services.AddScoped<UserSession>();
builder.Services.AddScoped<IUserSessionGetter>(serv => serv.GetRequiredService<UserSession>());
builder.Services.AddScoped<IUserSessionSetter>(serv => serv.GetRequiredService<UserSession>());

builder.Services.AddAutoMapper(typeof(MappingProfile));


var app = builder.Build();
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseRouting();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseStatusCodePages();
app.UseMiddleware<AuthentificationMiddleware>();
app.UseMiddleware<ExceptionMiddleware>();
app.MapControllers();
app.Run();
