using Application.IoC;
using Microsoft.EntityFrameworkCore;
using SmartPass.RepoGateway.Constants;
using SmartPass.RepoGateway.Extensions;
using SmartPass.Repository.Contexts;
using SmartPass.Repository.IoC;
using SmartPass.Services.Extensions;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString(SettingsConstants.SmartPassDb);

builder.Services.AddDbContext<SmartPassContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddOptions();

builder.Services.AddJwtAuthentication(
    builder.Services.ConfigureAuthOptions(builder.Configuration));

builder.Services.AddJwtAuthorization();

builder.Services.AddRepositoriesExtension();
builder.Services.AddServicesExtension();


// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//Generate migrations
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
    app.SeedData();
}



app.UseHttpsRedirection();

app.UseCors("AllowAllOrigins");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
