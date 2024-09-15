using Serilog;
using System.Net;

namespace BetaHub.Auth.Middleware
{
	public class GlobalExceptionHandlerMiddleware
	{
		private readonly RequestDelegate _next;
		public GlobalExceptionHandlerMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				// Call the next middleware in the pipeline
				await _next(context);
			}
			catch (Exception ex)
			{
				// Log the exception
				Log.Error(ex, $"-> {context.Request.Path}");

				// Handle the exception
				await HandleExceptionAsync(context, ex);
			}
		}

		private Task HandleExceptionAsync(HttpContext context, Exception exception)
		{
			// Set the status code based on the exception type (you can customize this)
			context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
			context.Response.ContentType = "application/json";

			// Customize the response message (you can add more details)
			var response = new
			{
				success = false,
				message = "An unexpected error occurred. Please try again later.",
				details = exception.Message // Optional: Include this for non-production environments
			};

			// Return the JSON response
			return context.Response.WriteAsJsonAsync(response);
		}
	}
}
