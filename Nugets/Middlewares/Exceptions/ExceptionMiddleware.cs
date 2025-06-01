namespace Packages.Middlewares.Exceptions;

using System.Text.Json;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Logging;
using Middlewares.Logging;

public sealed class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
{
    private const string InternalServerErrorMessage = "Server Error";
    private const string ValidationErrorTitle = "Validation failure";

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            string correlationId = GetCorrelationId(context);
            string requestPath = context.Request.Path;
            string httpMethod = context.Request.Method;

            logger.LogError(
                ex,
                "[CorrelationId: {CorrelationId}] Exception on {Method} {Path}: {Message}",
                correlationId,
                httpMethod,
                requestPath,
                ex.Message);

            await HandleExceptionAsync(context, ex);
        }
    }

    private static int GetStatusCode(Exception exception) =>
        exception switch
        {
            BadHttpRequestException => StatusCodes.Status400BadRequest,
            ValidationException => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError,
        };

    private static string GetResponseTitle(Exception exception) =>
        exception switch
        {
            ValidationException => ValidationErrorTitle,
            _ => InternalServerErrorMessage,
        };

    private static Dictionary<string, string[]>? GetValidationErrors(Exception exception)
    {
        if (exception is ValidationException validationException)
        {
            var result = new Dictionary<string, string[]>();
            foreach (var item in validationException.Errors)
            {
                result.Add(item.PropertyName, new[] { item.ErrorMessage });
            }

            return result;
        }

        return null;
    }

    private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        var statusCode = GetStatusCode(exception);

        var response = new
        {
            title = GetResponseTitle(exception),
            status = statusCode,
            errors = GetValidationErrors(exception),
            correlationId = GetCorrelationId(httpContext),
        };

        httpContext.Response.ContentType = "application/json";

        httpContext.Response.StatusCode = statusCode;

        await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
    }

    private static string GetCorrelationId(HttpContext context)
    {
        if (context.Items.TryGetValue(CorrelationIdMiddleware.CorrelationIdHeaderString, out var value)
            && value is string id)
        {
            return id;
        }

        return context.TraceIdentifier;
    }

}