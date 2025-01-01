using Aspire.AspireServiceDefault.Helpers;
using BetaHub.Auth.Middleware;
using BetaHub.Auth.Service.Application;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog logging
builder.Host.ConfigSeriLog();

// Add services to the container.
builder.AddServiceDefaults();
builder.Services.AddInfraServices(builder.Configuration);
builder.Services.AddControllers();

// configuring Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddHangfireConfig(builder.Configuration);

var app = builder.Build();

app.MapDefaultEndpoints();

// Use custom exception handling middleware
app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthorization();

//app.UseHangfireDashboard();

app.MapGroup("/identity").MapIdentityApi<IdentityUser>();

app.MapControllers();

app.Run();
