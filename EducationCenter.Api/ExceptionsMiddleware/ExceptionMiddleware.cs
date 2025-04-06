using Azure;
using EducationCenter.Service.Exceptions;
using EducationCenter.Api.Models;

namespace EducationCenter.Api.ExceptionsMiddleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate requestDelegate;
    private readonly ILogger<ExceptionMiddleware> logger;
    public ExceptionMiddleware(RequestDelegate requestDelegate, ILogger<ExceptionMiddleware> logger)
    {
        this.requestDelegate = requestDelegate;
        this.logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await requestDelegate(context);
        }
        catch (CustomException ex)
        {
            this.logger.LogError(ex.Message);
            context.Response.StatusCode = ex.statusCode;
            await context.Response.WriteAsJsonAsync(new Models.Response
            {
                StatusCode = ex.statusCode,
                Message = ex.Message
            });
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex.Message);
            context.Response.StatusCode = 500;
            await context.Response.WriteAsJsonAsync(new Models.Response
            {
                StatusCode = 500,
                Message = ex.Message
            });
        }
    }
}
