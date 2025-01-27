using PermissionsApp.Command.Application;
using PermissionsApp.Command.Infrastructure;
using PermissionsApp.Command.Infrastructure.Configurations;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager Configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddProblemDetails();

builder.Services.Configure<KafkaSettings>(options =>
    Configuration.GetSection("KafkaSettings").Bind(options));

builder.Services.AddApplication()
    .AddInfrastructure(Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy",
        policy => policy.WithOrigins("http://localhost:3000")
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

var app = builder.Build();

app.UseCors("FrontendPolicy");

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
