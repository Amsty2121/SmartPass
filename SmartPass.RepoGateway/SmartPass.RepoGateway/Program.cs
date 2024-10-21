using Application.IoC;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartPass.RepoGateway.Constants;
using SmartPass.RepoGateway.Extensions;
using SmartPass.Repository.Contexts;
using SmartPass.Repository.IoC;
using SmartPass.Repository.Models.Entities;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString(SettingsConstants.SmartPassDb);

builder.Services.AddDbContext<SmartPassContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddOptions();

builder.Services.AddJwtAuth(builder.Configuration);

builder.Services.AddRepositoriesExtension();
builder.Services.AddServicesExtension();


// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//Apply migrations
//Add-Migration InitialCreate -Context SmartPassContext -OutputDir Migrations\SmartPassContextContextMigrations
using (var scope = app.Services.GetService<IServiceScopeFactory>().CreateScope())
{
    //scope.ServiceProvider.GetRequiredService<IdentityContext>().Database.Migrate();
    scope.ServiceProvider.GetRequiredService<SmartPassContext>().Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //app.SeedData();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
