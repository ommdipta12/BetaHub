using BetaHub.Auth.Helpers;
using BetaHub.Auth.Middleware;
using BetaHub.Auth.Service.Application;
using Hangfire;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog logging
builder.Host.AddLogConfig();

// Add services to the container.
builder.Services.AddInfraServices(builder.Configuration);
builder.Services.AddControllers();

// configuring Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddHangfireConfig(builder.Configuration);

//builder.Services.AddMoniotringConfig(builder.Configuration);

var app = builder.Build();

// Use custom exception handling middleware
app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
app.UseHttpsRedirection();
/*app.MapHealthChecks("/api/_healthz", new HealthCheckOptions
{
	Predicate = _ => true,
	ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
app.UseHealthChecksUI(options =>
{
	options.UIPath = "/healthcheck";
	//options.AddCustomStylesheet("./HealthCheck/Custom.css");
});*/
//app.UseOpenTelemetryPrometheusScrapingEndpoint();

app.UseAuthorization();

//app.UseHangfireDashboard();

app.MapIdentityApi<IdentityUser>();

app.MapControllers();

app.Run();
