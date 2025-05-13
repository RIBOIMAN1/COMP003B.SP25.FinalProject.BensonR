using System.Diagnostics;
namespace COMP003B.SP25.FinalProject.BensonR.Middleware
{
	public class RequestTimingMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<RequestTimingMiddleware> _logger;
		public RequestTimingMiddleware(RequestDelegate next, ILogger<RequestTimingMiddleware> logger)
		{
			_next = next;
			_logger = logger;
		}
		public async Task InvokeAsync(HttpContext context)
		{
			var stopwatch = Stopwatch.StartNew();
			_logger.LogInformation($"[Request] {context.Request.Method} {context.Request.Path}");
			await _next(context);
			stopwatch.Stop();
			_logger.LogInformation($"[Response] {context.Response.StatusCode} | Processing Time: {stopwatch.ElapsedMilliseconds}ms");
		}
	}
}