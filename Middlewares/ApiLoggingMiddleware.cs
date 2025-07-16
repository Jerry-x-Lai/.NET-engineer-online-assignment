using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CryptoInfoApi.Middlewares
{
    /// <summary>
    /// 中介層：記錄所有 API Request 與 Response 的內容與狀態碼。
    /// </summary>
    public class ApiLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ApiLoggingMiddleware> _logger;

        public ApiLoggingMiddleware(RequestDelegate next, ILogger<ApiLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// 中介層主體，攔截並記錄 HTTP Request/Response。
        /// </summary>
        /// <param name="context">HTTP 請求內容</param>
        public async Task InvokeAsync(HttpContext context)
        {
            // Log Request
            context.Request.EnableBuffering();
            var requestBody = await new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true).ReadToEndAsync();
            context.Request.Body.Position = 0;
            _logger.LogInformation($"Request: {context.Request.Method} {context.Request.Path} Body: {requestBody}");

            // Log Response
            var originalBodyStream = context.Response.Body;
            using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            await _next(context);

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var responseText = await new StreamReader(context.Response.Body).ReadToEndAsync();
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            _logger.LogInformation($"Response: {context.Response.StatusCode} Body: {responseText}");

            await responseBody.CopyToAsync(originalBodyStream);
        }
    }
}
