using Infrastructure.Extensions;
using Microsoft.Extensions.Configuration;
using Presentation.Hubs;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add ConnectionString
builder.Services.ConfiguredDbContext(builder.Configuration);

// Add infrastructure services
builder.Services.AddInfrastructureServices();

// Add Azure SignaLR
builder.Services.AddSignalR().AddAzureSignalR(builder.Configuration.GetConnectionString("AzureSignalRConnectionString"));


// Configure CORS to allow frontend to connect
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
            .WithOrigins("http://localhost:4200", "https://salmon-sand-05cbce20f.6.azurestaticapps.net"); 
    });
});


builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(opt =>
    {
        opt.Title = "Scalar API";
        opt.Theme = ScalarTheme.BluePlanet;
    });
}

app.MapHub<ChatHub>("/chathub");

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
