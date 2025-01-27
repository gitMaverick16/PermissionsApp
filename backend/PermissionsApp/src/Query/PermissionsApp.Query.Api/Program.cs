using PermissionsApp.Query.Application;
using PermissionsApp.Query.Application.Common.Interfaces;
using PermissionsApp.Query.Infrastructure;
using PermissionsApp.Query.Infrastructure.Configurations;
using PermissionsApp.Query.Infrastructure.Permissions.Persistence;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager Configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddProblemDetails();

builder.Services.Configure<ElasticSettings>(options =>
    Configuration.GetSection("ElasticSettings").Bind(options));

builder.Services.AddScoped<ISeedPermissions, SeedPermissions>();

builder.Services.AddApplication()
    .AddInfrastructure();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var seedPermissions = scope.ServiceProvider.GetRequiredService<ISeedPermissions>();
    await seedPermissions.SeedPermission();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
