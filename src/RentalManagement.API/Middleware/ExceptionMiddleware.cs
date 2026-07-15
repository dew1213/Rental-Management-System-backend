using System.Net;
using System.Text;
using System.Text.Json;

namespace RentalManagement.API.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly IWebHostEnvironment _env;

    public ExceptionMiddleware(
        RequestDelegate next,
        ILogger<ExceptionMiddleware> logger,
        IWebHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception");

            // สร้างโฟลเดอร์ Logs
            var logDirectory = Path.Combine(_env.ContentRootPath, "Logs");
            Directory.CreateDirectory(logDirectory);

            // ตั้งชื่อไฟล์ตามวันที่
            var filePath = Path.Combine(
                logDirectory,
                $"Error_{DateTime.Now:yyyyMMdd}.txt");

            var log = new StringBuilder();
            log.AppendLine("====================================");
            log.AppendLine($"Time       : {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            log.AppendLine($"Path       : {context.Request.Path}");
            log.AppendLine($"Method     : {context.Request.Method}");
            log.AppendLine($"Message    : {ex.Message}");
            log.AppendLine($"StackTrace :");
            log.AppendLine(ex.StackTrace);

            if (ex.InnerException != null)
            {
                log.AppendLine();
                log.AppendLine("InnerException:");
                log.AppendLine(ex.InnerException.ToString());
            }

            log.AppendLine();

            await File.AppendAllTextAsync(filePath, log.ToString());

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(new
                {
                    message = "An unexpected error occurred."
                }));
        }
    }
}