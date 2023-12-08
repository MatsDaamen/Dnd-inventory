using Dnd_Inventory_DAL;
using Dnd_Inventory_DAL.Repositiories;
using Dnd_Inventory_Logic.Interfaces.Repositories;
using Dnd_Inventory_Logic.Interfaces.Services;
using Dnd_Inventory_Logic.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// database context
IConfigurationRoot configuration = new ConfigurationBuilder()
    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
    .AddJsonFile("appsettings.json")
    .Build();

builder.Services.AddDbContext<SessionDbContext>(
        options =>
        options.UseMySQL(configuration.GetConnectionString("Default"))
        );

// logic layer
builder.Services.AddScoped<ISessionService, SessionService>();

// dal layer
builder.Services.AddScoped<ISessionRepository, SessionRepository>();
builder.Services.AddScoped<IJoinKeyRepository, JoinKeyRepository>();
builder.Services.AddScoped<ISessionUsersRepository, SessionUsersRepository>();

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

using (var scope = app.Services.CreateScope())
{
    var service = scope.ServiceProvider;
    var context = service.GetService<InventoryDbContext>();

    context.Database.EnsureCreated();
}

app.Run();

namespace Dnd_Inventory_API
{
    public class dndInventoryApi { }
}