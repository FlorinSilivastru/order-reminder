namespace Middlewares.Audit;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Middlewares.Audit.Contracts;
using Middlewares.Audit.Interfaces;
using Middlewares.Logging;

public class AuditLogMiddleware(RequestDelegate next, IAuditLogger auditLogger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Path.ToString().Contains("HealthCheck"))
        {
            await next(context);
            return;
        }

        var correlationId = string.Empty;
        if (context.Request.Headers.TryGetValue(CorrelationIdMiddleware.CorrelationIdHeaderString, out var headerValue)
            && !string.IsNullOrWhiteSpace(headerValue))
        {
            correlationId = headerValue.ToString();
        }

        await LogRequest(context.Request, correlationId, auditLogger);

        var initialResponseStream = context.Response.Body;

        await using var responseStream = new MemoryStream();
        context.Response.Body = responseStream;

        await next(context);

        await LogResponse(context.Response, correlationId, context.Request, auditLogger);

        await responseStream.CopyToAsync(initialResponseStream);
    }

    private static async Task LogResponse(HttpResponse response, string? correlationId, HttpRequest request, IAuditLogger auditLogger)
    {
        response.Body.Seek(0, SeekOrigin.Begin);

        using var streamReader = new StreamReader(response.Body, leaveOpen: true);
        var responseBody = await streamReader.ReadToEndAsync();
        response.Body.Seek(0, SeekOrigin.Begin);

        var audit = new AuditEvent
        {
            AuditID = Guid.NewGuid(),
            IpAddress = request.HttpContext.Connection.RemoteIpAddress?.ToString(),
            Url = request.GetDisplayUrl(),
            UserName = request.HttpContext.User.Identity?.IsAuthenticated ?? false
                ? request.HttpContext.User.Identity.Name : "Anonymous",
            Response = responseBody,
            CorrelationId = correlationId,
        };

        await auditLogger.LogAsync(audit);
    }

    private static async Task LogRequest(HttpRequest request, string correlationId, IAuditLogger auditLogger)
    {
        using var streamReader = new StreamReader(request.Body, leaveOpen: true);
        var requestBody = await streamReader.ReadToEndAsync();

        var audit = new AuditEvent
        {
            AuditID = Guid.NewGuid(),
            IpAddress = request.HttpContext.Connection.RemoteIpAddress?.ToString(),
            Url = request.GetDisplayUrl(),
            UserName = request.HttpContext.User.Identity?.IsAuthenticated ?? false
                ? request.HttpContext.User.Identity.Name : "Anonymous",
            Request = "Request:" + requestBody,
            CorrelationId = correlationId,
        };

        await auditLogger.LogAsync(audit);
    }
}