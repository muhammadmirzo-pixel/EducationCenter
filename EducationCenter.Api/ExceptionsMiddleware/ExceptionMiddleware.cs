using EducationCenter.Service.Exceptions;

namespace EducationCenter.Api.ExceptionsMiddleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate requestDelegate;
    
    public ExceptionMiddleware(RequestDelegate requestDelegate)
    {
        this.requestDelegate = requestDelegate;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await this.requestDelegate(context);
        }
        catch (CustomException ex)
        {
            context.Response.StatusCode = ex.StatusCode;
            await context.Response.WriteAsJsonAsync(new { StatusCode = ex.StatusCode, message = ex.Message }); 
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = 500;
            await context.Response.WriteAsJsonAsync(new { message = ex.Message });
        }
    }
}
