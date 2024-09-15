using BetaHub.Auth.Helpers;
using BetaHub.Auth.Middleware;
using BetaHub.Auth.Service.Application;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog logging
builder.Host.AddLogConfig();

// Add services to the container.
builder.Services.AddInfraServices(builder.Configuration);
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHealthChecks()
	.AddSqlServer(builder.Configuration.GetConnectionString("Database")!, tags: ["sql-server"]);
builder.Services.AddHealthChecksUI(opt =>
	{
		//opt.AddHealthCheckEndpoint("sql-server", "/api/_healthz");
		//opt.MaximumHistoryEntriesPerEndpoint(50);
	})
	.AddInMemoryStorage();

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
app.MapHealthChecks("/api/_healthz", new HealthCheckOptions
{
	Predicate = _ => true,
	ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
app.UseHealthChecksUI(options =>
{
	options.UIPath = "/healthcheck";
	//options.AddCustomStylesheet("./HealthCheck/Custom.css");
});

app.UseAuthorization();

app.MapControllers();

app.Run();
