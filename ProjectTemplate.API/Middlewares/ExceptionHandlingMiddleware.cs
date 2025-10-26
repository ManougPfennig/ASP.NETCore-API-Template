using System;
using System.Net;
using ProjectTemplate.Domain.Exceptions;

namespace ProjectTemplate.API.Middlewares;


/// <summary>
/// RFC-7807 compliant request response middleware.
/// </summary>
/// <param name="next"></param>
/// <param name="logger"></param>
public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
	private readonly RequestDelegate _next = next;
	private readonly ILogger<ExceptionHandlingMiddleware> _logger = logger;

	public async Task InvokeAsync(HttpContext context)
	{
		try
		{
			await _next(context);
		}
		catch (Exception ex)
		{
			await HandleExceptionAsync(context, ex, _logger);
		}
	}

	private static async Task HandleExceptionAsync(HttpContext context, Exception ex, ILogger logger)
	{
		int status;
		string title;

		switch (ex)
		{
			case ExceptionBase appEx:
				status = appEx.StatusCode;
				title = appEx.Message;
				logger.LogWarning(ex, "Handled application exception: {Message}", ex.Message);
				break;

			default:
				status = (int)HttpStatusCode.InternalServerError;
				title = "An unexpected error occurred.";
				logger.LogError(ex, "Unhandled exception.");
				break;
		}

		context.Response.StatusCode = status;
		context.Response.ContentType = "application/problem+json";

		var problem = new
		{
			type = $"https://httpstatuses.com/{status}",
			title,
			status,
			traceId = context.TraceIdentifier
		};

		await context.Response.WriteAsJsonAsync(problem);
	}
}