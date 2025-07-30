using System.Net;
using System.Text.Json;

public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger, IWebHostEnvironment env)
{
	private readonly RequestDelegate _next = next;
	private readonly ILogger<ExceptionHandlingMiddleware> _logger = logger;
	private readonly IWebHostEnvironment _env = env;

	public async Task Invoke(HttpContext context)
	{
		try
		{
			await _next(context);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Unhandled exception occurred.");

			if (IsApiRequest(context.Request))
			{
				context.Response.ContentType = "application/json";
				context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

				var errorResponse = new
				{
					title = "Internal Server Error",
					status = 500,
					detail = _env.IsDevelopment() ? ex.Message : "An unexpected error occurred."
				};

				var json = JsonSerializer.Serialize(errorResponse);
				await context.Response.WriteAsync(json);
			}
			else
			{
				context.Response.Redirect("/Customer/Home/Error");
			}
		}
	}

	private bool IsApiRequest(HttpRequest request)
	{
		return request.Path.StartsWithSegments("/api") ||
			   request.Headers["Accept"].ToString().Contains("application/json");
	}
}
