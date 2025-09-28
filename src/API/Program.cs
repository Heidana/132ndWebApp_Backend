using _132ndWebsite.API.Endpoints;
using _132ndWebsite.Infrastructure.Data;
using _132ndWebsite.Infrastructure.Repositories;
using _132ndWebsite.Application.Interfaces;
using _132ndWebsite.Application.Services;
using _132ndWebsite.Infrastructure;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);



// Get the connection string from appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Register the DbContext to use SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Register the repository for dependency injection
builder.Services.AddScoped<ISquadronRepository, SquadronRepository>();

builder.Services.AddScoped<ISquadronService, SquadronService>();



var app = builder.Build();

// 2. Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// 3. Map the squadron endpoints
app.MapSquadronEndpoints();

app.Run();