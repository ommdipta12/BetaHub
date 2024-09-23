using BetaHub.Auth.Helpers;
using BetaHub.Auth.Middleware;
using BetaHub.Auth.Service.Application;
using Hangfire;
using Hangfire.SqlServer;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using OpenTelemetry.Metrics;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog logging
builder.Host.AddLogConfig();

// Add services to the container.
builder.Services.AddInfraServices(builder.Configuration);
builder.Services.AddControllers();

// configuring Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Hangfire
builder.Services.AddHangfire(configuration => configuration
			.UseSimpleAssemblyNameTypeSerializer()
			.UseRecommendedSerializerSettings()
			.UseSqlServerStorage(builder.Configuration.GetConnectionString("HangfireDatabase"),
			new SqlServerStorageOptions
			{
				CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
				SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
				QueuePollInterval = TimeSpan.Zero,
				UseRecommendedIsolationLevel = true,
				DisableGlobalLocks = true
			}));
builder.Services.AddHangfireServer();

//Health check
builder.Services.AddHealthChecks()
	.AddSqlServer(builder.Configuration.GetConnectionString("Database")!, tags: ["sql-server"]);
builder.Services.AddHealthChecksUI(opt =>
	{
		//opt.AddHealthCheckEndpoint("sql-server", "/api/_healthz");
		//opt.MaximumHistoryEntriesPerEndpoint(50);
	})
	.AddInMemoryStorage();

//OpenTelemetry
builder.Services.AddOpenTelemetry()
	.WithMetrics(o =>
	{
		o.AddPrometheusExporter();
		o.AddMeter(
			"Microsoft.AspNetCore.Hosting",
			"Microsoft.AspNetCore.Server.Kestrel");

		o.AddAspNetCoreInstrumentation()
		.AddHttpClientInstrumentation()
		.AddRuntimeInstrumentation()
		.AddPrometheusExporter();

	});

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
app.UseOpenTelemetryPrometheusScrapingEndpoint();

app.UseAuthorization();

app.UseHangfireDashboard();

app.MapControllers();

app.Run();
