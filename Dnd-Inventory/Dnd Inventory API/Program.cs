using Dnd_Inventory_API.authorization;
using Dnd_Inventory_API.WebSocket;
using Dnd_Inventory_DAL;
using Dnd_Inventory_DAL.Repositiories;
using Dnd_Inventory_Logic.Interfaces.Repositories;
using Dnd_Inventory_Logic.Interfaces.Services;
using Dnd_Inventory_Logic.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Security.Claims;

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

if (builder.Environment.EnvironmentName == "Testing")
{
    builder.Services.AddDbContext<SessionDbContext>(
    options =>
    options.UseMySQL(configuration.GetConnectionString("Test"))
    );
    Console.WriteLine("RUNNING TESTING DATABASE: " + configuration.GetConnectionString("Test"));
}
else
{
    builder.Services.AddDbContext<SessionDbContext>(
    options =>
    options.UseMySQL(configuration.GetConnectionString("Default"))
    );
}
// logic layer
builder.Services.AddScoped<ISessionService, SessionService>();
builder.Services.AddScoped<IInventoryService, InventoryService>();
builder.Services.AddScoped<IItemService, ItemService>();

// dal layer
builder.Services.AddScoped<ISessionRepository, SessionRepository>();
builder.Services.AddScoped<IJoinKeyRepository, JoinKeyRepository>();
builder.Services.AddScoped<ISessionUsersRepository, SessionUsersRepository>();
builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();

// Websocket
builder.Services.AddSignalR();
builder.Services.AddScoped<ISignalRHubService, SignalRHubService>();

// 1. Add Authentication Services
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.Authority = $"http://{builder.Configuration["Auth0:Domain"]}/";
    options.Audience = builder.Configuration["Auth0:Audience"];
    options.TokenValidationParameters = new TokenValidationParameters
    {
        NameClaimType = ClaimTypes.NameIdentifier
    };
    options.RequireHttpsMetadata = false;
});

builder.Services
  .AddAuthorization(options =>
  {
      options.AddPolicy(
        "read:sessions",
        policy => policy.Requirements.Add(
          new HasScopeRequirement("read:sessions", builder.Configuration["Auth0:Domain"])
        )
      );
      options.AddPolicy(
        "read:items",
        policy => policy.Requirements.Add(
            new HasScopeRequirement("read:items", builder.Configuration["Auth0:Domain"])
        )
      );
      options.AddPolicy(
        "read:inventory",
        policy => policy.Requirements.Add(
            new HasScopeRequirement("read:inventory", builder.Configuration["Auth0:Domain"])
        )
      );
  });

IdentityModelEventSource.ShowPII = true;

builder.Services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("Default", policy =>
    {
        policy.WithOrigins(builder.Configuration["Cors:Default"])
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("Default");

app.UseAuthentication();
app.UseAuthorization();

app.UseWebSockets();

app.MapHub<InventoryHub>("/hub/inventory").RequireCors(x => x
           .AllowAnyMethod()
           .AllowAnyHeader()
           .SetIsOriginAllowed(origin => true)
           .AllowCredentials());

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var service = scope.ServiceProvider;
    var context = service.GetService<SessionDbContext>();

    if (context is not null)
        context.Database.EnsureCreated();
}

app.Run();

namespace Dnd_Inventory_API
{
    public class DndInventoryApi { }
}